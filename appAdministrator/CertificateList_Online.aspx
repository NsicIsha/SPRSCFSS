<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminOnline.master" AutoEventWireup="true" CodeFile="CertificateList_Online.aspx.cs" Inherits="appAdministrator_CertificateList_Online" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../JScript/jquery-1.8.2.js"></script>

    <script language="javascript" type="text/javascript">
        function FunResetcertificate() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtRequest").value = "";
        }


        function Confirmrenewal(companyId, Url) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            var c = confirm("Renewal is under process (Click on OK to continue Amendment) .........??  ");
            if (c == true) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "CertificateList.aspx/CleanUncompletedata",
                    data: "{'companyId':'" + companyId + "','fomtyeId':'2'}",
                    dataType: "json",
                    success: function (data) {
                        var obj = data.d;
                        if (obj == 1) {

                            window.location = Url;
                        }
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });

                return true;

            }
            else {

                return false;

            }
            // document.forms[0].appendChild(confirm_value);
        }

        function Confirmamendent(companyId, Url) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            var c = confirm("Amendment is under process (Click on OK to continue Renewal) .........??  ");
            if (c == true) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "CertificateList.aspx/CleanUncompletedata",
                    data: "{'companyId':'" + companyId + "','fomtyeId':'3'}",
                    dataType: "json",
                    success: function (data) {
                        var obj = data.d;
                        if (obj == 1) {

                            window.location = Url;
                        }
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });

                return true;

            }
            else {

                return false;

            }
            // document.forms[0].appendChild(confirm_value);
        }

        function validDate() {
            var textboxval = $("#ctl00_ContentPlaceHolder1_txtvaliditydate").val();
            var hiddenprintflag = $("#ctl00_ContentPlaceHolder1_gvCertificateDetails_ctl02_HiddenFormPrintFlag").val();
            var hiddenprint = hiddenprintflag.split(',');
            alert(hiddenprint[0].val());
            alert(hiddenprintflag);
            if (hiddenprint[0].val() == 5) {
            }
            else {
                var fromdate = new Date(textboxval);
                if (fromdate <= new Date('04/01/2015')) {
                    alert("Please Enter another Date");
                    $("#ctl00_ContentPlaceHolder1_txtvaliditydate").val("");
                    return false;
                }
            }

        }



    </script>
    <style type="text/css">
        .txtlbl {
            height: 20px;
            width: 20%;
            float: right;
            white-space: nowrap;
            vertical-align: top;
            padding-left: 600px;
        }
        .auto-style1 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 35px;
            font-weight: bold;
            padding-left: 5px;
            padding-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:hiddenfield runat="server" id="hid_val" value="0" />
    <div align="left">
        <div align="left">
            <asp:label id="lblRegistration" runat="server" cssclass="headerAll" text="Print Certificate"></asp:label>
        </div>

        <div align="left" style="height: 10px;">
        </div>

        <div align="center">
            <asp:literal id="ltlMessage" runat="server"></asp:literal>
        </div>
        <div style="height: 10px;">
        </div>

        <div align="left" class="clsrowdiv">
            <div class="leftext" style="width: 7%;"></div>
            <div class="coloncls" style="width: 100%;">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
<td align="left" valign="top" class="auto-style1"><span class="MandatoryField">*</span>आपके पास उद्योग आधार संख्या है या उदयम पंजीकरण संख्या / <%--Do--%> You have UAM Number or UDYAM Registration Number</td>
<td align="left" valign="top" class="auto-style1" colspan="2">
     <asp:RadioButtonList ID="rdbMsmeRegtype" Width="250px" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbMsmeRegtype_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="UAM"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Udyam Registration"></asp:ListItem>
                                                              </asp:RadioButtonList>

                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                                ErrorMessage="Please Select Unit have UAM or Udyami Registration Number" ControlToValidate="rdbMsmeRegtype"   SetFocusOnError="true"
                                                ValidationGroup="1"></asp:RequiredFieldValidator>

