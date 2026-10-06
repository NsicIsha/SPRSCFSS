<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminOnline.master" AutoEventWireup="true" CodeFile="Online_UnitRenewalStep3.aspx.cs" Inherits="appAdministrator_Online_UnitRenewalStep3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">
                 Unit Renewal - Step 3
            </td>
        </tr>
         <tr style="width:95%;">
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                   <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
                   <tr style="width:100%;">
                   <td style="width:100%;">
                     <table cellpadding="0" cellspacing="0" border="1" style="width:100%;">
                       <tr>
                       <td style="width:15%; padding-left: 10px; padding-right: 10px;"><b>Step-1</b></td>
                        <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-2</b></td>
                         <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-3</b></td>
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
                            STEP 3 of 7
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (3) आवेदक फर्म की स्थिति<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Status of applicant MSE (continue from step 2)
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="style1">
                            (3.d)
                        </td>
                        <td align="left" valign="top" class="style1">
                            <span class="MandatoryField">*</span>क्या आपकी फर्म इनके अंतर्गत पंजीकृत है<br />Whether your MSE is registered under:
                        </td>
                        <td align="left" valign="top" class="style7">
                            <asp:RadioButtonList ID="rd3d" runat="server" RepeatLayout="Table" 
                                RepeatDirection="Vertical" TabIndex="1">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                 
                    <%--  <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (3.e)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>बिक्री कर पंजीकरण सं.<br />Sales Tax - Central  Sales Tax No, VAT Registration No. and Service Tax Registration No.:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3l" runat="server" CssClass="txt_bx200x80" TabIndex="9" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3l"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator7"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator8" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3l"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator8"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>--%>

                   <%-- <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (3.f)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या आपकी फर्म या उसके किसी निदेशक/ सांझेदार को किसी समय किसी अदालत द्वारा सिविल/ फौजदारी/ राजस्व या आर्थिक अपराध के लिए दण्डित किया गया है यदि हां, तो अपराध और दंड का ब्यौरा दें |<br />Whether your MSE or any of its Directors/ Partners were prosecuted at any time for any offence under any Court for Civil/ Criminal/ Revenue or Economic Offence? If so, give particulars of offence and penalty:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3o" runat="server" CssClass="txt_bx200x80" TabIndex="12" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3o"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator11"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator11" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3o"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator11"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>--%>
                    <tr align="left" valign="top" >
                        <td align="left" valign="top" class="leftHeading">
                            (3.e)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या आपकी फर्म किसी भारतीय/विदेशी/बड़े उद्योग कम्पनी की सहायक है, तो पेरेंट/होल्डिंग कम्पनी का ब्यौरा दें |<br />If the firm is a subsidiary/Associate of an Indian/foreign/large scale company, give particulars of Parent/holding Company:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3e" runat="server" CssClass="txt_bx200x80" TabIndex="2" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3e"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator9"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator1" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3e"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator1"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" >
                        <td align="left" valign="top" class="leftHeading">
                            (3.f)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>Did you applied for registration with NSIC before for this firm or any other firm? If so, give details:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3m" runat="server" CssClass="txt_bx200x80" TabIndex="10" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3m"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator8"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator9" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3m"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator9"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>

                  

                    <tr align="left" valign="top" >
                        <td align="left" valign="top" class="leftHeading">
                            (3.g)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>Are you already availing or have availed any assistance/facilitation from NSIC under this or through some other firm? If so, please give details:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3i_new" runat="server" CssClass="txt_bx200x80" TabIndex="10" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3i_new"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender27" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator8"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator14" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3i_new"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator9"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>





                    <tr align="left" valign="top" style="display:none">
                        <td align="left" valign="top" class="leftHeading">
                            (3.h)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या भारत में उसी मूल कम्पनी के अन्य सहायक हैं तो ब्यौरा दें |<br />If there are other subsidiaries of the same parent company in India, give full particulars:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3f" runat="server" CssClass="txt_bx200x80" TabIndex="3" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3f"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator1"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator2" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3f"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator2"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none">
                        <td align="left" valign="top" class="leftHeading">
                            (3.i)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या निदेशकों/सांझेदारों/स्वामी अन्य कम्पनियों में अपना वित्तीय हित रखते हैं या बोर्ड का प्रतिनिधित्व करते हैं, तो ब्यौरा दें |<br />If the Directors/Partners/Proprietor have financial interests or are represented on Board(s) of other companies, give details:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3g" runat="server" CssClass="txt_bx200x80" TabIndex="4" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3g"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator2"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator3" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3g"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator3"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none">
                        <td align="left" valign="top" class="leftHeading">
                            (3.j)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या आपकी पूँजी का कोई भाग किसी बड़ी इकाई के पास है ? यदि हों, तो ब्यौरा दें |<br />Whether any portion of your capital is held by any large unit? If some, give details:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3h" runat="server" CssClass="txt_bx200x80" TabIndex="5" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3h"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator3"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator4" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3h"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator4"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none">
                        <td align="left" valign="top" class="leftHeading">
                            (3.k)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या उसी प्रबंधन के अंतर्गत पूँजी या ऋण निधि की किसी बड़ी इकाई के साथ पारस्परिक सम्बद्धता है, यदि हां तो ब्यौरा दें |<br />Whether there is any inter-locking of capital or loan funds with any large unit under the same management? If so, give details:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3i" runat="server" CssClass="txt_bx200x80" TabIndex="6" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3i"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator4"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator5" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3i"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator5"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            (3.l)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या कोई ऋण या अग्रिम धनराशि किसी बड़े यूनिट या साधन युक्त व्यक्ति द्वारा उपक्रत किया गया है, यदि हां तो ब्यौरा दें |<br />Whether any loan or advance has been under written by any large unit or persons possessing large means? If so, give details:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3j" runat="server" CssClass="txt_bx200x80" TabIndex="7" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3j"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator5"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator6" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3j"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator6"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            (3.m)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>आयकर सर्किल/वार्ड/जिला<br />Income-tax Circle/Ward/District:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3k" runat="server" CssClass="txt_bx200x80" TabIndex="8" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3k"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator6"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator7" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3k"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator7"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                    
                    
                    
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            (3.n)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या आप इस नाम से या किसी अन्य नाम से इस निगम के साथ व्यापार कर रहे हैं यदि हां, तो नाम और पता बताएं |<br />Are you already doing business with NSIC under this or some other name? If so, give name and address:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_3n" runat="server" CssClass="txt_bx200x80" TabIndex="11" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_3n"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator10"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator10" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_3n"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator10"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                    
                    
                    <tr align="left" valign="top" >
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (4) वह भंडार/उत्पाद जिसके लिए पंजीकरण की आवश्यक है<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Stores for which registration is required
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" >
                        <td align="left" valign="top" class="style5">
                            (4.a)
                        </td>
                        <td align="left" valign="top" class="style5">
                            <span class="MandatoryField">*</span>Whether you are registered with DGS&D or any other government department? If so, give registration No. and date, attach a copy of registration certificate and specify the item/service for which registered:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style6">
                            <asp:TextBox ID="txt_4a" runat="server" CssClass="txt_bx200x80" TabIndex="12" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_4a"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator12"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator12" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 8000 characters"
                                ControlToValidate="txt_4a"
                                ClientValidationFunction="CharacterLimit8000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender24" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator12"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (4.b)
                        </td>
                         <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या आपने कोई भंडार/ उत्पाद पूर्ती व निपटान महानिदेशालय के माध्यम से सरकार को या सीधे सरकारी  विभाग को अथवा किसी अन्य आपूर्तिकर्ता के  माध्यम से सप्लाई किया है <%--यदि हां, तो पिछले तीन वर्ष के अनुबंध, यदि कोई हो, तो उसका ब्यौरा संलग्नक-एफ--%> <%--<a href="../Document/Annexures/AnnexureG.pdf" target="_blank"></a>--%> में दे |<br />Have you supplied any stores/products or rendered any services to the Government through GeM, Direct to the Government Department or through any other Supplier?<%-- If so, give details of the contract if any, during the last Three years in  Annexure-‘F’ in Upload Section--%> <%--<a href="../Document/Annexures/AnnexureF.pdf" target="_blank"></a>--%>:
                        </td>

                       <%-- <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>क्या आपने कोई भंडार/ उत्पाद पूर्ती व निपटान महानिदेशालय के माध्यम से सरकार को या सीधे सरकारी  विभाग को अथवा किसी अन्य आपूर्तिकर्ता के  माध्यम से सप्लाई किया है यदि हां, तो पिछले तीन वर्ष के अनुबंध, यदि कोई हो, तो उसका ब्यौरा संलग्नक-एफ  में दे |<br />Have you supplied any stores/products or rendered any services to the Government through DGS&amp;D, Direct to the Government Department or through any other Supplier? If so, give details of the contract if any, during the last Three years in  Annexure-‘F’ :
                        </td>--%>
                        <td align="left" valign="top" style="padding-bottom:10px;" class="style7">
                            <asp:TextBox ID="txt_4b" runat="server" CssClass="txt_bx200x80" TabIndex="12" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="None"
                                ErrorMessage="Please provide input"
                                ControlToValidate="txt_4b"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender25" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator13"
                            ></asp:ValidatorCalloutExtender>
                            
                            <asp:CustomValidator ID="CustomValidator13" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_4b"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender26" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator13"
                            ></asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="style1">
                        
                        </td>
                        <td align="left" valign="top" class="style1">
                        
                        </td>
                        <td align="left" valign="top" class="style7">
                        
                        </td>
                    </tr>
                    <tr align="right" valign="top">
                      <td colspan="2">
                 <%-- <input type="button" id="btnback" class="btnCls" value="Back"   onclick ="Javascript:window.history.back(); return false;"/>--%>
                      
                  <asp:Button ID="btnBack" runat="server"  Text="Back"  class="btnCls"  CausesValidation="false" onclick="btnBack_Click" 
                                 />
                        
                        </td>
                        <td >
                            <asp:UpdatePanel ID="updSubmt" runat="server" >
                        <ContentTemplate >
                        <div>
                                        <div style="float: right;">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" 
                                CssClass="btnCls" TabIndex="37"
                                ValidationGroup="1" Width="150px" onclick="btnSubmit_Click"  />
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

