<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminOnline.master" AutoEventWireup="true" CodeFile="Online_Unit_Ren_Amnd_Step2.aspx.cs" Inherits="appAdministrator_Online_Unit_Ren_Amnd_Step2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/appMaster/Admin.master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<%--<script type="text/javascript" language="javascript">
// function disableBackButton() {
//            window.history.forward();
//        }
//        setTimeout("disableBackButton()", 0);
        $(document).ready(function()
         {
            $("#progressbar").progressbar({ value: 30 });
            });
             
        function updateProgress() {            
            var value = $("#progressbar").progressbar("option", "100");
            if (value < 100) {
                $("#progressbar").progressbar("value", value + 1);                
            }
        }
         function UpdateProgressBar_DroDown()
            {
               $("#progressbar").progressbar({ value: 30 });
            }
               
    </script>--%>
 
 <style  type="text/css">
   .text21
   {
   }
 </style>
 <script type="text/javascript" language="javascript">

     $(document).ready(function() {

     
         
         var tblObj = document.getElementById("<%=grdSSI.ClientID %>");
         var txtObjlst = document.getElementById("<%=grdSSI.ClientID %>").getElementsByTagName("INPUT");
         var txtCount;
         txtCount = 0;
      //   alert(txtObjlst.length);

         for (var i = 0; i < txtObjlst.length; i++) {
             if (txtObjlst[i].type == "text") 
             {
                 //Do your validations
                // alert(txtObjlst[i].value);
                 if (txtObjlst[i].value != "" && txtObjlst[i].value != 0) 
                 {
                     txtObjlst[i].disabled = true;
                 }
                 else 
                 {
                     txtObjlst[i].disabled = false;
                 }

             }
         }

         var dateofincorporation = document.getElementById("<%=txt_3a.ClientID %>").value;
         var dateofproductbusiness = document.getElementById("<%=txt_3b.ClientID %>").value;
      //   alert(name);
         if (dateofincorporation != "")
             document.getElementById("<%=txt_3a.ClientID %>").disabled = true;
         else
             document.getElementById("<%=txt_3a.ClientID %>").disabled = false;

         if (dateofproductbusiness != "")
             document.getElementById("<%=txt_3b.ClientID %>").disabled = true;
         else
             document.getElementById("<%=txt_3b.ClientID %>").disabled = false;
            

         $("#progressbar").progressbar({ value: 30 });
     });