</td>
                                                </tr>

                                                <tr id="divUAMPAN" runat="server" visible="false" style="padding-top:25px">
                                                   <td align="left" valign="top" class="leftHeading">
                                                     

                                                       <span class="MandatoryField"></span>
                                            <asp:Label ID="lblUamNameDisplay" runat="server"  Text="उद्योग आधार संख्या /Udyog Aadhar Number:" Visible="false" ></asp:Label>
                                            <asp:Label ID="lblUamiReigistrationNameDisplay" runat="server" Text="उदयम पंजीकरण संख्या/Udyam Registration Number:" Visible="false"></asp:Label>
                                           

                                                        
                                                    <div>
                                                        <div style="float: left;">
                                                            <asp:TextBox ID="txtUAM" runat="server"  MaxLength="19" TabIndex="1"
                                                                ValidationGroup="validate" AutoPostBack="true" Style="text-transform: uppercase;"
                                                               ></asp:TextBox>
                                                           
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
                                                                ErrorMessage="Please Enter Valid Aadher Number" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                                ValidationExpression="[A-Za-z]{2}\d{2}[A-Za-z]{1}\d{7}" ValidationGroup="validate"></asp:RegularExpressionValidator>
                                                           


                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic"
                                                                ErrorMessage="Please Enter Valid Udyam Registration Number (e.g. UDYAM-UP-02-0000100)" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                                ValidationExpression="[A-Za-z]{5}(-)[A-Za-z]{2}(-)\d{2}(-)\d{7}" ValidationGroup="validate"></asp:RegularExpressionValidator>
                                                           
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic"
                                                                ErrorMessage="Please Enter aadhar Number" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                                ValidationGroup="validate"></asp:RequiredFieldValidator>
                                                            


                                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                ErrorMessage="Please Enter Udyam Registration Number" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                                ValidationGroup="validate"></asp:RequiredFieldValidator>
                                                          

                                                        </div>


                                                        <div style="float: left;">
                                                           


                                                            <asp:Label ID="Label2" runat="server" CssClass="errMessage" Text="Aahar already exists !"
                                                                Visible="false"></asp:Label>
                                                        </div>

                                                        
                                                    </td>
                                                    <td align="left" valign="top" class="leftHeading">
                                                         <span class="MandatoryField">*</span>पैन कार्ड संख्या/ PAN Card Number:   
                                                        <asp:TextBox ID="txtPan" runat="server" MaxLength="10" ValidationGroup="validate" style="text-transform:uppercase;" />
                                                        <asp:RequiredFieldValidator ControlToValidate="txtPan" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPan" 
                                                            Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$" ValidationGroup="validate"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td align="left" valign="top" class="leftHeading" style="padding-top:15px;">
                                                        <asp:Button ID="btnf" runat="server" Text="Fetch" onclick="btnSearch_Click" Width="200px" CssClass="btnCls"  ValidationGroup="validate" />

                                                        <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel3">

                                                            <ProgressTemplate>
                                                                <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                                                Processing...            
                                                            </ProgressTemplate>

                                                        </asp:UpdateProgress>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>

                        </ContentTemplate>
                                                   </asp:UpdatePanel>


              
            </div>
            <div class="txtdiv" style="width: 25%;">
            
            </div>
            <div class="btndiv" style="width: 40%;">
              





            </div>


            <div class="txtlbl">
                <b><span style="color: Red;">
                    <asp:label id="lbl_count_text" runat="server" text=""></asp:label>
                </span></b>
            </div>
            <div style="height: 10px;">
            </div>

            <div align="left" style="padding-left: 10px; padding-right: 10px; padding-top:125px">
                <asp:gridview id="gvCertificateDetails" runat="server" autogeneratecolumns="false"
                    width="100%" borderwidth="0" cellpadding="0" cellspacing="1"
                    onrowdatabound="gvCertificateDetails_RowDataBound"
                    onrowcommand="gvCertificateDetails_RowCommand" onpageindexchanging="CertificateListPageindexChanging" allowpaging="True" allowsorting="True" pagesize="10"> 
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fGPtbl_CUnit_RegistrationNo" HeaderText="SPR No." />
                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
           
                         <asp:TemplateField HeaderText="PAN No.">
                            <ItemTemplate>
                                   <asp:Label ID="lbl_pan_text" runat="server" Text='<%#Eval("fGPtbl_Company_PAN") %>'></asp:Label>
                                   <br />
                                   <asp:Label ID="lbl_hscode_pncode" runat="server" Text='<%#Eval("HSCODENPC") %>'></asp:Label>
                            </ItemTemplate>
                         </asp:TemplateField>
                <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                <asp:TemplateField HeaderText="Print<br>Certificate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbnPrint" runat="server" CommandName="print"
                            CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_PrintAccess").ToString()+","+Eval("fGPtbl_CR_AcceptedByAdmin").ToString() %>' CausesValidation="false" ToolTip="Print Certificate">Print</asp:LinkButton>&nbsp;
                       <%-- <asp:LinkButton ID="lbnRequest" runat="server" CausesValidation="false" CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_Id")%>'
                            CommandName="request" ToolTip="Make request for print" Width="60px">Request</asp:LinkButton>&nbsp;
                        <asp:Literal ID="ltlPrint" runat="server"></asp:Literal>--%>
                        <asp:HiddenField ID="HiddenFieldCompanyId" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                     <asp:HiddenField ID="HiddenFormPrintFlag" runat="server" Value='<%#Eval("doicheck").ToString()+","+Eval("RegistrationDate").ToString() %>' />

                       <%-- <asp:HiddenField ID="HiddenFieldCompanyIdAmendment" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                         <asp:HiddenField ID="HFRenewalOpenFlag" runat="server" Value='<%#Eval("RenFlag") %>' />--%>
                  <%-- <asp:Literal ID="ltlrenmessage" runat="server" Text="Renewal process started"></asp:Literal>--%>
                         <%--<asp:HiddenField ID="HiddenFieldCompanyId1" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                          <asp:HiddenField ID="HiddenAmedment" runat="server" Value='<%#Eval("fgtbl_Certificate_Amedment")%>' />--%>
                    </ItemTemplate>
                </asp:TemplateField>
              <%--  <asp:TemplateField HeaderText="Draft<br>Print" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbnDraftPrint" runat="server" CommandName="draftprint"
                            CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="Draft print">Draft Print
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%-- <asp:TemplateField HeaderText="Duplicate<br>Print" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbnDuplicatePrint" runat="server" CommandName="Duplicateprint"
                            CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="Duplicate print">Duplicate Print
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
               <%-- <asp:TemplateField HeaderText="Amendment" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                  <asp:LinkButton ID="lnkamedment" runat="server" CommandName="Amendment" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>'
                   CausesValidation="false" ToolTip="Amendment">Amendment</asp:LinkButton>&nbsp;
                   <asp:HiddenField ID="HiddenFieldCompanyIdAmendment" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                    <asp:Literal ID="lt_amendment" Visible ="false" runat="server" Text="Amendment process started"></asp:Literal>
                  </ItemTemplate>
              </asp:TemplateField>--%>
                  <%--<asp:TemplateField Visible ="false" >
                  <ItemTemplate>
                  <asp:LinkButton ID="lnkamdement_print" runat="server"  CommandName="APrint"   CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_PrintAccess").ToString()+","+Eval("fgtbl_Certificate_Amedment").ToString() %>'
                    CausesValidation="false" ToolTip="Amendment Print">APrint</asp:LinkButton>
                   <asp:LinkButton ID="lbnRequest1" runat="server" CausesValidation="false" CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_Id")%>'
                            CommandName="request1" ToolTip="Make request for print" Width="60px" Visible="false">Request</asp:LinkButton>&nbsp;
                        <asp:Literal ID="ltlPrint1" runat="server"></asp:Literal>
                        <asp:HiddenField ID="HiddenFieldCompanyId1" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                          <asp:HiddenField ID="HiddenAmedment" runat="server" Value='<%#Eval("fgtbl_Certificate_Amedment")%>' />
                  </ItemTemplate>
                  
                  </asp:TemplateField>--%>
                 <%-- <asp:TemplateField HeaderText="Renewal" ItemStyle-HorizontalAlign="Center" Visible="true" >
               <ItemTemplate>
                  <asp:LinkButton ID="lnkRenewal" runat="server" CommandName="Renewal" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>'
                   CausesValidation="false" ToolTip="Renewal">Renewal</asp:LinkButton>&nbsp;
                   <asp:HiddenField ID="HFRenewalOpenFlag" runat="server" Value='<%#Eval("RenFlag") %>' />
                   <asp:Literal ID="ltlrenmessage" runat="server" Text="Renewal process started"></asp:Literal>
                  </ItemTemplate>
              </asp:TemplateField>--%>
                  
               
               </Columns>
        </asp:gridview>

            </div>
        </div>


    </div>

 
        <asp:panel id="pnlOut" runat="server" cssclass="outerPopup">
        <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup">
            <center>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr valign="top" align="left" style="width:100%;">
                    <td valign="top" align="left" style="width:100%;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr valign="top" align="left" style="width:100%">
                                <td valign="top" align="right" colspan="2" style="padding-bottom:10px; padding-right:5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr align="left" valign="top">
                                                        <td align="center" valign="top" class="updateInnerHeader">
                                                            <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>
                                                            <asp:HiddenField ID="hdnfieldCertificateId" runat="server" />
                                                            <asp:HiddenField ID="hdnfieldcompanyid" runat="server" />
                                                        </td>
                                                        <td align="right" valign="top" style="width: 20px;">
                                                            <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnBack" runat="server"
                                                                ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                    
                                </td>
                            </tr>
                            
                            
                            
                              <tr valign="top" align="left">
                                <td valign="top" align="left" class="updateInnerColumn" >
                                    Reason for Request :
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtRequest" runat="server" MaxLength="5000"
                                        CssClass="txt_bx" TextMode="MultiLine"></asp:TextBox>
                                 </td>
                               </tr>
                               
                               
                               <tr valign="top" align="left">
                                <td align="left" valign="top" >
                                </td>
                               </tr>
                               <tr valign="top" align="center" >
                                <td valign="top" align="left" >
                                </td>
                                <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                        ValidationGroup="1" CssClass="btnCls" onclick="btnSubmit_Click"/>
                                        &nbsp;&nbsp;
                                        <input id="btnReset" type="button" value="Reset"class="btnCls" onclick="return FunResetcertificate();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
                <asp:RequiredFieldValidator ID="RFValidatorRequest" runat="server" ErrorMessage="Please provide Reason for request"
                SetFocusOnError="true" ControlToValidate="txtRequest" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidatorRequest" runat="server" ErrorMessage="Reason should be less than 5000 characters"
                Display="None" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txtRequest"
                ValidationGroup="1"></asp:CustomValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RFValidatorRequest">
                </asp:ValidatorCalloutExtender>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="CustomValidatorRequest">
                </asp:ValidatorCalloutExtender>
           </center>
        </asp:Panel>
    </asp:panel>


    <asp:modalpopupextender id="ModalPopupExtender1" runat="server" popupcontrolid="pnlOut"
        targetcontrolid="LinkButton1" cancelcontrolid="btnBack" backgroundcssclass="modalBackground">
    </asp:modalpopupextender>
    <asp:roundedcornersextender id="RoundedCornersExtender1" targetcontrolid="pnlInner"
        runat="server" bordercolor="#22518F" radius="10" corners="All">
    </asp:roundedcornersextender>
    <asp:linkbutton id="LinkButton1" runat="server" style="display: none;"></asp:linkbutton>

 




     <asp:panel id="pnlOutprint" runat="server" cssclass="outerPopup">
        <asp:Panel ID="pnlInnerprint" runat="server" Width="400px" CssClass="innerPopup">
            <center>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr valign="top" align="left" style="width:100%;">
                    <td valign="top" align="left" style="width:100%;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr valign="top" align="left" style="width:100%">
                                <td valign="top" align="right" colspan="2" style="padding-bottom:10px; padding-right:5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr align="left" valign="top">
                                                        <td align="center" valign="top" class="updateInnerHeader">
                                                            <asp:Label ID="Label1" runat="server" CssClass="header" Visible="False"></asp:Label>
                                                            <asp:HiddenField ID="hidpopupcompanyid" runat="server" />
                                                        </td>
                                                        <td align="right" valign="top" style="width: 20px;">
                                                            <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnClose" runat="server"
                                                                ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                    
                                </td>
                            </tr>
                            
                            
                            
                              <tr valign="top" align="left" id ="tr_validity" runat ="server" visible="false">
                                <td valign="top" align="left" class="updateInnerColumn" >
                                   VALIDITY START DATE :
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtvaliditydate" runat="server" 
                                        CssClass="txt_bx" ></asp:TextBox>
                                        <asp:CalendarExtender runat ="server" ID ="calvaliditydate" TargetControlID ="txtvaliditydate">
                                        </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RFVtxtvaliditydate" runat="server" ErrorMessage="Validity date is required"
                                    SetFocusOnError="true" ControlToValidate="txtvaliditydate" Display="Dynamic" ValidationGroup="2"></asp:RequiredFieldValidator>
                                 </td>
                               </tr>

		     <tr valign="top" align="left" id = "tr_proposal" runat ="server"  style='display:none;'>
                                <td valign="top" align="left" class="updateInnerColumn" >
                                  PROPOSAL DATE:
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtpropasaldate" runat="server" 
                                        CssClass="txt_bx" ></asp:TextBox>
                                         <%-- <asp:CalendarExtender runat ="server" ID ="calProposal" TargetControlID ="txtpropasaldate">
                                        </asp:CalendarExtender>
                                          <asp:RangeValidator runat="server" id="rngDate" controltovalidate="txtpropasaldate" type="Date" minimumvalue="04/01/2014" maximumvalue="01/01/3018" errormessage="Please enter a valid date within 2014!" />
                                        <asp:RequiredFieldValidator ID="RFVtxtpropasaldate" runat="server" ErrorMessage="Proposal date is required"
                                    SetFocusOnError="true" ControlToValidate="txtpropasaldate" Display="Dynamic" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                 </td>
                               </tr>

		   <tr valign="top" align="left">
                                <td valign="top" align="left" class="updateInnerColumn" >
                                 DATE OF ISSUE:
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtdateofissue" runat="server" 
                                        CssClass="txt_bx" ></asp:TextBox>
                                         <asp:CalendarExtender runat ="server" ID ="caldateofissue" TargetControlID ="txtdateofissue">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RFVtxtdateofissue" runat="server" ErrorMessage="Date of issue is required"
                                    SetFocusOnError="true" ControlToValidate="txtdateofissue" Display="Dynamic" ValidationGroup="2"></asp:RequiredFieldValidator>
                                 </td>
                               </tr>		
                            
                            
                            <%-- <tr valign="top" align="left">
                                <td valign="top" align="left" class="updateInnerColumn" >
                                Certificate Number:
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtCertificateNumber" runat="server" 
                                        CssClass="txt_bx" ></asp:TextBox>
                                         
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Certifiate Number is Required"
                                    SetFocusOnError="true" ControlToValidate="txtCertificateNumber" Display="Dynamic" ValidationGroup="2"></asp:RequiredFieldValidator>
                                 </td>
                               </tr>		--%>
                            
                            
                            	     
                                 <tr valign="top" align="left">
                               <td valign="top" align="left" class="updateInnerColumn">
                                <%--   Comments:--%>
                                Inspection agency<br /> comments / Note<br />(Optional):
                               </td>
                               <td valign="top" align="left" class="updateInnerColumn">
                                 <asp:TextBox ID="txt_bx_comments" runat="server" CssClass="txt_bx" TextMode="MultiLine"  MaxLength="150" Height="100px">
                                 
                                 </asp:TextBox>
                               </td>
                               </tr>		  

                               <tr valign="top" align="left">
                                <td align="left" valign="top" >
                                </td>
                               </tr>
                               <tr valign="top" align="center" >
                                <td valign="top" align="left" >
                                </td>
                                <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                    <asp:Button ID="btnSubmit_print" runat="server" Text="Submit" 
                                        CssClass="btnCls" ValidationGroup="2" 
                                       onclick="btnSubmit_print_Click" />
                                        &nbsp;&nbsp;
                                        <input id="Button1" type="button" causesvalidation="false" value="Reset"class="btnCls" onclick="return FunResetcertificate();" />
                                </td>      
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
         
           </center>
        </asp:Panel>
    </asp:panel>


    <asp:modalpopupextender id="modprintpopup" runat="server" popupcontrolid="pnlOutprint"
        targetcontrolid="lnkprint" cancelcontrolid="btnClose" backgroundcssclass="modalBackground">
    </asp:modalpopupextender>
    <asp:roundedcornersextender id="RoundedCornersExtender2" targetcontrolid="pnlInnerprint"
        runat="server" bordercolor="#22518F" radius="10" corners="All">
    </asp:roundedcornersextender>
    <asp:linkbutton id="lnkprint" runat="server" style="display: none;"></asp:linkbutton>
    <asp:linkbutton id="lnkDuplicatePrint" runat="server" style="display: none;"></asp:linkbutton>

    <!--Duplicate code-->
    <asp:Panel id="panelDuplicate" runat="server" cssclass="outerPopup">
         <asp:Panel ID="panelInnerDuplicate" runat="server" Height="500px" Width="400px" CssClass="innerPopup">
            <center>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr valign="top" align="left" style="width:100%;">
                    <td valign="top" align="left" style="width:100%;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr valign="top" align="left" style="width:100%">
                                <td valign="top" align="right" colspan="2" style="padding-bottom:10px; padding-right:5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr align="left" valign="top">
                                                      <%--  <td align="center" valign="top" class="updateInnerHeader">
                                                            <asp:Label ID="Label2" runat="server" CssClass="header"></asp:Label>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                                        </td>--%>
                                                        <td align="right" valign="top" style="width: 20px;">
                                                            <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImageButton1" runat="server"
                                                                ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                    
                                </td>
                            </tr>
                            
                            
                            
                              <tr valign="top" align="left">
                               <td valign="top" align="left" class="updateInnerColumn" >
                                 DATE OF FIR:
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtFIRDate" runat="server" 
                                        CssClass="txt_bx" ></asp:TextBox>
                                         <asp:CalendarExtender Format="MM/dd/yyyy"  runat ="server"  ID ="CalendarExtender1" TargetControlID ="txtFIRDate">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date of FIR is required"
                                    SetFocusOnError="true" ControlToValidate="txtFIRDate" Display="Dynamic" ValidationGroup="11"></asp:RequiredFieldValidator>

                                 </td>
                               </tr>

                             <tr valign="top" align="left">
                               <td valign="top" align="left" class="updateInnerColumn" >
                                Certificate Number:
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtCertificateNumber_Duplicate" runat="server" 
                                        CssClass="txt_bx" ></asp:TextBox>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Certificate Number is required"
                                    SetFocusOnError="true" ControlToValidate="txtCertificateNumber_Duplicate" Display="Dynamic" ValidationGroup="11"></asp:RequiredFieldValidator>

                                 </td>
                               </tr>
                            

                               <tr valign="top" align="left">
                                
                                <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                     
                                        <asp:Panel ID="Panel1" runat="server" >
        <table width="80%" >
        <%--<tr>
        <td  colspan="2" class="style1">   
        <b>Option for Inspection Fees </b>
        
        </td>
        </tr>--%>
        <tr>
          <td colspan="2">
           <%-- <asp:RadioButtonList ID="rdl_inspectionmode_payment" runat="server" RepeatDirection="Horizontal" 
                AutoPostBack="True" onselectedindexchanged="rbl_selectindexchanged_InspectionAgency">
           
            </asp:RadioButtonList>--%>
          </td>
        </tr>

   <tr>
                      <td align="left" valign="top" style="padding-left:10px; padding-right:10px;  margin-top:0px;"  colspan="3">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                          <tr>
                             <td valign="top">
                               <asp:Panel ID="PanelChequeDD" runat="server" Visible="false">
                                 <table width="80%">




                                     
        <tr >
        <td valign="top" >
        <b>Cheque / DD No.</b>
        </td>
        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_chqddNo_inspection" runat="server" CssClass="txt_bx" 
                                 MaxLength="50"></asp:TextBox>
                                
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                ErrorMessage="Please Enter Cheque or DD Number"
                                                ControlToValidate="txt_chqddNo_inspection"
                                                SetFocusOnError="true"
                                                ValidationGroup="11"
                                            ></asp:RequiredFieldValidator>
                                      
                                            
                           
                        </td>
        </tr>
        <tr>
        <td  valign="top">
        <b>Bank Name</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_BankNam_inspection" runat="server" CssClass="txt_bx" 
                                 MaxLength="50"></asp:TextBox>
                                
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                ErrorMessage="Please Enter Bank Name"
                                                ControlToValidate="txt_BankNam_inspection"
                                                SetFocusOnError="true"
                                                ValidationGroup="11"
                                            ></asp:RequiredFieldValidator>
                                             
                                        
                           
                        </td>
        </tr>
       
       <tr>
        <td  valign="top">
        <b>Bank Branch</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_BankBranch_inspection" runat="server" CssClass="txt_bx" 
                                  MaxLength="50"></asp:TextBox>
                                
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                ErrorMessage="Please Enter Bank Branch"
                                                ControlToValidate="txt_BankBranch_inspection"
                                                SetFocusOnError="true"
                                               ValidationGroup="11"
                                            ></asp:RequiredFieldValidator>
                                   
                        </td>
        </tr>
	  
      <%--  <tr>
        <td valign="top">
        <b>Payable At</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_PayableAt_inspection" runat="server" CssClass="txt_bx" 
                                  MaxLength="50"></asp:TextBox>
                                
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                ErrorMessage="Please Enter Payable At"
                                                ControlToValidate="txt_PayableAt_inspection"
                                                SetFocusOnError="true"
                                                ValidationGroup="11"
                                            ></asp:RequiredFieldValidator>
                                            
                                         
                           
                        </td>
        </tr>--%>
        
         <tr>
        <td valign="top">
        <b>Cheque / DD Amount (Including 18% GST)</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_DD_Inspection" runat="server" CssClass="txt_bx" 
                                  MaxLength="50" Text="1180" ReadOnly="true" ></asp:TextBox>
                           
                        </td>
        </tr>
  <tr>
        <td valign="top">
        <b>Cheque / DD Date (as MM/DD/YYYY)</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_chqddate_inspection" runat="server" CssClass="txt_bx" 
                                  MaxLength="50" ReadOnly="false" ></asp:TextBox>


             <asp:RequiredFieldValidator ID="RequiredFieldValidator9454" runat="server"
                                                ErrorMessage="Please Enter Cheque / DD Date"
                                                ControlToValidate="txt_chqddate_inspection"
                                                SetFocusOnError="true" Display="Dynamic"
                                               ValidationGroup="11"
                                            ></asp:RequiredFieldValidator>

               

                           
           <asp:CompareValidator ID="CValidatortxt_cheq1" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                    Display="Dynamic"  ControlToValidate="txt_chqddate_inspection" Operator="DataTypeCheck" Type="Date"
                  >
           </asp:CompareValidator>
            
                           
 
                        </td>
        </tr>
        

                                     </table>
                                   </asp:Panel></td></tr>
                            </table>
                          </td>
       </tr>
            



               <tr>
                      <td align="left" valign="top" style="padding-left:10px; padding-right:10px;  margin-top:0px;"  colspan="3">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                          <tr>
                             <td valign="top">
                               <asp:Panel ID="PanelInspectionOnlieRites" runat="server" Visible="false">
                                 <table width="80%">
                                    <tr>
                                      <td align="left" valign="top">
                                       <%-- <asp:RadioButtonList ID="rdbInspectionPaymentType_Online" runat="server" 
                                              RepeatDirection="Horizontal" AutoPostBack="true" onselectedindexchanged="rdbInspectionPaymentType_Online_selectindexchanged"
                                              >
                                          <asp:ListItem Value="1">NEFT</asp:ListItem>
                                           <asp:ListItem Value="2">RTGS</asp:ListItem>
                                        </asp:RadioButtonList>
                                          
                                        <asp:RequiredFieldValidator   
            ID="RequiredFieldValidatorrdbInspectionPaymentType_Online"  
            runat="server"  
            ControlToValidate="rdbInspectionPaymentType_Online"  
            ErrorMessage="Select Payment Option." 
             ValidationGroup="1" 
               SetFocusOnError="true"
            >  
        </asp:RequiredFieldValidator> --%>
             
                                      </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding-bottom:10px">

                                            <asp:Label ID="lblOnlineNorthernMsg" visible="false" Font-Bold="true" ForeColor="red"    runat="server" />
                                            <br /> 

                                          

                                             <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="true" ForeColor="Green" Font-Size="Large" Target="_blank"
 >
