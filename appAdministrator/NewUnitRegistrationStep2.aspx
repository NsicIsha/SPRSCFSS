<%@ Page Title="NSIC SPR :: New Unit Registration:Step 2" Language="C#" MasterPageFile="~/appMaster/Admin.master"
    AutoEventWireup="true" CodeFile="NewUnitRegistrationStep2.aspx.cs" Inherits="appAdministrator_NewUnitRegistrationStep2"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        // function disableBackButton() {
        //            window.history.forward();
        //        }
        //        setTimeout("disableBackButton()", 0);
        $(document).ready(function() {
            $("#progressbar").progressbar({ value: 30 });
        });

        function updateProgress() {
            var value = $("#progressbar").progressbar("option", "100");
            if (value < 100) {
                $("#progressbar").progressbar("value", value + 1);
            }
        }
        function UpdateProgressBar_DroDown() {
            $("#progressbar").progressbar({ value: 30 });
        }

        function getGrRow() {
            var layersGrid = document.getElementById('<%= gv_director.ClientID%>');
            {
                alert('0');
            }
            else
            {
               var count = (layersGrid.rows.length) - 1;
               alert(count);
            }
  
        }
        
         
          
               
    </script>
     <style type="text/css">
        #mask
        {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 4;
            opacity: 0.4;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=40)"; /* first!*/
            filter: alpha(opacity=40); /* second!*/
            background-color: gray;
            display: none;
            width: 100%;
            height: 100%;
        }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField runat ="server" ID ="hid_ppage" />
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">
                New Unit Registration - Step 2
            </td>
        </tr>
        <tr style="width: 95%;">
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                    <tr style="width: 100%;">
                        <td style="width: 100%;">
                            <table cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                                <tr>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                        <b>Step-1</b>
                                    </td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                        <b>Step-2</b>
                                    </td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                        Step-3
                                    </td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                        Step-4
                                    </td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                        Step-5
                                    </td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                        Step-6
                                    </td>
                                    <td style="width: 10%; padding-left: 10px; padding-right: 10px;">
                                        Step-7
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="width: 100%;">
                        <td id="step2" style="width: 100%;">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td id="progressbar" align="left">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" class="rowStep">
                            STEP 2 of 7
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height: 10px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (2) लघु उद्योग पंजीकरण संख्या एवं तारीख/ (कृपया प्रमाणपत्र की नोटरी द्वारा प्रमाणित
                            लगाएं)<br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EM Registration No. & Date/ (Please attach a
                            Notarized copy of certificate)
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        <asp:HiddenField ID ="hdncount" runat ="server" Value ="" />
                            <asp:Panel runat="server" ID="pnl_ssi" ScrollBars="Auto" Width="98%">
                                <asp:GridView ID="grdSSI" runat="server"  AutoGenerateColumns="False" OnRowDataBound="grdSSI_RowDataBound"
                                    HorizontalAlign="Left" Width="100%">
                                    <HeaderStyle CssClass="GrdviewHeader" Height="30" />
                                    <RowStyle CssClass="GrdviewRowOdd" />
                                    <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                    <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                    <Columns>
                                        <asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory Address"
                                            ItemStyle-Width="400px">
                                            <ItemStyle Width="400px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="EM Part-II No. / Udyog Aadhar Memorandum No. ">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_21" runat="server" Text='<%# Bind("fGPtbl_CUnit_SSIRegistrationNo") %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdnunit" runat="server" Value='<%# Bind("fGPtbl_CUnit_Id") %>' />
                                                <asp:RequiredFieldValidator ID="RFValidatortxt21" runat="server" ErrorMessage="Please enter EM number"
                                                    ControlToValidate="txt_21" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="VCExtendertxt21" runat="server" TargetControlID="RFValidatortxt21">
                                                </asp:ValidatorCalloutExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EM Part-II Date/ Udyog Aadhar Memorandum Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_22" runat="server" Text='<%# Bind("fGPtbl_CUnit_SSIRegistrationDate") %>'></asp:TextBox>
                                                
                                                <asp:RequiredFieldValidator ID="RFValidatortxt_22" runat="server" ErrorMessage="Date is required"
                                                    Display="Dynamic" ControlToValidate="txt_22" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CValidatortxt_22" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                                    Display="Dynamic" ControlToValidate="txt_22" Operator="DataTypeCheck" Type="Date"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                                <asp:CalendarExtender ID="CExtendertxt_22" runat="server" PopupButtonID="txt_22"
                                                    TargetControlID="txt_22">
                                                </asp:CalendarExtender>
                                                <asp:ValidatorCalloutExtender ID="VCExtendertxt22" runat="server" TargetControlID="RFValidatortxt_22"
                                                    PopupPosition="TopLeft">
                                                </asp:ValidatorCalloutExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                        
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="3" style="height: 10px;">
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (3) आवेदक फर्म की स्थिति/ Status of applicant firm
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (3.a)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>संस्थापना की तारीख/ Date of Incorporation:
                        </td>
                        <td align="left" valign="top" style="padding-bottom: 10px;">
                            <asp:TextBox ID="txt_3a" runat="server" CssClass="txt_bx" TabIndex="3" ValidationGroup="1"
                                MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFValidatortxt_3a" runat="server" Display="None"
                                ErrorMessage="Please Enter Date of Incorporation" ControlToValidate="txt_3a"
                                SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="VCExtendertxt_3a" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="RFValidatortxt_3a">
                            </asp:ValidatorCalloutExtender>
                            
                            <asp:CalendarExtender ID="CExtendertxt_3a" runat="server" TargetControlID="txt_3a">
                            </asp:CalendarExtender>
                            <asp:CompareValidator ID="CValidatortxt_3a" runat="server" Display="Dynamic" SetFocusOnError="true"
                                ValidationGroup="1" ErrorMessage="Please enter date in MM/DD/YYYY format" ControlToValidate="txt_3a"
                                Operator="DataTypeCheck" Type="Date">
                            </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (3.b)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>उत्पादन/ कारोबार आरम्भ करने की तारीख<br />
                            Date of commencement of production/business:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3b" runat="server" CssClass="txt_bx" MaxLength="50" TabIndex="4"
                                ValidationGroup="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFValidatortxt_3b" runat="server" Display="None"
                                ErrorMessage="Please Enter Date of commencement of production/business" ControlToValidate="txt_3b"
                                SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="VCExtendertxt_3b" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="RFValidatortxt_3b">
                            </asp:ValidatorCalloutExtender>
                            
                            <asp:CalendarExtender ID="CalendarExtendertxt_3b" runat="server" TargetControlID="txt_3b">
                            </asp:CalendarExtender>
                            <asp:CompareValidator ID="CompareValidatortxt_3b" runat="server" Display="Dynamic" SetFocusOnError="true"
                                ValidationGroup="1" ErrorMessage="Please enter date in MM/DD/YYYY format" ControlToValidate="txt_3b"
                                Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (3.c)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>फर्म की सरंचना/ Constitution of the firm:
                        </td>
                        <td align="left" valign="top">
                            <asp:RadioButtonList ID="rd3c" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
                                TabIndex="5">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (3.c.a) स्वामी/साझेदारों/निदेशकों/कर्ता जिनका फर्म से हित है, के नाम, पते व टेलीफोन
                            सं.<br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Names,
                            addresses and telephone Nos. of Proprietor/Partners/Directors/Karta having interest
                            in the firm
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading" colspan="3">
                            <asp:Panel runat="server" ID="pnl_director" ScrollBars="Auto" Width="98%">
                                <asp:GridView ID="gv_director" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    OnRowDataBound="gv_director_RowDataBound" DataKeyNames="rowno" OnRowEditing="gv_director_RowEditing"
                                    OnRowUpdating="gv_director_RowUpdating" 
                                    onrowdeleting="gv_director_RowDeleting">
                                    <HeaderStyle CssClass="GrdviewHeader" Height="30" />
                                    <RowStyle CssClass="GrdviewRowOdd" />
                                    <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                    <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                    <Columns>
                                        <asp:TemplateField HeaderText=" नाम/ Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_directorname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_directorname" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_directorname" runat="server" Width="120px" ValidationGroup="gridd"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqdirectorname" runat="server" Display="None" ErrorMessage="Please Enter DIRECTOR Name"
                                                    ControlToValidate="txt_foot_directorname" SetFocusOnError="true" ValidationGroup="gridd"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="vc_directorname" runat="server" PopupPosition="BottomLeft"
                                                    TargetControlID="reqdirectorname">
                                                </asp:ValidatorCalloutExtender>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" पता/ Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_director_address" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_director_address" runat="server" TextMode="MultiLine" Height="100px"
                                                    Text='<%# Bind("Address") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_directoraddress" TextMode="MultiLine" Height="100px" runat="server"
                                                    ValidationGroup="gridd"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqdirectoraddress" runat="server" Display="None"
                                                    ErrorMessage="Please Enter DIRECTOR Address" ControlToValidate="txt_foot_directoraddress"
                                                    SetFocusOnError="true" ValidationGroup="gridd"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="vc_directoraddress" runat="server" PopupPosition="BottomLeft"
                                                    TargetControlID="reqdirectoraddress">
                                                </asp:ValidatorCalloutExtender>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" Display="None" SetFocusOnError="true"
                                                    ErrorMessage="Director address max lenght is 5000 characters" ControlToValidate="txt_foot_directoraddress"
                                                    ValidationGroup="gridd" ClientValidationFunction="CharacterLimit5000"></asp:CustomValidator>
                                                <asp:ValidatorCalloutExtender ID="vc_customdirectoraddress" runat="server" PopupPosition="BottomLeft"
                                                    TargetControlID="CustomValidator1">
                                                </asp:ValidatorCalloutExtender>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" पिन कोड/ Pin Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_director_pincode" runat="server" Text='<%# Bind("Pincode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_director_pincode" runat="server" Width="50px" Text='<%# Bind("Pincode") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_directorzipcode" ValidationGroup="gridd" MaxLength="6" Width="50px" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqpincode" runat="server" Display="None" ErrorMessage="Please Enter DIRECTOR Pin Code"
                                                    ControlToValidate="txt_foot_directorzipcode" SetFocusOnError="true" ValidationGroup="gridd"></asp:RequiredFieldValidator>
                                                <asp:FilteredTextBoxExtender ID="fitdirectorpincode" TargetControlID="txt_foot_directorzipcode"
                                                    FilterType="Numbers" runat="server">
                                                </asp:FilteredTextBoxExtender>
                                                <asp:ValidatorCalloutExtender ID="vc_directorpincode" runat="server" PopupPosition="BottomLeft"
                                                    TargetControlID="reqpincode">
                                                </asp:ValidatorCalloutExtender>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="  राज्य/ State">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_state" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lbledit_state" runat="server" Visible="false" Text='<%# Bind("stateid") %>'></asp:Label>
                                                <asp:DropDownList ID="ddl_director_state" OnSelectedIndexChanged="bind_city_edit_statechange"
                                                    AutoPostBack="true" runat="server">
                                                </asp:DropDownList>
                                                <%--<asp:TextBox ID="TextBox4" runat="server" 
                                                        Text='<%# Bind("fGPtbl_Company_Directors_StateId") %>'></asp:TextBox>--%>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddl_footer_state" AutoPostBack="true" runat="server" ValidationGroup="gridd"
                                                    OnSelectedIndexChanged="bind_city_footer_statechange" Width="150px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqstate" runat="server" Display="None" ErrorMessage="Please Select State"
                                                    ControlToValidate="ddl_footer_state" InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="gridd"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="vcstate" runat="server" PopupPosition="BottomLeft"
                                                    TargetControlID="reqstate">
                                                </asp:ValidatorCalloutExtender>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ज़िला/शहर/District/ City">
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
                                                <asp:RequiredFieldValidator ID="reqcity" runat="server" Display="None" ErrorMessage="Please Select City"
                                                    ControlToValidate="ddl_footer_city" SetFocusOnError="true" ValidationGroup="gridd"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="vc_city" runat="server" PopupPosition="BottomLeft"
                                                    TargetControlID="reqcity">
                                                </asp:ValidatorCalloutExtender>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" फोन/ Telephone/Mobile">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_director_phone" runat="server" Text='<%# Bind("Telephone") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_director_phone" runat="server" Width="80px" Text='<%# Bind("Telephone") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_director_phone" Width="80px" runat="server"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="fit_tel" runat="server" FilterMode="ValidChars"
                                                    FilterType="Custom" TargetControlID="txt_director_phone" ValidChars="1234567890,() .-+P">
                                                </asp:FilteredTextBoxExtender>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                     
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_directorEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_directorEmail" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_directorEmail" runat="server" Width="120px" ValidationGroup="gridd"></asp:TextBox>
                                               <%-- <asp:RequiredFieldValidator ID="reqdirectoremail" runat="server" Display="None" ErrorMessage="Please Enter DIRECTOR Email"
                                                    ControlToValidate="txt_foot_directorEmail" SetFocusOnError="true" ValidationGroup="gridd"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="vc_directoremail" runat="server" PopupPosition="BottomLeft"
                                                    TargetControlID="reqdirectoremail">
                                                </asp:ValidatorCalloutExtender>--%>

                                                 <asp:RegularExpressionValidator ID="RegularExpressionvalidoremail" runat="server" ControlToValidate="txt_foot_directorEmail"
                                                Display="None" ErrorMessage="Please provide valid Email Id !" SetFocusOnError="True"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1">
                                            </asp:RegularExpressionValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender51" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RegularExpressionvalidoremail">
                                            </asp:ValidatorCalloutExtender>



                                            </FooterTemplate>
                                        </asp:TemplateField>



                                         <asp:TemplateField HeaderText="Length of Experience(in years)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_director_exp" runat="server" Text='<%# Bind("Experience") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_director_exp" runat="server" Width="50px" Text='<%# Bind("Experience") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_director_exp" runat="server" Width="50px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Adhar No.(optional)" HeaderStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_director_Adhar" runat="server" Text='<%# Bind("Adhar") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_director_Adhar" runat="server" Width="50px" Text='<%# Bind("Adhar") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_director_Adhar" runat="server" Width="50px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                     
                                        <asp:TemplateField HeaderText="हिस्सेदारी/ Shareholding">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_director_share" runat="server" Text='<%# Bind("ShareHolding") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_director_share" runat="server" Width="50px" Text='<%# Bind("ShareHolding") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_director_share" runat="server" Width="50px"></asp:TextBox>
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
                                                     OnClientClick="return confirm('Are you want to delete ?');"   CommandName="delete" CausesValidation="false">
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
                            </asp:Panel>
                        </td>
                    </tr>

                    <%--<tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (3.c.b) प्रदर्शन विवरण 
                             <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   Performance Statement
                        </td>
                    </tr>


                     <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading" colspan="3">