</script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">
                Unit Amendment - Step 2
            </td>
        </tr>
        <tr style="width:95%;">
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                   <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
                   <tr style="width:100%;">
                   <td style="width:100%;">
                     <table cellpadding="0" cellspacing="0" border="1" style="width:100%;">
                       <tr>
                           <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-1</b></td>
                           <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-2</b></td>
                           <td style="width:15%;padding-left: 10px; padding-right: 10px;">Step-3</td>
                           <td style="width:15%;padding-left: 10px; padding-right: 10px;">Step-4</td>
                           <td style="width:15%;padding-left: 10px; padding-right: 10px;">Step-5</td>
                           <td style="width:15%;padding-left: 10px; padding-right: 10px;">Step-6</td>
                           <td style="width:10%;padding-left: 10px; padding-right: 10px;">Step-7</td>
                       </tr>
                     
                     </table>
                   
                   </td>
                   </tr>
                         
                   
                     <tr style="width:100%;">
                    
                     
                       
                         <td id="step2" style="width:100%;">
                      
                         <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
                         <tr>
                         <td id="progressbar" align="left" ></td>
                         </tr>
                         
                         
                         
                         </table>
                       
                       
                     </td>
                     </tr>
                       
                   
                   </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="padding-left:10px; padding-right:10px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" class="rowStep">
                            STEP 2 of 7
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (2) लघु उद्योग पंजीकरण संख्या एवं तारीख/ (कृपया प्रमाणपत्र की नोटरी द्वारा प्रमाणित लगाएं)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EM Registration No. & Date/ (Please attach a Notarized copy of certificate)
                        </td>
                        <%--Uttam Kumar --%>
                    </tr>
                    <tr>
                    <td colspan="3">
                        <asp:GridView ID="grdSSI" runat="server" 
                            AutoGenerateColumns="False" >
                            <HeaderStyle CssClass="GrdviewHeader" Height="30" />
                                        <RowStyle CssClass="GrdviewRowOdd" />
                                        <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                    <Columns >
                    <asp:BoundField  DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory Address" ItemStyle-Width="400px">                    
                    
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="EM Part-II No. / Udyog Aadhar Memorandum No." >
                    <ItemTemplate  >
                    <asp:TextBox ID="txt_21" CssClass ="text21" runat="server" Text='<%# Bind("fGPtbl_CUnit_SSIRegistrationNo") %>' ></asp:TextBox>
                    <asp:HiddenField ID="hdnunit" runat="server" Value='<%# Bind("fGPtbl_CUnit_Id") %>' />
                    </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField  HeaderText="EM Part-II Date/ Udyog Aadhar Memorandum Date">
                    <ItemTemplate  >
                    <asp:TextBox ID="txt_22"  CssClass ="text21" runat="server" Text='<%# Bind("fGPtbl_CUnit_SSIRegistrationDate") %>'></asp:TextBox>
                    
                    
                    <asp:RequiredFieldValidator ID="RFValidatortxt_22" runat="server" ErrorMessage="Date is required"
                            Display="Dynamic" ControlToValidate="txt_22" ValidationGroup="1"></asp:RequiredFieldValidator>
                        <%--<asp:CompareValidator ID="CValidatortxt_22" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                            Display="Dynamic" ControlToValidate="txt_22" Operator="DataTypeCheck" Type="Date"
                            ValidationGroup="1"></asp:CompareValidator>--%>
                       
                        <asp:ValidatorCalloutExtender ID="VCExtendertxt22" runat="server" TargetControlID="RFValidatortxt_22"
                            PopupPosition="TopLeft">
                        </asp:ValidatorCalloutExtender>
                    
                    <asp:CalendarExtender ID="CEtxt_22" runat="server" TargetControlID="txt_22">
                            </asp:CalendarExtender>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    </Columns>
                    </asp:GridView>
                    </td>
                    
                    </tr>
                  <tr>
                  <td style="height:10px;"></td>
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
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_3a" runat="server" CssClass="txt_bx" TabIndex="3"  
                                ValidationGroup="1" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                ErrorMessage="Please Enter Date of Incorporation" ControlToValidate="txt_3a"
                                SetFocusOnError="true" ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                PopupPosition="BottomLeft" TargetControlID="RequiredFieldValidator3"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CalendarExtender ID="CEtxt_3a" runat="server" TargetControlID="txt_3a">
                            </asp:CalendarExtender>
                            
                           <%-- <asp:CompareValidator ID="CVtxt_3a" runat="server" Display="Dynamic"
                                SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Please enter date in MM/DD/YYYY format"
                                ControlToValidate="txt_3a" Operator="DataTypeCheck" Type="Date">
                                </asp:CompareValidator>--%>
                           
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (3.b)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>उत्पादन/ कारोबार आरम्भ करने की तारीख<br />Date of commencement of production/business:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3b" runat="server" CssClass="txt_bx" MaxLength="50" 
                                TabIndex="4" ValidationGroup="1"  ></asp:TextBox>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                                ErrorMessage="Please Enter Date of commencement of production/business"
                                ControlToValidate="txt_3b" SetFocusOnError="true" ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator4"
                            ></asp:ValidatorCalloutExtender>--%>
                            
                             <asp:CalendarExtender ID="CEtxt_3b" runat="server" TargetControlID="txt_3b">
                            </asp:CalendarExtender>
                            
       <%--                     <asp:CompareValidator ID="CVtxt_3b" runat="server" Display="Dynamic"
                                SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Please enter date in MM/DD/YYYY format"
                                ControlToValidate="txt_3b" Operator="DataTypeCheck" Type="Date">
                                </asp:CompareValidator>--%>
                            
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
                            <asp:RadioButtonList ID="rd3c" runat="server" RepeatLayout="Table" 
                                RepeatDirection="Vertical" TabIndex="5">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                          
                            (3.c.a) स्वामी/साझेदारों/निदेशकों/कर्ता जिनका फर्म से हित है, के नाम, पते व टेलीफोन सं.<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Names, addresses and telephone Nos. of Proprietor/Partners/Directors/Karta having interest in the firm
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                        <asp:HiddenField ID="hdnfDirectorId1" runat="server" />
                            <asp:HiddenField ID="hdnfDirectorId2" runat="server" />
                            <asp:HiddenField ID="hdnfDirectorId3" runat="server" />
                            <asp:HiddenField ID="hdnfDirectorId4" runat="server" />
                            <asp:HiddenField ID="hdnfDirectorId5" runat="server" />
                            (3.c.a.1)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>नाम/ Name:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca1Name" runat="server" CssClass="txt_bx" MaxLength="500" 
                                TabIndex="6" ValidationGroup="1" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                ErrorMessage="Please Enter DIRECTOR Name"
                                ControlToValidate="txt_3ca1Name"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator5"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>पता/ Address:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_3ca1Address" runat="server" CssClass="txt_bx" TabIndex="7" 
                                ValidationGroup="1" TextMode="MultiLine" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="None"
                                ErrorMessage="Please Enter DIRECTOR Address"
                                ControlToValidate="txt_3ca1Address"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator9"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator1" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Director address max lenght is 5000 characters"
                                ControlToValidate="txt_3ca1Address"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator1"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>पिन कोड/ Pin Code:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca1Pin" runat="server" CssClass="txt_bx" MaxLength="10" 
                                TabIndex="8" ValidationGroup="1" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="None"
                                ErrorMessage="Please Enter DIRECTOR Pin Code"
                                ControlToValidate="txt_3ca1Pin"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txt_3ca1Pin" FilterType="Numbers" runat="server">
                            </asp:FilteredTextBoxExtender>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator10"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>राज्य/ State:
                        </td>
                        <td align="left" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div style="float:left;">
                                            <asp:DropDownList ID="ddlState_3ca1" runat="server" CssClass="clsDDL" 
                                                AutoPostBack="True" TabIndex="9" 
                                                ValidationGroup="1" 
                                                onselectedindexchanged="ddlState_3ca1_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="None"
                                                ErrorMessage="Please Select State"
                                                ControlToValidate="ddlState_3ca1"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator11"
                                            ></asp:ValidatorCalloutExtender>
                                        </div>
                                        <div style="float:left;">
                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                <ProgressTemplate>
                                                    <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" /> </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>ज़िला/शहर/District/ City:
                        </td>
                        <td align="left" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlCity_3ca1" runat="server" CssClass="clsDDL" 
                                        TabIndex="10" ValidationGroup="1"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="None"
                                        ErrorMessage="Please Select City"
                                        ControlToValidate="ddlCity_3ca1"
                                        SetFocusOnError="true"
                                        ValidationGroup="1"
                                    ></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                        PopupPosition="BottomLeft"
                                        TargetControlID="RequiredFieldValidator12"
                                    ></asp:ValidatorCalloutExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>फोन/ Telephone/Mobile  :
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca1Tele" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox>
                                <br /><span style="color:#FF0000; padding-top:5px;">(eg: 011-1234567, 9871234567)</span>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="None"
                                ErrorMessage="Please Enter DIRECTOR Telephone"
                                ControlToValidate="txt_3ca1Tele"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator13"
                            ></asp:ValidatorCalloutExtender>--%>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_step2_tel" runat="server" FilterMode="ValidChars" FilterType="Custom" 
                            TargetControlID="txt_3ca1Tele" ValidChars="1234567890,() .-+P" >
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Email</td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca1Email" runat="server" CssClass="txt_bx"  
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Length of Experience (in years)</td>
                        <td align="left" valign="top">
                           <asp:TextBox ID="txt_3ca1Exp" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Adhar No. (optional)</td>
                        <td align="left" valign="top">
                          <asp:TextBox ID="txt_3ca1Adhar" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                     <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            हिस्सेदारी/ Shareholding:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca1share" runat="server" CssClass="txt_bx" ></asp:TextBox>
                          
                        </td>
                    </tr>
                    
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (3.c.a.2)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            नाम/ Name:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca2Name" runat="server" CssClass="txt_bx" MaxLength="500" 
                                TabIndex="12" ValidationGroup="1" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पता/ Address:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_3ca2Address" runat="server" CssClass="txt_bx" TabIndex="13" 
                                ValidationGroup="1" TextMode="MultiLine" ></asp:TextBox>
                            
                            <asp:CustomValidator ID="CustomValidator2" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Director address max lenght is 5000 characters"
                                ControlToValidate="txt_3ca2Address"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator2"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पिन कोड/ Pin Code:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca2Pin" runat="server" CssClass="txt_bx" MaxLength="10" 
                                TabIndex="14" ValidationGroup="1" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            राज्य/ State:
                        </td>
                        <td align="left" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div style="float:left;">
                                            <asp:DropDownList ID="ddlState_3ca2" runat="server" CssClass="clsDDL" 
                                                AutoPostBack="True" TabIndex="15" 
                                                ValidationGroup="1" 
                                                onselectedindexchanged="ddlState_3ca1_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div style="float:left;">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" /> </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                           ज़िला/शहर/District/ City:
                        </td>
                        <td align="left" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                              <ContentTemplate>
                                <asp:DropDownList ID="ddlCity_3ca2" runat="server" CssClass="clsDDL" 
                                TabIndex="16" ValidationGroup="1"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please provide city name"
                                  ValidationGroup="1" SetFocusOnError="true" ControlToValidate="ddlCity_3ca2" Display="None" ></asp:RequiredFieldValidator>
                                  <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator1">
                                  </asp:ValidatorCalloutExtender>
                              </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            फोन/ Telephone/Mobile:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca2Tele" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="17" ValidationGroup="1" ></asp:TextBox>
                                 <br /><span style="color:#FF0000; padding-top:5px;">(eg: 011-1234567, 9871234567)</span>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Email</td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca2Email" runat="server" CssClass="txt_bx" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Length of Experience (in years)</td>
                        <td align="left" valign="top">
                           <asp:TextBox ID="txt_3ca2Exp" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Adhar No. (optional)</td>
                        <td align="left" valign="top">
                          <asp:TextBox ID="txt_3ca2Adhar" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                     <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            हिस्सेदारी/ Shareholding:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca2share" runat="server" CssClass="txt_bx" ></asp:TextBox>
                          
                        </td>
                    </tr>
                    
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (3.c.a.3)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            नाम/ Name:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca3Name" runat="server" CssClass="txt_bx" MaxLength="500" 
                                TabIndex="18" ValidationGroup="1" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पता/ Address:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_3ca3Address" runat="server" CssClass="txt_bx" TabIndex="19" 
                                ValidationGroup="1" TextMode="MultiLine" ></asp:TextBox>
                            
                            <asp:CustomValidator ID="CustomValidator3" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Director address max lenght is 5000 characters"
                                ControlToValidate="txt_3ca3Address"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator3"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पिन कोड/ Pin Code:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca3Pin" runat="server" CssClass="txt_bx" MaxLength="10" 
                                TabIndex="20" ValidationGroup="1" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            राज्य/ State:
                        </td>
                        <td align="left" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div style="float:left;">
                                            <asp:DropDownList ID="ddlState_3ca3" runat="server" CssClass="clsDDL" 
                                                AutoPostBack="True" TabIndex="21" 
                                                ValidationGroup="1" 
                                                onselectedindexchanged="ddlState_3ca1_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div style="float:left;">
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                <ProgressTemplate>
                                                    <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" /> </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                           ज़िला/शहर/District/ City:
                        </td>
                        <td align="left" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                <asp:DropDownList ID="ddlCity_3ca3" runat="server" CssClass="clsDDL" 
                                TabIndex="22" ValidationGroup="1"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please provide city name" ValidationGroup="1"
                                    SetFocusOnError="true" ControlToValidate="ddlCity_3ca3" Display="None"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator2">
                                    </asp:ValidatorCalloutExtender>
                                </ContentTemplate>
                             </asp:UpdatePanel>
                         </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            फोन/ Telephone/Mobile:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca3Tele" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="23" ValidationGroup="1" ></asp:TextBox>
                                <br /><span style="color:#FF0000; padding-top:5px;">(eg: 011-1234567, 9871234567)</span>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Email</td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca3Email" runat="server" CssClass="txt_bx"  
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Length of Experience (in years)</td>
                        <td align="left" valign="top">
                           <asp:TextBox ID="txt_3ca3Exp" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Adhar No. (optional)</td>
                        <td align="left" valign="top">
                          <asp:TextBox ID="txt_3ca3Adhar" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            हिस्सेदारी/ Shareholding:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca3share" runat="server" CssClass="txt_bx" ></asp:TextBox>
                          
                        </td>
                    </tr>
                    
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (3.c.a.4)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            नाम/ Name:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca4Name" runat="server" CssClass="txt_bx" MaxLength="500" 
                                TabIndex="24" ValidationGroup="1" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पता/ Address:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_3ca4Address" runat="server" CssClass="txt_bx" TabIndex="25" 
                                ValidationGroup="1" TextMode="MultiLine" ></asp:TextBox>
                            
                            <asp:CustomValidator ID="CustomValidator4" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Director address max lenght is 5000 characters"
                                ControlToValidate="txt_3ca4Address"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator4"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पिन कोड/ Pin Code:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca4Pin" runat="server" CssClass="txt_bx" MaxLength="10" 
                                TabIndex="26" ValidationGroup="1" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            राज्य/ State:
                        </td>
                        <td align="left" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div style="float:left;">
                                            <asp:DropDownList ID="ddlState_3ca4" runat="server" CssClass="clsDDL" 
                                                AutoPostBack="True" TabIndex="27" 
                                                ValidationGroup="1" 
                                                onselectedindexchanged="ddlState_3ca1_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div style="float:left;">
                                            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
                                                <ProgressTemplate>
                                                    <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" /> </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                           ज़िला/शहर/District/ City:
                        </td>
                        <td align="left" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                              <ContentTemplate>
                                <asp:DropDownList ID="ddlCity_3ca4" runat="server" CssClass="clsDDL" 
                                TabIndex="28" ValidationGroup="1"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please provide city name" Display="None"
                                  ValidationGroup="1" ControlToValidate="ddlCity_3ca4" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                  <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RequiredFieldValidator7">
                                  </asp:ValidatorCalloutExtender>
                              </ContentTemplate>
                          </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            फोन/ Telephone/Mobile:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca4Tele" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="29" ValidationGroup="1" ></asp:TextBox>
                                <br /><span style="color:#FF0000; padding-top:5px;">(eg: 011-1234567, 9871234567)</span>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Email</td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca4Email" runat="server" CssClass="txt_bx"
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Length of Experience (in years)</td>
                        <td align="left" valign="top">
                           <asp:TextBox ID="txt_3ca4Exp" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Adhar No. (optional)</td>
                        <td align="left" valign="top">
                          <asp:TextBox ID="txt_3ca4Adhar" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            हिस्सेदारी/ Shareholding:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca4share" runat="server" CssClass="txt_bx" ></asp:TextBox>
                          
                        </td>
                    </tr>
                    
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (3.c.a.5)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            नाम/ Name:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca5Name" runat="server" CssClass="txt_bx" MaxLength="500" 
                                TabIndex="30" ValidationGroup="1" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पता/ Address:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_3ca5Address" runat="server" CssClass="txt_bx" TabIndex="31" 
                                ValidationGroup="1" TextMode="MultiLine" ></asp:TextBox>
                            
                            <asp:CustomValidator ID="CustomValidator5" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Director address max lenght is 5000 characters"
                                ControlToValidate="txt_3ca5Address"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator5"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पिन कोड/ Pin Code:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca5Pin" runat="server" CssClass="txt_bx" MaxLength="10" 
                                TabIndex="32" ValidationGroup="1" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            राज्य/ State:
                        </td>
                        <td align="left" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div style="float:left;">
                                            <asp:DropDownList ID="ddlState_3ca5" runat="server" CssClass="clsDDL" 
                                                AutoPostBack="True" TabIndex="33" 
                                                ValidationGroup="1" 
                                                onselectedindexchanged="ddlState_3ca1_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div style="float:left;">
                                            <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel5">
                                                <ProgressTemplate>
                                                    <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" /> </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            ज़िला/शहर/District/ City:
                        </td>
                        <td align="left" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                              <ContentTemplate>
                                <asp:DropDownList ID="ddlCity_3ca5" runat="server" CssClass="clsDDL" 
                                TabIndex="34" ValidationGroup="1"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please provide city name"
                                  ValidationGroup="1" ControlToValidate="ddlCity_3ca5" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                  <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RequiredFieldValidator8">
                                  </asp:ValidatorCalloutExtender>
                             </ContentTemplate>
                          </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            फोन/ Telephone/Mobile:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca5Tele" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="35" ValidationGroup="1" ></asp:TextBox>
                                <br /><span style="color:#FF0000; padding-top:5px;">(eg: 011-1234567, 9871234567)</span>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Email</td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca5Email" runat="server" CssClass="txt_bx" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Length of Experience (in years)</td>
                        <td align="left" valign="top">
                           <asp:TextBox ID="txt_3ca5Exp" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                            &nbsp;</td>
                        <td align="left" valign="top" class="leftHeading">
                            Adhar No. (optional)</td>
                        <td align="left" valign="top">
                          <asp:TextBox ID="txt_3ca5Adhar" runat="server" CssClass="txt_bx" MaxLength="12" 
                                TabIndex="11" ValidationGroup="1" ></asp:TextBox></td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            हिस्सेदारी/ Shareholding:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3ca5share" runat="server" CssClass="txt_bx" ></asp:TextBox>
                          
                        </td>
                    </tr>
                    
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading" >
                            (3.c.b)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या आप किसी बड़े उद्योग या रा.ल.उ.नि./ डी.जी.एस. एंड डी से पंजीकृत इकाइयों के सांझेदार/ निदेशक हैं यदि हां तो उनका ब्यौरा दीजिए<br />Whether you are a Partner/Director of any large industries or units registered with NSIC/DGS&D. If so, give details:
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_3cb" runat="server" CssClass="txt_bx" MaxLength="5000" 
                                TextMode="MultiLine" Height="80"  Text="N.A"
                                TabIndex="36" ValidationGroup="1"></asp:TextBox>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None"
                                ErrorMessage="Please Enter &quot;Whether you are a Partner/Director of any large industries or units registered with NSIC/DGS&amp;D. If so, give details&quot;"
                                ControlToValidate="txt_3cb"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator6"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator6" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max lenght is 5000 characters"
                                ControlToValidate="txt_3cb"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator6"
                            ></asp:ValidatorCalloutExtender>--%>
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
                        <asp:Button ID="btn_back" runat="server" Text="BACK" CssClass="btnCls" 
                                CausesValidation="false" onclick="btn_back_Click"  />
                        
                        
                      <%--   <input type="button" id="btnback" class="btnCls" value="Back"   onclick ="Javascript:window.history.back(); return false;"/>--%>
                        </td>
                        
                           <td>  
                            <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" 
                                CssClass="btnCls" TabIndex="37"
                                ValidationGroup="1" Width="150px" onclick="btnSubmit_Click" />
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
            <td style="width:100%; text-align:left;" class="MandatoryMessage">
                <span class="MandatoryField">*</span> Mandatory Fields
            </td>
        </tr>
        
                </table>

</asp:Content>


