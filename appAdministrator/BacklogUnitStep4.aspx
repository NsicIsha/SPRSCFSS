<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="BacklogUnitStep4.aspx.cs" 
Inherits="appAdministrator_BacklogUnitStep4" Title="Backlog step4" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">

    function CheckManufacturing() {
        var grid = document.getElementById("<%= gvCapitalDetails.ClientID%>");
        alert(grid.rows.Length);
    }

    function FuntxtSum() {
        var txt6a1 = document.getElementById("ctl00_ContentPlaceHolder1_gvCapitalDetails_ctl02_txt_6a1").val();
        var txt6a2 = document.getElementById("ctl00_ContentPlaceHolder1_gvCapitalDetails_ctl02_txt_6a2").val();
        var txt6b = document.getElementById("ctl00_ContentPlaceHolder1_gvCapitalDetails_ctl02_txt_6b").val();
        var txt6a1Val = eval(0.0);
        var txt6a2Val = eval(0.0);
        var txt6bVal = eval(0.0);
        try
        {
            txt6a1Val = eval(txt6a1.value);
            txt6a2Val = eval(txt6a2.value);
            txt6bVal = eval(txt6b.value);

            document.getElementById("ctl00_ContentPlaceHolder1_gvCapitalDetails_ctl02_txt_tot").value = eval(txt6a1Val) + eval(txt6a2Val) + eval(txt6bVal);
        }
        catch(e){
            document.getElementById("ctl00_ContentPlaceHolder1_gvCapitalDetails_ctl02_txt_tot").value = "0.0";
        }
//        ctl00_ContentPlaceHolder1_txt_6a1
//        ctl00_ContentPlaceHolder1_txt_6a2
//        ctl00_ContentPlaceHolder1_txt_6b

//        ctl00_ContentPlaceHolder1_txt_tot
    }


    function AddText(rowsno) {
        var txtTotal = 0.00; //place where we will keep the total
        var passed = false; //bool val to check whether all textboxes are passed
        var id = 2;
        var i = 0;
        //var ROWSNO = $("#hiddenvariable").val();
        while (i < rowsno) 
        {
            var newId = i +  parseInt(id.toString());

            var getElement1 = $("#ctl00_ContentPlaceHolder1_gvCapitalDetails_ctl0" + newId + "_txt_6a1").val();
            var getElement2 = $("#ctl00_ContentPlaceHolder1_gvCapitalDetails_ctl0" + newId + "_txt_6a2").val();
            var getElement3 = $("#ctl00_ContentPlaceHolder1_gvCapitalDetails_ctl0" + newId + "_txt_6b").val();
            var getElementTot;

            
            if (getElement1 != null && getElement2 != null && getElement3 != null)
             {
                 getElementTot = parseFloat(getElement1) + parseFloat(getElement2) + parseFloat(getElement3);
                 $("#ctl00_ContentPlaceHolder1_gvCapitalDetails_ctl0" + newId + "_txt_tot").val(getElementTot);
            
        }
        i = i + 1; //increment id
            
        }
    }
     
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:HiddenField ID ="hdn_enterprise" runat ="server" />
<asp:HiddenField ID ="hdn_enterprise_size" runat ="server" />
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">
                New Unit Registration - Step 4
            </td>
        </tr>
         

        <tr>
            <td align="left" valign="top" style="padding-left:10px; padding-right:10px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" class="rowStep">
                            STEP 4 of 7
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (5) कारखाने / गोदाम के ब्योरे <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Details of Factory/Godown
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                    
                        <td align="left" valign="top" class="leftHeading" colspan="3">
                            <asp:GridView ID="gvFactoryDetails" AutoGenerateColumns="false" runat="server" 
                                onrowdatabound="gvFactoryDetails_RowDataBound">
                                <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                                <RowStyle CssClass="GrdviewRowOdd" />
                                <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                <Columns>
                                    <asp:BoundField  DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory Address">                    
                                    </asp:BoundField>
                                
                                    <asp:TemplateField ItemStyle-VerticalAlign="Top" HeaderText="*(5.a.1)पता/ Address"  >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_5a1" runat="server" TextMode="MultiLine" CssClass="txt_bx150" Text='<%#Eval("fGPtbl_CUnit_5A1") %>'></asp:TextBox>
                                            <asp:HiddenField ID="hdnunit" runat="server" Value='<%# Bind("fGPtbl_CUnit_Id") %>' />
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                ErrorMessage="Please provide Address"
                                                ControlToValidate="txt_5a1"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator1"
                                            ></asp:ValidatorCalloutExtender>
                                            
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                                ErrorMessage="Max allowed lenght is 5000 characters"
                                                ControlToValidate="txt_5a1"
                                                ClientValidationFunction="CharacterLimit5000"
                                            ></asp:CustomValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="CustomValidator1"
                                            ></asp:ValidatorCalloutExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField ItemStyle-VerticalAlign="Top" HeaderText="*(5.a.2)फ़ोन स./ Tel. No." ControlStyle-Width="125px" ItemStyle-Width="155px" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_5a2" runat="server" CssClass="txt_bx" Text='<%#Eval("fGPtbl_CUnit_5A2") %>' 
                                                ValidationGroup="1" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ErrorMessage="Please provide Tel. No." ControlToValidate="txt_5a2" Display="None" ></asp:RequiredFieldValidator>
                                                
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator2"
                                            ></asp:ValidatorCalloutExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-VerticalAlign="Top" ControlStyle-Width="125px" ItemStyle-Width="155px" >
                                        <HeaderTemplate>
                                            <table cellpadding="0" cellspacing="0" border="1">
                                                <tr align="left" valign="top">
                                                    <td align="left" valign="top" colspan="2">
                                                        (5.b) क्या  विनिर्माण कार्य पॉवर के बिना या पॉवर की सहायता से किया जाता है यदि पॉवर के सहायता से किया जाता है तो बताये :<br />Is process of manufacturing is carried out with or with out power? If with the aid of power please state:-
                                                    </td>
                                                </tr>
                                                <tr align="left" valign="top">
                                                    <td align="left" valign="top">
                                                        <span>*</span>(5.b.1) हार्स पावर - आवंटित |<br />Horse Power - allotted
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <span>*</span>(5.b.2) हार्स पावर - स्थापित |<br />Horse Power - installed:
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="0" cellspacing="0" border="1">
                                                <tr align="left" valign="top">
                                                    <td align="left" valign="top">
                                                        <asp:TextBox ID="txt_5b1" runat="server" CssClass="txt_bx" Text='<%#Eval("fGPtbl_CUnit_5BB1") %>' ></asp:TextBox></td>
                                                    <td align="left" valign="top">
                                                        <asp:TextBox ID="txt_5b2" runat="server" CssClass="txt_bx" Text='<%#Eval("fGPtbl_CUnit_5BB2") %>'></asp:TextBox></td>
                                                 </tr>
                                             </table>
                                         </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="(5.c)*कारखाने/ कवर किये गए गोदाम / बिना कवर का क्षेत्र, विभागों और प्रयोगशालाओं आदि का संक्षिप्त विवरण |<br />Brief Description of the Factory/Godown Covered/Uncovered Area Departments and laboratories etc.:" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_5c" runat="server" CssClass="txt_bx200x80" Text='<%#Eval("fGPtbl_CUnit_5C") %>' 
                                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox>
                                                
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                ControlToValidate="txt_5c" ErrorMessage="Please enter brief description of the Factory/Godown Covered/Uncovered Area Departments and laboratories etc." 
                                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
                                                 <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator3"
                                            ></asp:ValidatorCalloutExtender> 
                                            
                                             <asp:CustomValidator ID="CustomValidator2" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                                ErrorMessage="Max allowed lenght is 8000 characters"
                                                ControlToValidate="txt_5a1"
                                                ClientValidationFunction="CharacterLimit8000"
                                            ></asp:CustomValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="CustomValidator2"
                                            ></asp:ValidatorCalloutExtender> 
                                        </ItemTemplate>
                                      </asp:TemplateField>
                                   </Columns>
                                </asp:GridView>
                            </td>
                     </tr>
                          
                     <tr align="left" valign="top">
                            <td colspan="3" align="right" valign="top" style="height:10px;">
                                &nbsp;
                            </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="style1">
                            (6) पूंजी संरचना का ब्यौरा <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Detail of capital structure:-
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                    
                        <td align="left" valign="top" class="leftHeading" colspan="3">
                            <asp:GridView ID="gvCapitalDetails" Width="100%" AutoGenerateColumns="false" 
                                runat="server" onrowdatabound="gvCapitalDetails_RowDataBound"  
                                >
                                <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                                <RowStyle CssClass="GrdviewRowOdd" />
                                <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                <Columns>
                                    <asp:BoundField  DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory Address">                    
                                    </asp:BoundField>
                                   <asp:TemplateField ControlStyle-Width="150px" ItemStyle-Width="180px">
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" border="1">
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" colspan="2">
                                                    (6.a) स्थिर पूंजी (बिना मूल्यह्रास के मूल लागत)
                                                            Fixed Capital (original value without depreciation:- 
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top">
                                                    (6.a.1)* भूमि और भवन |
                                                            Land & Building: 
                                                </td>
                                                <td align="left" valign="top">
                                                    (6.a.2) * संयंत्र और मशीनरी |
                                                            Plant & Machinery  (  In lacs ) : 
                                                </td>
                                                <td align="left" valign="top">
                                                    (6.a.3) * Equipments & Tools  (  In lacs ) : 
                                                </td>

                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0" border="1">
                                            <tr valign="top" align="left">
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txt_6a1" runat="server" Text='<%#Eval("fGPtbl_CUnit_6A1") %>'
                                                        CssClass="txt_bx">0.0</asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_6a1"
                                                        FilterType="Custom,Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                        ControlToValidate="txt_6a1" ErrorMessage="Please enter value for Land & Building " 
                                                        ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
                                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                        PopupPosition="BottomLeft"
                                                        TargetControlID="RequiredFieldValidator4"
                                                    ></asp:ValidatorCalloutExtender> 
                                                </td>
                                                <td valign="top" align="left">
                                                    <asp:TextBox ID="txt_6a2" runat="server" CssClass="txt_bx" Text='<%#Eval("fGPtbl_CUnit_6A2") %>'></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_6a2"
                                                        FilterType="Custom,Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                        ControlToValidate="txt_6a2" ErrorMessage="Please enter value for Plant & Machinery " 
                                                        ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                        PopupPosition="BottomLeft"
                                                        TargetControlID="RequiredFieldValidator5"
                                                    ></asp:ValidatorCalloutExtender> 
                                                </td>
                                                   <td valign="top" align="left">
                                                    <asp:TextBox ID="txt_6a3" runat="server" CssClass="txt_bx" Text='<%#Eval("fGPtbl_CUnit_6A3") %>'></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender52" runat="server" TargetControlID="txt_6a3"
                                                        FilterType="Custom,Numbers" ValidChars="."></asp:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" 
                                                        ControlToValidate="txt_6a3" ErrorMessage="Please enter value for Tools & Equipment " 
                                                        ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender52" runat="server"
                                                        PopupPosition="BottomLeft"
                                                        TargetControlID="RequiredFieldValidator52"
                                                    ></asp:ValidatorCalloutExtender> 
                                                </td>



                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                   </asp:TemplateField> 
                                   <asp:TemplateField ControlStyle-Width="150px" ItemStyle-Width="180px" HeaderText="(6.b.1)* चालू परिसंपत्ति (इसमें कच्चे माल का स्टॉक , उपयोज्य भंडार ,अर्धनिर्मित उत्पाद, निर्मित उत्पाद नकद रोकड़ ,बैंक अधिशेष आदि शामिल होना चाहिए ) |<br />Current Assets (this should include stock of Raw material. Consumable stores, semi finished/finished products, cash in hand, bank balance etc.):" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_6b" runat="server" CssClass="txt_bx" Text='<%#Eval("fGPtbl_CUnit_6B") %>' ></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_6b"
                                                 FilterType="Custom,Numbers" ValidChars="." ></asp:FilteredTextBoxExtender>
                                                 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                ControlToValidate="txt_6b" ErrorMessage="Please enter value for Current Assets " 
                                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
                                             <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator6"
                                            ></asp:ValidatorCalloutExtender> 
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                            <asp:TemplateField ControlStyle-Width="150px" ItemStyle-Width="180px" HeaderText="(6.b.2)* Current assests for Service Sector(this should binclude manpower, cash in hand, bank balanace etc.):" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_6b2" runat="server" CssClass="txt_bx" Text='<%#Eval("fGPtbl_CUnit_6B2") %>' ></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender51" runat="server" TargetControlID="txt_6b2"
                                                 FilterType="Custom,Numbers" ValidChars="." ></asp:FilteredTextBoxExtender>
                                                 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" 
                                                ControlToValidate="txt_6b2" ErrorMessage="Please enter value for Current Assets for Service Sector" 
                                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
                                             <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender51" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator51"
                                            ></asp:ValidatorCalloutExtender> 
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total(6.a.1 + 6.a.2 + 6.a.3 + 6.b.1 + 6.b.2)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_tot" runat ="server" CssClass="txt_bx" Text="0.0" ></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator11" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                                ErrorMessage="Invalid data"
                                                ControlToValidate="txt_tot"
                                                ClientValidationFunction="IsNumericNonNegative"
                                            ></asp:CustomValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="CustomValidator11"
                                            ></asp:ValidatorCalloutExtender> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                             </asp:GridView>

                         </td>
                     </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>
                    
                  <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (7) तकनीकी सुविधाएं <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Technical facilities :-
                        </td>
                    </tr>
                      
                    <tr align="left" valign="top">
                    
                        <td align="left" valign="top" class="leftHeading"  colspan="3">
                            <asp:GridView ID="gvTechnicalDetails" AutoGenerateColumns="false" 
                                runat="server" onrowdatabound="gvTechnicalDetails_RowDataBound">
                                <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                                <RowStyle CssClass="GrdviewRowOdd" />
                                <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                <Columns>
                                    <asp:BoundField  DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory Address">                    
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="(7.a) * संलग्न 'बी' के अनुसार स्थापित संयंत्र व मशीनरी के ब्योरे|<br/>Details of plant and machinery installed as per Annexure 'B': " >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_7a" runat="server" CssClass="txt_bx150"
                                                ValidationGroup="1" TextMode="MultiLine" Text='<%#Eval("fGPtbl_CUnit_7A") %>' >N.A.</asp:TextBox>
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                ControlToValidate="txt_7a" ErrorMessage="Please enter details of plant and machinery installed as per Annexure 'B' " 
                                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
                                              <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator9"
                                            ></asp:ValidatorCalloutExtender> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="(7.b)* विनिर्माणाधीन भंडार / उत्पाद को विनिर्दिष्ट करे|<br/> Specify stores/products under manufacture : ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_7b" runat="server" CssClass="txt_bx150" ValidationGroup="1" TextMode="MultiLine" Text='<%#Bind("fGPtbl_CUnit_7B") %>' >N.A.</asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                ControlToValidate="txt_7b" ErrorMessage="Please specify stores/products under manufacture " 
                                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator10"
                                            ></asp:ValidatorCalloutExtender> 
                                            
                                             <asp:CustomValidator ID="CustomValidator3" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                                ErrorMessage="Max allowed lenght is 5000 characters"
                                                ControlToValidate="txt_7b"
                                                ClientValidationFunction="CharacterLimit5000"
                                            ></asp:CustomValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="CustomValidator3"
                                            ></asp:ValidatorCalloutExtender> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="(7.c)*उत्पादों की तकनीकी ज्ञान तथा प्रवाह चार्ट सहित पूरी सूचना  |<br />Full information of the technical know-how of products with flow chart.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_7c" runat="server" CssClass="txt_bx150" ValidationGroup="1" TextMode="MultiLine" Text='<%#Eval("fGPtbl_CUnit_7C") %>'>N.A.</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                                ControlToValidate="txt_7c" ErrorMessage="Please enter full information of the technical know-how of products with flow chart" 
                                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
                                             <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator11"
                                            ></asp:ValidatorCalloutExtender> 
                                            
                                             <asp:CustomValidator ID="CustomValidator4" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                                ErrorMessage="Max allowed lenght is 5000 characters"
                                                ControlToValidate="txt_7c"
                                                ClientValidationFunction="CharacterLimit5000"
                                            ></asp:CustomValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="CustomValidator4"
                                            ></asp:ValidatorCalloutExtender> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="(7.d)*नेमी तथा स्वीकार्यता टेस्ट के लिए गुणवत्ता व्यवस्था |<br />Quality control arrangement for routine and acceptance test :">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_7d" runat="server" CssClass="txt_bx150"
                                                ValidationGroup="1" TextMode="MultiLine" Text='<%#Eval("fGPtbl_CUnit_7D") %>'>N.A.</asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                                ControlToValidate="txt_7d" ErrorMessage="Please enter details about quality control arrangement for routine and acceptance test  " 
                                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
                                              <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator12"
                                            ></asp:ValidatorCalloutExtender> 
                                            
                                             <asp:CustomValidator ID="CustomValidator5" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                                ErrorMessage="Max allowed lenght is 5000 characters"
                                                ControlToValidate="txt_7d"
                                                ClientValidationFunction="CharacterLimit5000"
                                            ></asp:CustomValidator>
                                          <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="CustomValidator5"
                                            ></asp:ValidatorCalloutExtender> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="(7.e)* टेस्टिंग  मशीनरी व सुविधाओ के ब्योरे  |<br />Details of testing machinery & facilities :">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_7e" runat="server" CssClass="txt_bx150" ValidationGroup="1" TextMode="MultiLine" Text='<%#Eval("fGPtbl_CUnit_7E") %>'>N.A.</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                ControlToValidate="txt_7e" ErrorMessage="Please enter details of testing machinery & facilities " 
                                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator13"
                                            ></asp:ValidatorCalloutExtender> 
                                            
                                             <asp:CustomValidator ID="CustomValidator6" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                                ErrorMessage="Max allowed lenght is 5000 characters"
                                                ControlToValidate="txt_7e"
                                                ClientValidationFunction="CharacterLimit5000"
                                            ></asp:CustomValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                                PopupPosition="BottomLeft"
                                                TargetControlID="CustomValidator6"
                                            ></asp:ValidatorCalloutExtender> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                             </asp:GridView>
                         </td>
                     </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            &nbsp;</td></tr><tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (8)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                           <span class="MandatoryField">*</span> कृपया बताये कि टाइप टैस्टो सहित जहाँ यह  अपेक्षित है , उत्पादों को पूरी तरह से टैस्ट किया गया है यदि हाँ तो टैस्ट रिपोर्टो कि एक प्रति लगाए |<br />State if the products have been fully tested, including type tests where required in specifications. If so, attach copy of test reports:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_8" runat="server" CssClass="txt_bx200x80" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                ControlToValidate="txt_8" ErrorMessage="Please enter details of testing machinery & facilities " 
                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator14"
                            ></asp:ValidatorCalloutExtender> 
                            
                            <asp:CustomValidator ID="CustomValidator7" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 5000 characters"
                                ControlToValidate="txt_8"
                                ClientValidationFunction="CharacterLimit5000"
                            ></asp:CustomValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator7"
                            ></asp:ValidatorCalloutExtender> 
                                                        
                        </td>
                    </tr>
                      <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                           
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (9)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                          <span class="MandatoryField">*</span>  क्या उत्पादों को बिक्री बाद सर्विस की आवश्यकता होती है | उन स्थानों का नाम व पता दे जहाँ ऐसी सुविधाए उपलब्ध है | बताए की क्या आपके उत्पाद पुरे भारत में सीधे आपके द्वारा ही बेचे जाते है, यदि नहीं तो उन फ़र्मो के नाम बताए जिनके साथ उत्पाद - वार/ छेत्र - वार विपणन प्रबंध किए गए है |<br />If the products require after sales service, give Names & Addresses of places where such facilities are available.Indicate if your products are directly marketed by you in whole of India. If not, indicate names of firms with whom marketing arrangements have been made product-wise/area-wise:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_9" runat="server" CssClass="txt_bx200x80"  
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                ControlToValidate="txt_9" ErrorMessage="Please enter details of testing machinery & facilities " 
                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator15"
                            ></asp:ValidatorCalloutExtender> 
                            
                            <asp:CustomValidator ID="CustomValidator8" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 8000 characters"
                                ControlToValidate="txt_9"
                                ClientValidationFunction="CharacterLimit8000"
                            ></asp:CustomValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator8"
                                
                            ></asp:ValidatorCalloutExtender> 
                        </td>
                    </tr>
                      <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (10)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                          <span class="MandatoryField">*</span>  लघु उद्योग इकाई का विगत तीन वर्षो  का (वर्ष - वार) प्रचालन परिणाम व वित्तीय स्थिति दर्शाते हुए विवरण | सम्बद्ध अंकेक्षित तुलन पत्रों सहित(संलग्नक - सी )<br />Statement indicating the Result of Operations and Financial Position of the 
                            EM Unit for the last three years (year-wise) duly supported by relevant Audited Balance Sheets (Annexure-C):
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_10" runat="server" CssClass="txt_bx200x80" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                ControlToValidate="txt_10" ErrorMessage="Please enter details of testing machinery & facilities " 
                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender24" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator16"
                            ></asp:ValidatorCalloutExtender> 
                            
                            <asp:CustomValidator ID="CustomValidator9" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 8000 characters"
                                ControlToValidate="txt_10"
                                ClientValidationFunction="CharacterLimit8000"
                            ></asp:CustomValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender25" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator9"
                                
                            ></asp:ValidatorCalloutExtender> 
                            
                           
                        </td>
                    </tr>
                     <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (11)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                          <span class="MandatoryField">*</span>  बैंकरों के नाम व पते (प्रत्येक बैंक का खाता सं. दें और सलग्नक-एफ के अनुसार मूल बैंकर रिपोर्ट प्रस्तुत करे )<br />Name and address of the Bankers (Indicate Account No. with each bank and submit Bankers Report in Original as per proforma in Annexure-F):
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_11" runat="server" CssClass="txt_bx200x80" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                ControlToValidate="txt_11" ErrorMessage="Please enter details of testing machinery & facilities " 
                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender26" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator17"
                            ></asp:ValidatorCalloutExtender> 
                            
                            <asp:CustomValidator ID="CustomValidator10" runat="server" Display="None" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Max allowed lenght is 8000 characters"
                                ControlToValidate="txt_11"
                                ClientValidationFunction="CharacterLimit8000"
                            ></asp:CustomValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender27" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="CustomValidator10"
                                
                            ></asp:ValidatorCalloutExtender> 
                            
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (12)
                            अतिरिक्त्त / वैकल्पिक जानकारी <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Additional/Optional information

                            </td>
                    </tr>
                    
                    
                    
                   
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (12.a
                            )</td><td align="left" valign="top" class="leftHeading">
                       <span class="MandatoryField">*</span> कृपया बताएं कि क्या उत्पाद बी.आई.एस. मार्का है |  यदि हां, तो वैध लाइसेंस की एक प्रति लगाएं |<br />State if the products are BIS Marked. If so, attach a copy of valid BIS License.
                        </td>
                        
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_12a" runat="server" CssClass="txt_bx200x80"  
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                                ControlToValidate="txt_12a" ErrorMessage="Please enter BIS details " 
                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator18"
                            ></asp:ValidatorCalloutExtender> 
                        
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (12.b
                            )</td><td align="left" valign="top" class="leftHeading">
                       <span class="MandatoryField">*</span> कृपया बताएं कि क्या फर्म आई अस ओ 9000 प्रमाणन में है | यदि हां , तो उसकी एक प्रति लगाएं |<br />State if the firm has ISO 9000 Certification. If yes,attach copy of the same.
                        </td>
                        
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_12b" runat="server" CssClass="txt_bx200x80" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                ControlToValidate="txt_12b" ErrorMessage="Please enter BIS details " 
                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender29" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator19"
                            ></asp:ValidatorCalloutExtender> 
                            
                        
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (12.c
                            )</td><td align="left" valign="top" class="leftHeading">
                             <span class="MandatoryField">*</span> State name, address, & email of the Industrial Association holding membership, if any:
                      </td>
                        
                        <td align="left" valign="top" style="padding-bottom:10px;">
                              <asp:TextBox ID="txt_12c" runat="server" CssClass="txt_bx200x80" 
                                ValidationGroup="1" TextMode="MultiLine">N.A.</asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" 
                                ControlToValidate="txt_12c" ErrorMessage="Please enter BIS details " 
                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender61" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator61"
                            ></asp:ValidatorCalloutExtender> 
                            </td>
                    </tr>
                    
                     <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (13) इस आवेदन के हस्ताक्षरकर्ता का नाम व पदनाम (जहां लागू हो, मुख्तारनामा की एक प्रति लगाएं)|<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name and designation of the signatory of this application(enclose a copy of Power of Attorney where applicable)
                            

                            </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (13.a
                            )</td><td align="left" valign="top" class="leftHeading">
                       <span class="MandatoryField">*</span>हस्ताक्षरकर्ता का नाम |<br />Name of the signatory of this application
                        </td>
                        
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_13a" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" >N.A.</asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                                ControlToValidate="txt_13a" ErrorMessage="Please enter name of the signatory " 
                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender30" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator20"
                            ></asp:ValidatorCalloutExtender> 
                            
                        
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (13.b
                            )</td><td align="left" valign="top" class="leftHeading">
                       <span class="MandatoryField">*</span>हस्ताक्षरकर्ता का पदनाम |<br />Designation of the signatory of this application
                        </td>
                        
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_13b" runat="server" CssClass="txt_bx"  
                                ValidationGroup="1" >N.A.</asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
                                ControlToValidate="txt_13b" ErrorMessage="Please enter designation of the signatory " 
                                ValidationGroup="1" Display="None"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator21"
                            ></asp:ValidatorCalloutExtender> 
                            
                        
                        </td>
                    </tr>
                    
                    
                    
                    
                  <tr align="right" valign="top">
                        <td colspan="2">
                         <%-- <input type="button" id="btnback" class="btnCls" value="Back"   onclick ="Javascript:window.history.back(); return false;"/>--%>
                         <asp:Button ID="btnBack" runat="server" text="Back" Visible="true" CssClass="btnCls" CausesValidation="false"  onclick="btn_back_click"  />
                         
                         </td>
                          <%--<td>
                          
                            <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" 
                                CssClass="btnCls" TabIndex="9"
                                ValidationGroup="1" Width="150px" onclick="btnSubmit_Click" />
                        </td>--%>
                        <td >
                        <asp:UpdatePanel ID="updSubmt" runat="server" >
                        <ContentTemplate >
                        <div>
                                        <div style="float: right;">
                                            <asp:Button ID="btnSubmit"   runat="server" Text="Save &amp; Continue &gt;&gt;" 
                                CssClass="btnCls" TabIndex="9"
                                ValidationGroup="1" Width="150px" onclick="btnSubmit_Click" />
                                
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
                            <%--<asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" CssClass="btnCls"
                                ValidationGroup="1" Width="150px" OnClick="btnSubmit_Click" TabIndex="54" />--%>
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