<asp:Panel runat="server" ID="pnl_PerformanceSheet" ScrollBars="Auto"  >
                                <asp:GridView ID="gv_PerformanceSheet"  runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="PrimaryId"
                                      OnRowEditing="gv_PerformanceSheet_RowEditing"
                                    OnRowUpdating="gv_PerformanceSheet_RowUpdating" 
                                    onrowdeleting="gv_PerformanceSheet_RowDeleting"
                                    
                                    >
                                    <HeaderStyle CssClass="GrdviewHeader" Height="30" />
                                    <RowStyle CssClass="GrdviewRowOdd" />
                                    <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                    <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                    <Columns>
                                        <asp:TemplateField HeaderText=" नाम/ Order Number, Date & Authority placing the contracts">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_OrderNumber" runat="server" Text='<%# Bind("OrderNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_OrderNumber" TextMode ="MultiLine" runat="server" Text='<%# Bind("OrderNumber") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_OrderNumber" runat="server" TextMode="MultiLine" Width="120px" ValidationGroup="gridd"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqdirectorname" runat="server" Display="None" ErrorMessage="Please Enter Order Number"
                                                    ControlToValidate="txt_foot_OrderNumber" SetFocusOnError="true" ValidationGroup="gridd"></asp:RequiredFieldValidator>
                                              
                                            </FooterTemplate> 
                                        </asp:TemplateField>
                                       
                                         <asp:TemplateField HeaderText=" पता/ Date of Receipt of Order ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Date_of_ReceiptOrder" runat="server" Text='<%# Bind("Date_of_ReceiptOrder") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Date_of_ReceiptOrder" runat="server" TextMode="MultiLine" Height="100px"
                                                    Text='<%# Bind("Date_of_ReceiptOrder") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_Date_of_ReceiptOrder"  runat="server"
                                                    ValidationGroup="gridd"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqdirectoraddress" runat="server" Display="Dynamic" 
                                                    ErrorMessage="Please Enter Date of Receipt of Order" ControlToValidate="txt_foot_Date_of_ReceiptOrder"
                                                    SetFocusOnError="true" ValidationGroup="gridd"></asp:RequiredFieldValidator>
                                               
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" पिन कोड/ Description of Stores/Product/Services">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Store_Produt_Service_Desc" runat="server" Text='<%# Bind("Store_Produt_Service_Desc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Store_Produt_Service_Desc" runat="server" Width="50px" Text='<%# Bind("Store_Produt_Service_Desc") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_Store_Produt_Service_Desc" ValidationGroup="gridd"  TextMode="MultiLine"  runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqpincodeDesc" runat="server" Display="Dynamic" ErrorMessage="Please Enter Description of Stores/Product/Services"
                                                    ControlToValidate="txt_foot_Store_Produt_Service_Desc" SetFocusOnError="true" ValidationGroup="gridd"></asp:RequiredFieldValidator>
                                                 
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="  राज्य/ Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Quantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Quantity" runat="server" 
                                                        Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                 <asp:TextBox ID="txt_foot_Quantity" ValidationGroup="gridd"  TextMode="SingleLine"  runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqpincodeQ" runat="server" Display="Dynamic" ErrorMessage="Please Enter Quantity"
                                                    ControlToValidate="txt_foot_Quantity" SetFocusOnError="true" ValidationGroup="gridd"></asp:RequiredFieldValidator>
                                                 
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ज़िला/शहर/Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Value" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Value" runat="server" 
                                                        Text='<%# Bind("Value") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                 <asp:TextBox ID="txt_foot_Value" ValidationGroup="gridd"  TextMode="SingleLine"  runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqpincodeV" runat="server" Display="Dynamic" ErrorMessage="Please Enter Value"
                                                    ControlToValidate="txt_foot_Value" SetFocusOnError="true" ValidationGroup="gridd"></asp:RequiredFieldValidator>
                                                 
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" फोन/ Name of Consignee">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Name_Consignee" runat="server" Text='<%# Bind("Name_Consignee") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Name_Consignee" runat="server" Width="80px" Text='<%# Bind("Name_Consignee") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_Name_Consignee" Width="80px" runat="server"></asp:TextBox>
                                               
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                     
                                        <asp:TemplateField HeaderText="Due date of Delivery">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_DeliveryDueDate" runat="server" Text='<%# Bind("DeliveryDueDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_DeliveryDueDate" runat="server" Text='<%# Bind("DeliveryDueDate") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_DeliveryDueDate" runat="server" Width="120px" ValidationGroup="gridd"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date of Material Delivery">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_MaterialDeliveryDate" runat="server" Text='<%# Bind("MaterialDeliveryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_MaterialDeliveryDate" runat="server" Text='<%# Bind("MaterialDeliveryDate") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_MaterialDeliveryDate" runat="server" Width="120px" ValidationGroup="gridd"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Despatching particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_DespatchingParticulars" runat="server" Text='<%# Bind("DespatchingParticulars") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_DespatchingParticulars" runat="server" Width="50px" Text='<%# Bind("DespatchingParticulars") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_DespatchingParticulars" runat="server" Width="50px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Remarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Remarks" runat="server" Width="50px" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_Remarks" runat="server" TextMode="MultiLine" ></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                     
                                         
                                        <asp:TemplateField HeaderText="Options">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_edit" runat="server" CommandArgument='<%# Bind("ordernumber") %>'
                                                    CommandName="edit" CausesValidation="false">
                                                   Edit
                                                </asp:LinkButton>
                                                &nbsp;&nbsp;
                                                 <asp:LinkButton ID="lnk_delete" runat="server" CommandArgument='<%# Bind("ordernumber") %>'
                                                  Visible="false"   OnClientClick="return confirm('Are you want to delete ?');"   CommandName="delete" CausesValidation="false">
                                                   Delete
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnk_update" runat="server"  CommandArgument='<%# Bind("ordernumber") %>' CommandName="update" CausesValidation="false">
                                                   Update
                                                </asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btn_add" runat="server" Text="Add New" ValidationGroup="gridd" OnClick="AddnewPerformanceSheet" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