<asp:Label ID="lbl_OnlineMsg" runat="server" Font-Size="Small" ></asp:Label></asp:HyperLink>


                                        </td>

                                    </tr>
                                   
                                    <tr>


                                      
                                      
                                      <td align="left" valign="top">
                                        <asp:Panel ID="Panel3" runat="server">
                                        
          
                                          <table width="80%">
                                        
                                                           
                                      
                                            <tr>
                                            <td valign="top">
                                              <b>UTR NO</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txtUTRNO_InspectionAgency" runat="server" CssClass="txt_bx"  
                                 MaxLength="50" ReadOnly="false" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                ErrorMessage="Please Enter Utr Number"
                                                ControlToValidate="txtUTRNO_InspectionAgency"
                                                SetFocusOnError="true"
                                              ValidationGroup="11"
                                            ></asp:RequiredFieldValidator>
                                     
                                
                           
                        </td>
                                            </tr>
                                                  <tr>
                                            <td valign="top">
                                              <b>Bank Name</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txtBankName_InspectionAgency" runat="server" CssClass="txt_bx" 
                                 MaxLength="50" ReadOnly="false" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                ErrorMessage="Please Enter Bank Name"
                                                ControlToValidate="txtBankName_InspectionAgency"
                                                SetFocusOnError="true"
                                               ValidationGroup="11"
                                            ></asp:RequiredFieldValidator>
                                          
                        </td>
                                            </tr>
                                            
                                                    <tr>
                                            <td valign="top">
                                              <b>Branch Name</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txtBranchName_InspectionAgency" runat="server" CssClass="txt_bx" 
                                  MaxLength="50" ReadOnly="false" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                ErrorMessage="Please Enter Branch Name"
                                                ControlToValidate="txtBranchName_InspectionAgency"
                                                SetFocusOnError="true"
                                                 ValidationGroup="11"
                                            ></asp:RequiredFieldValidator>
                                         
                        </td>
                                            </tr>
                                                   <tr>
                                            <td valign="top">
                                              <b>Date (as MM/DD/YYYY)</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txtDate_InspectionAgency" runat="server" CssClass="txt_bx" 
                                 MaxLength="50" ></asp:TextBox>
                                
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                                ErrorMessage="Please Enter Date"
                                                ControlToValidate="txtDate_InspectionAgency"
                                                SetFocusOnError="true"
                                                 ValidationGroup="11"
                                            ></asp:RequiredFieldValidator>
                                            
                                 <asp:CompareValidator ID="CompareValidator112" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                    Display="Dynamic" ControlToValidate="txtDate_InspectionAgency" Operator="DataTypeCheck" Type="Date"
                                    ValidationGroup="1">
                                   </asp:CompareValidator>
                                
                           
                           
                        </td>
                                            </tr>
                                            
                                                      <tr>
                                            <td>
                                              <b>Amount (Including 18% GST)</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txtAmount_InspectionAgency" runat="server" CssClass="txt_bx" 
                                   MaxLength="50" ReadOnly="true" Text="1180" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ErrorMessage="Please Enter Amount"
                                                ControlToValidate="txtDate_InspectionAgency"
                                                SetFocusOnError="true"
                                                ValidationGroup="11"
                                            ></asp:RequiredFieldValidator>
                        </td>
                                            </tr>
                                            
                                        </table>
                                        </asp:Panel>
                                      
                                      </td>
                                      
                                      
                                      
                                      
                                    
                                    </tr>
                                 
                                 
                                 </table>
                               
                               </asp:Panel>
                             
                             </td>
                          </tr>
                        
                        </table>
                      
                      </td>
                    </tr>  
        
        </table>
        </asp:Panel>











                                 </td>
                               </tr>
                               
                               <tr valign="top" align="left">
                                <td align="left" valign="top" >
                                </td>
                               </tr>
                               <tr valign="top" align="center" >
                                <td valign="top" align="left" >
                                </td>
                                <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                    <asp:Button ID="Button2" runat="server" Text="Submit" ValidationGroup="11"
                                        CssClass="btnCls" /> <%--onclick="btnSubmit_duplicateprint_Click"--%>
                                        &nbsp;&nbsp;
                                        <input id="btnDReset" type="button" value="Reset"class="btnCls" onclick="return FunResetcertificate();" />

                                    <asp:Label ID="lblRedMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
             
           </center>
    </asp:Panel>
    </asp:Panel>

    <!--Duplicate code-->
 <asp:modalpopupextender id="modalDuplicate" runat="server" popupcontrolid="panelDuplicate" targetcontrolid="lnkDuplicatePrint"
        cancelcontrolid="btnClose" backgroundcssclass="modalBackground">
    </asp:modalpopupextender>
     <asp:roundedcornersextender id="RoundedCornersExtender3" targetcontrolid="panelInnerDuplicate"
        runat="server" bordercolor="#22518F" radius="10" corners="All">
    </asp:roundedcornersextender>
    

 
</asp:Content>

