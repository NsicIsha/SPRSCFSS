<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Report.master" AutoEventWireup="true" CodeFile="Online_UnitAmendment_ViewChanges.aspx.cs" Inherits="appAdministrator_Online_UnitAmendment_ViewChanges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript" >
    function CallPrint(strid)
   {
   $("#ctl00_ContentPlaceHolder1_btn1").hide();
     $("#ctl00_ContentPlaceHolder1_btn").hide();
   var prtContent = document.getElementById(strid);
   var WinPrint = window.open('','','left=0,top=0,width=700px,height=800px,toolbar=1,scrollbars=1,status=0');
   WinPrint.document.write(prtContent.innerHTML);
   WinPrint.document.close();
   WinPrint.focus();
   $("#Print_Div").hide();
   WinPrint.print();
   WinPrint.close();
    $("#ctl00_ContentPlaceHolder1_btn1").show();
     $("#ctl00_ContentPlaceHolder1_btn").show();
   }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">
                Amendment Changes
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" id="btn" runat="server">
                <asp:LinkButton ID="lbBack" runat="server" CssClass="headerLink" 
                    style="padding-right:10px;" onclick="lbBack_Click">&lt;&lt; Back</asp:LinkButton>
                    
                   <%--<asp:LinkButton ID="lnkNext" runat="server" CssClass="headerLink" 
                    style="padding-right:10px;" onclick="lnkNext_Click" >  Next&lt;&lt;&lt;&lt;  </asp:LinkButton>--%> 
                    <asp:Button ID="btnNext" runat="server" Text="Save&Continue" CssClass="btnCls" Width="150px" OnClick="lnkNext_Click" />
            </td>
        </tr>
        <tr>
            
            <td align="left" valign="top" style="padding-left:10px; padding-right:10px;">
            
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>
                    
                    <%--<tr align="left" valign="top">
                        <td align="left" valign="top" style="width:5%;" class="leftHeading">
                            1.
                        </td>
                        
                        <td align="left" valign="top" style="width:60%;" class="leftHeadingBottom">
                            GP Reference Number:
                        </td>
                        <td align="left" valign="top" style="width:35%;" class="rightHeadingBottom">
                            <asp:Label ID="lblGpref" runat="server"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.1
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            पैन कार्ड संख्या/ PAN Card Number:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblPAN" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.2
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            आवेदक फर्म का नाम/ Name of Applicant firm:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblCompany" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.3
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            Declaration of having made the entry in MSME Data Bank:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblMSME" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top" >
                    <td align="left" valign="top" class="leftHeading">
                    1.4
                    </td>
                    <td align="left" valign="top" class="leftHeading">
                       उद्योग आधार संख्या/Udyog Aadhar number:
                    </td>
                     <td align="left" valign="top" class="rightHeadingBottom">
                       <asp:Label ID="lbl_udyog_aadhar" runat="server" ></asp:Label>
                     </td>
                    
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            1.a मुख्य कार्यालय/ HEAD OFFICE
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.a.1
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पता/ Address:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.a.2
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पिन कोड/ Pin Code:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblPin" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.a.3
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            राज्य/ State:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblState" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.a.4
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                          ज़िला/शहर/District/ City:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblCity" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.a.5
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            फोन/ Telephone:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.a.6
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            फैक्स/ Fax:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblFax" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.a.7
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            ई-मेल/ E-mail:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            1.b कारखाना/ FACTORY
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="leftHeading">
                          <asp:GridView ID="GridFactory" runat="server" AutoGenerateColumns="false" 
                                onrowdatabound="GridFactory_RowDataBound" >
                            <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                            <RowStyle CssClass="GrdviewRowOdd" />
                            <AlternatingRowStyle CssClass="GrdviewRowEven" />
                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate><%#Container.DataItemIndex+1 %>
                                        <asp:HiddenField ID="HDFunitid" runat="server" Value='<%#Eval("fGPtbl_CUnit_Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Address" />
                                
                                <asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Zip" HeaderText="Pin" />
                                
                                <asp:BoundField DataField="UnitState" HeaderText="State" />
                                
                                <asp:BoundField DataField="UnitCity" HeaderText="City" />
                                
                                <asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Phone" HeaderText="Telephone" />
                                
                                <asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Fax" HeaderText="Fax" />
                                
                                <asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Email" HeaderText="Email" />
                            </Columns>                
                           
                          </asp:GridView>
                        </td>
                    </tr>
                   
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            
                        </td>
                    </tr>
                    
                     <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.d
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                          उद्यमी की विशेष श्रेणी/Special Category of Enterpreneur:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblCatcast" runat="server"></asp:Label>                
                        </td>
                    </tr>
                     <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.e
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                           सामाजिक श्रेणी की उद्यम/ Enterprise Social Category:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblCatarea" runat="server"></asp:Label>                
                        </td>
                    </tr>
                    
                     <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.f
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                         श्रेणी यूनिट का आकार/Category UnitSize:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblCatusize" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        
                        <td align="left" colspan="3" valign="top" class="rowHeading">
                        &nbsp;
                        </td>
                     </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            3
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            सरंचना का नाम/Name of Constitution:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblConst" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            3.a</td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            संस्थापना की तारीख/ Date of Incorporation:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblIncorp" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            3.b
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            उत्पादन/ कारोबार आरम्भ करने की तारीख<br />Date of commencement of production/business:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lblBuss" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            3.c.5
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            आप किसी बड़े उद्योग या रा.ल.उ.नि./ डी.जी.एस. एंड डी से पंजीकृत इकाइयों के सांझेदार/ निदेशक हैं यदि हां तो उनका ब्यौरा<br />You are a Partner/Director of any large industries or units registered with NSIC/DGS&D. If so,details:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3c5Part" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            3.c
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            आपकी फर्म इनके अंतर्गत पंजीकृत है<br />Your firm is registered under:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3dregunder" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            3.e
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            आपकी फर्म किसी भारतीय/विदेशी/बड़े उद्योग कम्पनी की सहायक है, तो पेरेंट/होल्डिंग कम्पनी का ब्यौरा|<br />The firm is a subsidiary/Associate of an Indian/foreign/large scale company, particulars of Parent/holding Company:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3eAssociate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            3.f
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            भारत में उसी मूल कम्पनी के अन्य सहायक हैं तो ब्यौरा |<br />There are other subsidiaries of the same parent company in India, full particulars:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3flParent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            3.g
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            निदेशकों/सांझेदारों/स्वामी अन्य कम्पनियों में अपना वित्तीय हित रखते हैं या बोर्ड का प्रतिनिधित्व करते हैं, तो ब्यौरा|<br />The Directors/Partners/Proprietor have financial interests or are represented on Board(s) of other companies, details:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3gDirector" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            3.h
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            आपकी पूँजी का कोई भाग किसी बड़ी इकाई के पास है| यदि हों, तो ब्यौरा|<br />Any portion of your capital is held by any large unit. If some, details:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3hCapital" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            3.i
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            उसी प्रबंधन के अंतर्गत पूँजी या ऋण निधि की किसी बड़ी इकाई के साथ पारस्परिक सम्बद्धता है, यदि हां तो ब्यौरा|<br />There is any inter-locking of capital or loan funds with any large unit under the same management. If so, details:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3iInter" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            3.j
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            कोई ऋण या अग्रिम धनराशि किसी बड़े यूनिट या साधन युक्त व्यक्ति द्वारा उपक्रत किया गया है, यदि हां तो ब्यौरा |<br />Any loan or advance has been under written by any large unit or persons possessing large means. If so, details:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3jLoans" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            3.k
                        </td>
                        <td align="left" valign="top"class="leftHeadingBottom">
                            आयकर सर्किल/वार्ड/जिला<br />Income-tax Circle/Ward/District:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3k1Icircle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            3.d
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            बिक्री कर पंजीकरण सं.<br />Sales Tax - Central  Sales Tax No, VAT Registration No. and Service Tax Registration No:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3lSalestax" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            3.m
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            क्या आपने इससे पहले भी इस निगम में इस नाम से या किसी अन्य नाम से पंजीकरण के लिए आवेदन किया था यदि हां, तो उसका परिणाम क्या रहा |<br />Did you applied for registration with NSIC before under this or any other name? If so, with what result?:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3mNsicreg" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            3.n
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            आप इस नाम से या किसी अन्य नाम से इस निगम के साथ व्यापार कर रहे हैं यदि हां, तो नाम और पता|<br />If you already doing business with NSIC under this or some other name. If so, name and address:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3nnsicbuz" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            3.e
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            आपकी फर्म या उसके किसी निदेशक/ सांझेदार को किसी समय किसी अदालत द्वारा सिविल/ फौजदारी/ राजस्व या आर्थिक अपराध के लिए दण्डित किया गया है यदि हां, तो अपराध और दंड का ब्यौरा|<br />Your firm or any of its Directors/ Partners were prosecuted at any time for any offence under any Court for Civil/ Criminal/ Revenue or Economic Offence. If so, particulars of offence and penalty:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl3oProsecute" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        
                        <td align="left" colspan="3" valign="top" class="rowHeading">
                        &nbsp;
                        </td>
                     </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            4.a
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            आप पूर्ती एवं निपटान महानिदेशालय के पास पंजीकृत हैं | यदि हां तो पंजीकरण संख्या व तारीख|<br />You are registered with DGS&amp;D? If so, registration No. and date :
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl4aDGSD" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            4.a
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            आपने कोई भंडार/ उत्पाद पूर्ती व निपटान महानिदेशालय के माध्यम से सरकार को या सीधे सरकारी  विभाग को अथवा किसी अन्य आपूर्तिकर्ता के  माध्यम से सप्लाई किया है यदि हां, तो पिछले तीन वर्ष के अनुबंध, यदि कोई हो, तो उसका ब्यौरा |<br />You supplied any stores to the Government through DGS&amp;D, Direct to the Government Department or through any other Supplier. If so, details of the contract if any:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl4bDGSDsupplied" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (4.b उन भंडारों/ उत्पादों का ब्यौरा जिनके लिए पंजीकरण प्रार्थित है<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Details of the stores/products for which the registration is sought
                        </td>
                    </tr>
                    <%--DataKeyNames="fGPtbl_CUnit_Store_Id"--%>
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="leftHeading">
                            
                        <asp:GridView ID="gvStoredetails" runat="server" Width="100%" 
                            BorderWidth="0" CellPadding="3"
                            CellSpacing="1" BackColor="#FFFFFF" AutoGenerateColumns="false"
                            
                            AllowSorting="false"
                            AllowPaging="false" onrowdatabound="gvStoredetails_RowDataBound" >
                            <HeaderStyle CssClass="GrdviewHeader" Height="100" />
                            <RowStyle CssClass="GrdviewRowOdd" />
                            <AlternatingRowStyle CssClass="GrdviewRowEven" />
                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <div style="height:90px;">S.No./ क्र. सं.</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <%--<asp:HiddenField ID="HDFUnitid" runat="server" Value='<%#Eval("fGPtbl_CUnit_Store_UnitId") %>' />--%>
                                        <asp:HiddenField ID="HDFUnitid" runat="server" Value='<%#Eval("fGPtbl_CUnit_Store_Id") %>' />
                                        <asp:HiddenField ID="HDFAmdStoreId" runat="server" Value='<%#Eval("fGPtbl_CUnitAmendment_Store_Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="fGPtbl_CUnit_Store_DescriptionStoresProducts" HtmlEncode="false" HeaderText=" Description/ Stores/ Products/Services<br />भंडारों/ उत्पादों /सेवा का विवरण<br/>(2)" HeaderStyle-HorizontalAlign="Center" />
                                
                                <asp:BoundField DataField="fGPtbl_CUnit_Store_RefOfBISSpecification" HtmlEncode="false" HeaderText="Ref. of BIS Specification or other Specification<br />बी.आई.एस. विशिष्ट का सन्दर्भ या अन्य विशिष्टता<br />(3)" HeaderStyle-HorizontalAlign="Center" />
                                
                                <asp:BoundField DataField="fGPtbl_CUnit_Store_ModelBrand" HtmlEncode="false" HeaderText="Model/ Brand<br />माडल/ ब्रांड<br />(4)" HeaderStyle-HorizontalAlign="Center" />
                                
                                <asp:BoundField DataField="fGPtbl_CUnit_Store_LimitingSizeCapacityRating" HtmlEncode="false" HeaderText="Limiting Size Capacity/ Rating<br />सीमित आकार/ क्षमता/ रेटिंग<br />(5)" HeaderStyle-HorizontalAlign="Center" />
                                
                                <asp:BoundField DataField="fGPtbl_CUnit_Store_AnnualProductionCapacity" HtmlEncode="false" HeaderText="Monthly Production Capacity per shift<br />महीने के उत्पादन क्षमता प्रति पारि<br />(6)" HeaderStyle-HorizontalAlign="Center" />
                                
                                <asp:BoundField DataField="fGPtbl_CUnit_Store_NoOfShifts" HtmlEncode="false" HeaderText="No. of shifts factory normally works<br />पारियों की सं. जो प्राय: कारखाने में चलती है<br />(7)" HeaderStyle-HorizontalAlign="Center" />
                                
                            </Columns>
                        </asp:GridView>
                                    
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" style="height:10px;">
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
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate><%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="HDNFUnit" runat="server" Value='<%#Eval("fGPtbl_CUnit_Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField  DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory Address" />                    
                                
                                    <asp:BoundField DataField="fGPtbl_CUnit_5A1" HtmlEncode="false" HeaderText="पता/ Address" />
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_5A2" HeaderText="फ़ोन स./ Tel. No." />
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_5BB1" HtmlEncode="false" HeaderText="हार्स पावर - आवंटित |<br />Horse Power - allotted" />
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_5BB2" HtmlEncode="false" HeaderText=" हार्स पावर - स्थापित |<br />Horse Power - installed:" />
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_5C" HtmlEncode="false" HeaderText="(5.c)*कारखाने/ कवर किये गए गोदाम / बिना कवर का क्षेत्र, विभागों और प्रयोगशालाओं आदि का संक्षिप्त विवरण |<br />Brief Description of the Factory/Godown Covered/Uncovered Area Departments and laboratories etc.:" />
                                    
                                   </Columns>
                                </asp:GridView>
                        </td>
                     </tr>
                     <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" style="height:10px;">
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (5.d) Uploaded documents
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="leftHeading">
                            
                        <asp:GridView ID="gvDocumentDetail" runat="server" Width="100%" 
                            BorderWidth="0" CellPadding="3"
                            CellSpacing="1" BackColor="#FFFFFF" AutoGenerateColumns="false"
                            AllowSorting="false"
                            AllowPaging="false" onrowdatabound="gvDocumentDetail_RowDataBound"> 
                            <HeaderStyle CssClass="GrdviewHeader" Height="40px" />
                            <RowStyle CssClass="GrdviewRowOdd" />
                            <AlternatingRowStyle CssClass="GrdviewRowEven" />
                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                            <EmptyDataTemplate>
                                No Document Found
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No./ क्र. सं." HeaderStyle-Width="40px">
                                    
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NAME">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLName" runat="server" Text='<%#Eval("fGPtbl_DU_Name") %>' Target="_blank"></asp:HyperLink>
                                        <asp:HiddenField ID="HiddenFieldpath" runat="server" Value='<%#Eval("fGPtbl_DU_FilePath") %>' />
                                        <asp:HiddenField ID="HiddenFieldname" runat="server" Value='<%#Eval("fGPtbl_DU_FileName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                          
                          </td>
                     </tr>
                     
                     <tr align="left" valign="top">
                        <td align="left" valign="top" style="height:10px;">
                        </td>
                     </tr>  
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (6) पूंजी संरचना का ब्यौरा &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Detail of capital structure:-
                        </td>
                    </tr>
                    
                    
                    <tr align="left" valign="top">
                    
                        <td align="left" valign="top" class="leftHeading" colspan="3">
                            <asp:GridView ID="gvCapitalDetails" Width="100%" AutoGenerateColumns="false" 
                                runat="server" onrowdatabound="gvCapitalDetails_RowDataBound">
                                <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                                <RowStyle CssClass="GrdviewRowOdd" />
                                <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate><%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="HiddenFieldUnit" runat="server" Value='<%#Eval("fGPtbl_CUnit_Id") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField  DataField="fGPtbl_CUnit_1B_Factory_Address" HtmlEncode="false" HeaderText="Factory Address" />
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_6A1" HtmlEncode="false" HeaderText="भूमि और भवन|<br />Land & Building:" />
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_6A2" HtmlEncode="false" HeaderText="संयंत्र और मशीनरी|<br/>Plant & Machinery:" />
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_6B" HtmlEncode="false" HeaderText="चालू परिसंपत्ति (इसमें कच्चे माल का स्टॉक , उपयोज्य भंडार ,अर्धनिर्मित उत्पाद, निर्मित उत्पाद नकद रोकड़ ,बैंक अधिशेष आदि शामिल होना चाहिए ) |<br />Current Assets (this should include stock of Raw material. Consumable stores, semi finished/finished products, cash in hand, bank balance etc.):" />
                                </Columns>
                             </asp:GridView>
                         </td>
                     </tr>
                     
                     <tr align="left" valign="top">
                        <td align="left" valign="top" style="height:10px;">
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
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate><%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="HFTechnicalUnit" runat="server" Value='<%#Eval("fGPtbl_CUnit_Id") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField  DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory Address"/>
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_7A" HtmlEncode="false" HeaderText="(7.a) * संलग्न 'जे' के अनुसार स्थापित संयंत्र व मशीनरी के ब्योरे|<br/>Details of plant and machinery installed:" />
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_7B" HtmlEncode="false" HeaderText="(7.b)* विनिर्माणाधीन भंडार / उत्पाद को विनिर्दिष्ट करे|<br/> Specify stores/products under manufacture : "  Visible="false"/>
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_7C" HtmlEncode="false" HeaderText="(7.b)*उत्पादों की तकनीकी ज्ञान तथा प्रवाह चार्ट सहित पूरी सूचना  |<br />Full information of the technical know-how of products with flow chart. " />
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_7D" HtmlEncode="false" HeaderText="(7.c)*नेमी तथा स्वीकार्यता टेस्ट के लिए गुणवत्ता व्यवस्था |<br />Quality control arrangement for routine and acceptance test :" />
                                    
                                    <asp:BoundField DataField="fGPtbl_CUnit_7E" HtmlEncode="false" HeaderText="(7.d)* टेस्टिंग  मशीनरी व सुविधाओ के ब्योरे  |<br />Details of testing machinery & facilities :" />
                                    
                                </Columns>
                             </asp:GridView>
                         </td>
                     </tr>
                     
                     <tr align="left" valign="top">
                        <td align="left" valign="top" style="height:10px;">
                        </td>
                     </tr> 
                        <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                           (7.a) व्यापार की मात्रा (लाख रूपयों में)<br /> Turnover (in lacs):- 
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                    
                        <td align="left" valign="top" class="leftHeading"  colspan="3">
                            <asp:GridView ID="grdTurnOver" AutoGenerateColumns="true" runat="server" 
                                Width="100%"  CellPadding="10" CellSpacing="1" 
                                onrowdatabound="grdturnover_rowDatbound" >
                                <EmptyDataTemplate>
                                    <center><strong>No information found</strong></center>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                                <RowStyle CssClass="GrdviewRowOdd" />
                                <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                <Columns>
                                   
                                    
                                </Columns>
                             </asp:GridView>
                         </td>
                     </tr>
                     
                       
                     
                      <tr align="left" valign="top">
                        <td align="left" valign="top" style="height:10px;">
                        </td>
                     </tr> 
                     
                    
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            8
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            टाइप टैस्टो सहित जहाँ यह  अपेक्षित है , उत्पादों को पूरी तरह से टैस्ट किया गया है यदि हाँ तो टैस्ट रिपोर्ट |<br />If the products have been fully tested, including type tests where required in specifications:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl8Typetest" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top" style="display:none;">
                        <td align="left" valign="top" class="leftHeading">
                            9
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            उत्पादों को बिक्री बाद सर्विस की आवश्यकता होती है | उन स्थानों का नाम व पता जहाँ ऐसी सुविधाए उपलब्ध है | आपके उत्पाद पुरे भारत में सीधे आपके द्वारा ही बेचे जाते है, यदि नहीं तो उन फ़र्मो जिनके साथ उत्पाद - वार/ छेत्र - वार विपणन प्रबंध किए गए है |<br />If the products require after sales service, Names & Addresses of places where such facilities are available.If your products are directly marketed by you in whole of India. If not, names of firms with whom marketing arrangements have been made product-wise/area-wise:
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl9Serviceprod" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                    9
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            लघु उद्योग इकाई का विगत तीन वर्षो  का (वर्ष - वार) प्रचालन परिणाम व वित्तीय स्थिति दर्शाते हुए विवरण | सम्बद्ध अंकेक्षित तुलन पत्रों सहित <a href="../Document/Annexures/AnnexureC.pdf" target="_blank">(संलग्नक - सी )</a><br />Statement indicating the Result of Operations and Financial Position of the SSI Unit for the last three years (year-wise) duly supported by relevant Audited Balance Sheets<a href="../Document/Annexures/AnnexureC.pdf" target="_blank">(Annexure-C)</a> :
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl10Threeyrs" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            10
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            बैंकरों के नाम व पते (प्रत्येक बैंक का खाता सं. दें और <a href="../Document/Annexures/AnnexureF.pdf" target="_blank">(सलग्नक-एफ)</a> के अनुसार मूल बैंकर रिपोर्ट प्रस्तुत करे )<br />Name and address of the Bankers (Indicate Account No. with each bank and submit Bankers Report in Original as per proforma in <a href="../Document/Annexures/AnnexureF.pdf" target="_blank">Annexure-F</a>):
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl11Banker" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                           (11) अतिरिक्त्त / वैकल्पिक जानकारी <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Additional/Optional information
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            11.a
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                           कृपया बताएं कि क्या उत्पाद बी.आई.एस. मार्का है | यदि हां, तो वैध लाइसेंस की एक प्रति लगाएं  |<br />State if the products are BIS Marked. If so, attach a copy of valid BIS License. 
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl12aBis" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            11.b
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            कृपया बताएं कि क्या फर्म आई अस ओ प्रमाणन में है | यदि हां , प्रमाण पत्र संख्या और वैधता प्रदान करे|
|<br />State if the firm has ISO Certification. If yes, provide certification no. and validity.
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl12bIso" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                           (12) इस आवेदन के हस्ताक्षरकर्ता का नाम व पदनाम (जहां लागू हो, मुख्तारनामा की एक प्रति लगाएं)|<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name and designation of the signatory of this application(enclose a copy of Power of Attorney where applicable)
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            12.a
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            हस्ताक्षरकर्ता का नाम |<br />Name of the signatory of this application
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl13aSignatory" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            12.b
                        </td>
                        <td align="left" valign="top" class="leftHeadingBottom">
                            हस्ताक्षरकर्ता का पदनाम |<br />Designation of the signatory of this application
                        </td>
                        <td align="left" valign="top" class="rightHeadingBottom">
                            <asp:Label ID="lbl13bDesignation" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (3.c.a) स्वामी/साझेदारों/निदेशकों/कर्ता जिनका फर्म से हित है, के नाम, पते व टेलीफोन सं.<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Names, addresses and telephone Nos. of Proprietor/Partners/Directors/Karta having interest in the firm
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            (3.c.a.1)
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            नाम/ Name:
                        </td>
                        <td align="left" valign="top">
                                <asp:Label ID="lbl_3ca1Name" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पता/ Address:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:Label ID="lbl_3ca1Address" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पिन कोड/ Pin Code:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca1Pin" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            राज्य/ State:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblState_3ca1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                           ज़िला/शहर/District/ City:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblCity_3ca1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            फोन/ Telephone:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca1Tele" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            हिस्सेदारी/ Shareholding:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca1Share" runat="server"></asp:Label>
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
                            <asp:Label ID="lbl_3ca2Name" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पता/ Address:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:Label ID="lbl_3ca2Address" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पिन कोड/ Pin Code:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca2Pin" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            राज्य/ State:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblState_3ca2" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            ज़िला/शहर/District/ City:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblCity_3ca2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            फोन/ Telephone:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca2Tele" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            हिस्सेदारी/ Shareholding:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca2Share" runat="server"></asp:Label>
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
                            <asp:Label ID="lbl_3ca3Name" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पता/ Address:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:Label ID="lbl_3ca3Address" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पिन कोड/ Pin Code:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca3Pin" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            राज्य/ State:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblState_3ca3" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            ज़िला/शहर/District/ City:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblCity_3ca3" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            फोन/ Telephone:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca3Tele" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            हिस्सेदारी/ Shareholding:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca3Share" runat="server"></asp:Label>
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
                            <asp:Label ID="lbl_3ca4Name" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पता/ Address:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:Label ID="lbl_3ca4Address" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पिन कोड/ Pin Code:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca4Pin" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            राज्य/ State:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblState_3ca4" runat="server"></asp:Label> 
                            
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            ज़िला/शहर/District/ City:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblCity_3ca4" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            फोन/ Telephone:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca4Tele" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            हिस्सेदारी/ Shareholding:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca4Share" runat="server"></asp:Label>
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
                            <asp:Label ID="lbl_3ca5Name" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पता/ Address:
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:Label ID="lbl_3ca5Address" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            पिन कोड/ Pin Code:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca5Pin" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            राज्य/ State:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblState_3ca5" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                          ज़िला/शहर/District/ City:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblCity_3ca5" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            फोन/ Telephone:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca5Tele" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            हिस्सेदारी/ Shareholding:
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lbl_3ca5Share" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr align="right" valign="top">
                        <td align="left" valign="top" colspan="3">
                            <div align="left">
                               <%-- <div style="float:right;">
                                    Shows Changes
                                    <div class="clsLegends_Left_Deactivate" style="background-color:Red;float:right;"></div>
                                    <div style="float:right;">Uttam</div>
                                </div>--%>
                                  <table cellpadding="0" cellspacing="0" border="0">
                                  <tr>
                                    <td align="right"><b>Shows Changes: &nbsp;&nbsp;</b></td>
                                      
                                      <td><div class="clsLegends_Left_Deactivate" style="background-color:Red;float:right;">&nbsp;&nbsp;&nbsp;</div></td>
                                        <td><b>&nbsp;&nbsp;&nbsp;&nbsp;changes&nbsp;&nbsp;&nbsp;&nbsp;</b></td>
                                      <td><div class="clsLegends_Left_Deactivate" style="background-color:Blue;float:right;">&nbsp;&nbsp;&nbsp;</div></td>
                                      <td><b>&nbsp;&nbsp;&nbsp;&nbsp; New Added</b></td> 
                                      <td style="width:350px;" align="right" id="btn1" runat="server">
                                      <%--<asp:LinkButton ID="lnknextBottom" runat="server" CssClass="headerLink" 
                    style="padding-right:10px;" onclick="lnkNext_Click" > Next &lt;&lt;&lt;&lt; </asp:LinkButton>--%>
                                    <asp:Button ID="btnNext_bottom" runat="server" style="padding-right:10px; width:150px;" Text="Save&Continue" CssClass="btnCls" OnClick="lnkNext_Click" />
                                    </td>
                                  </tr>
                                  
                                  </table>
                            </div>
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
        
    </table>

</asp:Content>