</td>
                    </tr>--%>

                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            (3.c.b)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या आप किसी बड़े उद्योग या रा.ल.उ.नि./ डी.जी.एस.
                            एंड डी से पंजीकृत इकाइयों के सांझेदार/ निदेशक हैं यदि हां तो उनका ब्यौरा दीजिए<br />
                            Whether you are a Partner/Director of any large industries or units registered with
                            NSIC/DGS&D. If so, give details:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3cb" runat="server" CssClass="txt_bx" MaxLength="5000" TextMode="MultiLine"
                                Height="80" TabIndex="36" ValidationGroup="1" Text="N.A"></asp:TextBox>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None"
                                ErrorMessage="Please Enter &quot;Whether you are a Partner/Director of any large industries or units registered with NSIC/DGS&amp;D. If so, give details&quot;"
                                ControlToValidate="txt_3cb" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator6">
                            </asp:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="CustomValidator6" runat="server" Display="None" SetFocusOnError="true"
                                ValidationGroup="1" ErrorMessage="Max lenght is 5000 characters" ControlToValidate="txt_3cb"
                                ClientValidationFunction="CharacterLimit5000"></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator6">
                            </asp:ValidatorCalloutExtender>--%>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                        </td>
                        <td align="left" valign="top">
                        </td>
                    </tr>
                    <tr align="right" valign="top">
                        <td colspan="2">
                            <asp:Button ID="btn_back" runat="server" Text="BACK" CssClass="btnCls" CausesValidation="false"
                                OnClick="btn_back_Click" />
                            <%--   <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_3b" MaskType="Date"
                                Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                                ClearTextOnInvalid="true">
                                </asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlToValidate="txt_3b" ControlExtender="MaskedEditExtender2"
                                InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                                TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required" ></asp:MaskedEditValidator>--%>
                        </td>
                        <%--   <input type="button" id="btnback" class="btnCls" value="Back"   onclick ="Javascript:window.history.back(); return false;"/>--%>
                        <td>
                            <asp:UpdatePanel ID="updSubmt" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div style="float: right;">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" CssClass="btnCls"
                                                TabIndex="37" ValidationGroup="1" Width="150px"  OnClick="btnSubmit_Click" />
                                        </div>
                                        <div style="float: right;">
                                            <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updSubmt">
                                                <ProgressTemplate>
                                                    <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--<td>  
                            <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" 
                                CssClass="btnCls" TabIndex="37"
                                ValidationGroup="1" Width="150px" onclick="btnSubmit_Click" />
                        </td>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: left;" class="MandatoryMessage">
                <span class="MandatoryField">*</span> Mandatory Fields
            </td>
        </tr>
        
   
    </table>
</asp:Content>
