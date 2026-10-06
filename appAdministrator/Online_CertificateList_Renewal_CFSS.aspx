<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Online_CertificateList_Renewal_CFSS.aspx.cs" Inherits="appAdministrator_Online_CertificateList_Renewal_CFSS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <style type="text/css">
       .clstblCell {
           vertical-align: top;
           text-align: justify;
           padding-right: 5px;
       }

       .clstblCellHead {
           vertical-align: top;
           font-weight: bold;
           text-transform: uppercase; 
       }
   </style>
   <script language="javascript" type="text/javascript">
       function FunResetTurnover() {
           document.getElementById("ctl00_ContentPlaceHolder1_txtTurnover").value = "";
           document.getElementById("ctl00_ContentPlaceHolder1_txtTurnover").focus();

       }
       function disableBackButton() {
           window.history.forward();
       }
       setTimeout("disableBackButton()", 0);



       function Btn_chekModified() {


           var id = 1;
           var i = 0;
           var count = 0
           var j;
           var unitId = $("#ctl00_ContentPlaceHolder1_hdn_UnitID").val();


           var CountRadio = $("#ctl00_ContentPlaceHolder1_hdn_unitID_Count").val();
           var nradio = 0;
           ok = false;

           if ($("input[@name=rdReg]:checked").is(':checked') == true) {

               isSelected = true;
               var Mystring = new Array();
               Mystring = unitId.split(',');

               // alert(Mystring);

               var newid = new Array();
               newid = Mystring;



               for (var j = 0; j < newid.length - 1; j++) {

                   if ($("input:radio[name=rd_" + newid[j] + "]").is(':checked') == true) {

                   }
                   else {
                       //alert(newid[j]);
                       alert("Please select registration both payment slab.");

                       return false;
                   }
               }
               // for loop

           }
           else {
               alert("Please select registration both payment slab.");

               return false;


           }




       }

       $(document).ready(function () {
           $("#progressbar").progressbar({ value: 90 });
       });

       function updateProgress() {
           var value = $("#progressbar").progressbar("option", "100");
           if (value < 100) {
               $("#progressbar").progressbar("value", value + 1);
           }
       }

   </script>

    <script language="javascript" type="text/javascript">
        var id = 0;
        function CaptchaRefresh(imgc, src) {

            imgc = "ctl00_ContentPlaceHolder1_" + imgc;

            //debugger;
            id++;
            document.getElementById(imgc).src = src + '?id=' + id;

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <table cellpadding="0" cellspacing="0" border="0" width="100%">
      <tr>
          <td align="left" class="headerAll"></td>
      </tr>
      <tr style="width: 95%;">
          <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
              <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">



                  <tr style="width: 100%;">



                      <td id="step2" style="width: 100%;"></td>
                  </tr>


              </table>
          </td>
      </tr>





      <tr>
          <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
              <table border="0" cellpadding="0" cellspacing="0" width="100%">

                  <tr>
                      <td align="left" colspan="4" class="headerAll" style="width: 170px;">Unit Renewal
                      </td>
                  </tr>
                  <tr align="left" valign="top">
                      <td colspan="4" align="left" valign="top" style="height: 10px; padding-top: 20px" class="leftHeading">Use Combination of  UDYAM Registration Number & PAN to Retrive Unit Details for Renewal
                      </td>
                  </tr>


                  <tr align="left" valign="top" width="100%" id="trMSMEDBSEARCH" runat="server">
                      <td colspan="4" style="padding: 10px">
                          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                              <ContentTemplate>


                                  <table border="1" cellpadding="2" cellspacing="2" width="100%">
                                      <tr>
                                          <td align="left" valign="top" class="leftHeading"><span class="MandatoryField">*</span>आपके पास उदयम पंजीकरण संख्या ह / <%--Do--%> You have UDYAM Registration Number</td>
                                          <td align="left" valign="top" class="leftHeading" colspan="3">
                                              <asp:RadioButtonList ID="rdbMsmeRegtype" Width="250px" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbMsmeRegtype_SelectedIndexChanged">
                                                   <%--<asp:ListItem Value="1" Text="UAM" ></asp:ListItem>--%>
                                                  <asp:ListItem Value="2" Text="Udyam Registration"></asp:ListItem>
                                              </asp:RadioButtonList>

                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic"
                                                  ErrorMessage="Please Select Unit have Udyami Registration Number" ControlToValidate="rdbMsmeRegtype" SetFocusOnError="true"
                                                  ValidationGroup="1"></asp:RequiredFieldValidator>

                                          </td>
                                      </tr>

                                      <tr id="divUAMPAN" runat="server" visible="false" style="padding-top: 25px">
                                          <td align="left" valign="top" class="leftHeading">


                                              <span class="MandatoryField"></span>
                                              <asp:Label ID="lblUamNameDisplay" runat="server" Text="उद्योग आधार संख्या /Udyog Aadhar Number:" Visible="false"></asp:Label>
                                              <asp:Label ID="lblUamiReigistrationNameDisplay" runat="server" Text="उदयम पंजीकरण संख्या/Udyam Registration Number:" Visible="false"></asp:Label>



                                              <div>
                                                  <div style="float: left;">
                                                      <asp:TextBox ID="txtUAM" runat="server" MaxLength="19" TabIndex="1"
                                                          ValidationGroup="validate" AutoPostBack="true" Style="text-transform: uppercase;"></asp:TextBox>

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
                                                      <%--<asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="update_adhar">
                                                              <ProgressTemplate>
                                                                  <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                              </ProgressTemplate>
                                                          </asp:UpdateProgress>--%>


                                                      <asp:Label ID="Label1" runat="server" CssClass="errMessage" Text="Aahar already exists !"
                                                          Visible="false"></asp:Label>
                                                  </div>

                                                  <%-- <div style="float: left;">
                                                          <asp:Label ID="lbl_aadharnumber" runat="server" CssClass="errMessage" Text="Aahar already exists !"
                                                              Visible="false"></asp:Label>

                                                      </div>--%>
                                                  <%-- </ContentTemplate>

                                          </asp:UpdatePanel>--%>

                                                  <%-- <asp:TextBox ID="txtUAM" runat="server" MaxLength="12" ValidationGroup="validate" />
                                                      <asp:RequiredFieldValidator ControlToValidate="txtUAM" ErrorMessage="Required" runat="server" ValidationGroup="validate" />--%>
                                          </td>
                                          <td align="left" valign="top" class="leftHeading">
                                              <span class="MandatoryField">*</span>पैन कार्ड संख्या/ PAN Card Number:   
                                                      <asp:TextBox ID="txtPan" runat="server" MaxLength="10" ValidationGroup="validate" Style="text-transform: uppercase;" />
                                              <asp:RequiredFieldValidator ControlToValidate="txtPan" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPan"
                                                  Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$" ValidationGroup="validate"></asp:RegularExpressionValidator>
                                          </td>
                                      </tr>


                                        <tr id="divUAMPAN1" runat="server" visible="false" style="padding-top: 25px">
                                          
                                           
                                          <td colspan="4" align="center" valign="top" class="leftHeading" style="padding-top: 15px;">
                                              <asp:Button ID="btnf" runat="server" Text="Fetch" CssClass="btnCls" OnClick="btnf_Click" ValidationGroup="validate" />
                                          </td>

                                      </tr>


                                      <tr>
                                          <td colspan="4" align="center" class="leftHeading">
                                              <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel3">

                                                  <ProgressTemplate>
                                                      <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                                      <span style="color: red; font-size: medium">Please wait while we are processing your request <%--Please wait, while your request is being processed--%></span>
                                                  </ProgressTemplate>

                                              </asp:UpdateProgress>
                                          </td>
                                      </tr>
                                      <tr>
                                          <td colspan="4">
                                              <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                          </td>
                                      </tr>

                                  </table>
                              </ContentTemplate>
                          </asp:UpdatePanel>
                      </td>
                  </tr>



                  <tr>
                      <td colspan="3">
                          <asp:GridView ID="gvCertificateDetails" runat="server" AutoGenerateColumns="false"
                              Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1"
                              OnRowDataBound="gvCertificateDetails_RowDataBound"
                              OnRowCommand="gvCertificateDetails_RowCommand" AllowPaging="True" AllowSorting="True" PageSize="10">
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
                                  <%--    <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>--%>
                                  <asp:TemplateField HeaderText="PAN No.">
                                      <ItemTemplate>
                                          <asp:Label ID="lbl_pan_text" runat="server" Text='<%#Eval("fGPtbl_Company_PAN") %>'></asp:Label>
                                          <br />
                                          <asp:Label ID="lbl_hscode_pncode" runat="server" Text='<%#Eval("HSCODENPC") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                                  <asp:TemplateField Visible="false" HeaderText="Print<br>Certificate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lbnPrint" runat="server" CommandName="print"
                                              CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_PrintAccess").ToString()+","+Eval("fGPtbl_CR_AcceptedByAdmin").ToString() %>' CausesValidation="false" ToolTip="Print Certificate">Print</asp:LinkButton>&nbsp;
                      <asp:LinkButton ID="lbnRequest" runat="server" CausesValidation="false" CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_Id")%>'
                          CommandName="request" ToolTip="Make request for print" Width="60px">Request</asp:LinkButton>&nbsp;
                      <asp:Literal ID="ltlPrint" runat="server"></asp:Literal>
                                          <asp:HiddenField ID="HiddenFieldCompanyId" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                                          <asp:HiddenField ID="HiddenFormPrintFlag" runat="server" Value='<%#Eval("doicheck").ToString()+","+Eval("RegistrationDate").ToString() %>' />
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField Visible="false" HeaderText="Draft<br>Print" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lbnDraftPrint" runat="server" CommandName="draftprint"
                                              CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="Draft print">Draft Print
                                          </asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField Visible="false" HeaderText="Duplicate<br>Print" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lbnDuplicatePrint" runat="server" CommandName="Duplicateprint"
                                              CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="Duplicate print">Duplicate Print
                                          </asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField Visible="false" HeaderText="Amendment" ItemStyle-HorizontalAlign="Center">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lnkamedment" runat="server" CommandName="Amendment" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>'
                                              CausesValidation="false" ToolTip="Amendment">Amendment</asp:LinkButton>&nbsp;
                 <asp:HiddenField ID="HiddenFieldCompanyIdAmendment" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                                          <asp:Literal ID="lt_amendment" Visible="false" runat="server" Text="Amendment process started"></asp:Literal>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField Visible="false">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lnkamdement_print" runat="server" CommandName="APrint" CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_PrintAccess").ToString()+","+Eval("fgtbl_Certificate_Amedment").ToString() %>'
                                              CausesValidation="false" ToolTip="Amendment Print">APrint</asp:LinkButton>
                                          <asp:LinkButton ID="lbnRequest1" runat="server" CausesValidation="false" CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_Id")%>'
                                              CommandName="request1" ToolTip="Make request for print" Width="60px" Visible="false">Request</asp:LinkButton>&nbsp;
                      <asp:Literal ID="ltlPrint1" runat="server"></asp:Literal>
                                          <asp:HiddenField ID="HiddenFieldCompanyId1" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                                          <asp:HiddenField ID="HiddenAmedment" runat="server" Value='<%#Eval("fgtbl_Certificate_Amedment")%>' />
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Renewal" ItemStyle-HorizontalAlign="Center" Visible="true">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lnkRenewal" runat="server" CommandName="Renewal" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>'
                                              CausesValidation="false" ToolTip="Renewal">Renewal</asp:LinkButton>&nbsp;
                 <asp:HiddenField ID="HFRenewalOpenFlag" runat="server" Value='<%#Eval("RenFlag") %>' />
                                          <asp:Literal ID="ltlrenmessage" runat="server" Text="Renewal process started"></asp:Literal>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                      </td>
                  </tr>
                  <tr>
                      <td colspan="3">
                          <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
                      </td>
                  </tr>
              </table>
          </td>
      </tr>
      <tr>
          <td align="left">&nbsp;
          </td>
      </tr>
      <tr>
          <td style="width: 100%; text-align: left;" class="MandatoryMessage">
              <span class="MandatoryField">*</span> Mandatory Fields
          </td>
      </tr>
  </table>
</asp:Content>

