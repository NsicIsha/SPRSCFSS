<%@ Page Title="NSIC SPR :: New Unit Registration:Step 5" Language="C#" MasterPageFile="~/appMaster/AdminUpload.master" AutoEventWireup="true" CodeFile="NewUnitRegistrationStep5.aspx.cs" Inherits="appAdministrator_NewUnitRegistrationStep5" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">



    <%--<script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $("#ctl00_ContentPlaceHolder1_btnSubmit").click(function() {
                //TEXTBOX 1 & UPLOAD CONTROL 1
                var txtObj = document.getElementById("ctl00_ContentPlaceHolder1_TextBox1");
                var uplObj = document.getElementById("ctl00_ContentPlaceHolder1_FileUpload1");
                if (trim(txtObj.value) != "" && trim(txtObj.value) != "Document Name") {
                    if (trim(uplObj.value) == "") {
                        alert("You need to select document to upload");
                        uplObj.focus(); 
                        return false;
                    }
                }
                if (trim(uplObj.value) != "") {
                    if (trim(txtObj.value) == "" || trim(txtObj.value) == "Document Name") {
                        alert("Please enter name for document");
                        txtObj.focus();
                        return false;
                    }
                }

                //TEXTBOX 2 & UPLOAD CONTROL 2
                txtObj = document.getElementById("ctl00_ContentPlaceHolder1_TextBox2");
                uplObj = document.getElementById("ctl00_ContentPlaceHolder1_FileUpload2");
                if (trim(txtObj.value) != "" && trim(txtObj.value) != "Document Name") {
                    if (trim(uplObj.value) == "") {
                        alert("You need to select document to upload");
                        uplObj.focus();
                        return false;
                    }
                }
                if (trim(uplObj.value) != "") {
                    if (trim(txtObj.value) == "" || trim(txtObj.value) == "Document Name") {
                        alert("Please enter name for document");
                        txtObj.focus();
                        return false;
                    }
                }

                //TEXTBOX 3 & UPLOAD CONTROL 3
                txtObj = document.getElementById("ctl00_ContentPlaceHolder1_TextBox3");
                uplObj = document.getElementById("ctl00_ContentPlaceHolder1_FileUpload3");
                if (trim(txtObj.value) != "" && trim(txtObj.value) != "Document Name") {
                    if (trim(uplObj.value) == "") {
                        alert("You need to select document to upload");
                        uplObj.focus();
                        return false;
                    }
                }
                if (trim(uplObj.value) != "") {
                    if (trim(txtObj.value) == "" || trim(txtObj.value) == "Document Name") {
                        alert("Please enter name for document");
                        txtObj.focus();
                        return false;
                    }
                }

                //TEXTBOX 4 & UPLOAD CONTROL 4
                txtObj = document.getElementById("ctl00_ContentPlaceHolder1_TextBox4");
                uplObj = document.getElementById("ctl00_ContentPlaceHolder1_FileUpload4");
                if (trim(txtObj.value) != "" && trim(txtObj.value) != "Document Name") {
                    if (trim(uplObj.value) == "") {
                        alert("You need to select document to upload");
                        uplObj.focus();
                        return false;
                    }
                }
                if (trim(uplObj.value) != "") {
                    if (trim(txtObj.value) == "" || trim(txtObj.value) == "Document Name") {
                        alert("Please enter name for document");
                        txtObj.focus();
                        return false;
                    }
                }

                //TEXTBOX 5 & UPLOAD CONTROL 5
                txtObj = document.getElementById("ctl00_ContentPlaceHolder1_TextBox5");
                uplObj = document.getElementById("ctl00_ContentPlaceHolder1_FileUpload5");
                if (trim(txtObj.value) != "" && trim(txtObj.value) != "Document Name") {
                    if (trim(uplObj.value) == "") {
                        alert("You need to select document to upload");
                        uplObj.focus();
                        return false;
                    }
                }
                if (trim(uplObj.value) != "") {
                    if (trim(txtObj.value) == "" || trim(txtObj.value) == "Document Name") {
                        alert("Please enter name for document");
                        txtObj.focus();
                        return false;
                    }
                }

                //TEXTBOX 6 & UPLOAD CONTROL 6
                txtObj = document.getElementById("ctl00_ContentPlaceHolder1_TextBox6");
                uplObj = document.getElementById("ctl00_ContentPlaceHolder1_FileUpload6");
                if (trim(txtObj.value) != "" && trim(txtObj.value) != "Document Name") {
                    if (trim(uplObj.value) == "") {
                        alert("You need to select document to upload");
                        uplObj.focus();
                        return false;
                    }
                }
                if (trim(uplObj.value) != "") {
                    if (trim(txtObj.value) == "" || trim(txtObj.value) == "Document Name") {
                        alert("Please enter name for document");
                        txtObj.focus();
                        return false;
                    }
                }

                //TEXTBOX 7 & UPLOAD CONTROL 7
                txtObj = document.getElementById("ctl00_ContentPlaceHolder1_TextBox7");
                uplObj = document.getElementById("ctl00_ContentPlaceHolder1_FileUpload7");
                if (trim(txtObj.value) != "" && trim(txtObj.value) != "Document Name") {
                    if (trim(uplObj.value) == "") {
                        alert("You need to select document to upload");
                        uplObj.focus();
                        return false;
                    }
                }
                if (trim(uplObj.value) != "") {
                    if (trim(txtObj.value) == "" || trim(txtObj.value) == "Document Name") {
                        alert("Please enter name for document");
                        txtObj.focus();
                        return false;
                    }
                }

                //TEXTBOX 8 & UPLOAD CONTROL 8
                txtObj = document.getElementById("ctl00_ContentPlaceHolder1_TextBox8");
                uplObj = document.getElementById("ctl00_ContentPlaceHolder1_FileUpload8");
                if (trim(txtObj.value) != "" && trim(txtObj.value) != "Document Name") {
                    if (trim(uplObj.value) == "") {
                        alert("You need to select document to upload");
                        uplObj.focus();
                        return false;
                    }
                }
                if (trim(uplObj.value) != "") {
                    if (trim(txtObj.value) == "" || trim(txtObj.value) == "Document Name") {
                        alert("Please enter name for document");
                        txtObj.focus();
                        return false;
                    }
                }

                //TEXTBOX 9 & UPLOAD CONTROL 9
                txtObj = document.getElementById("ctl00_ContentPlaceHolder1_TextBox9");
                uplObj = document.getElementById("ctl00_ContentPlaceHolder1_FileUpload9");
                if (trim(txtObj.value) != "" && trim(txtObj.value) != "Document Name") {
                    if (trim(uplObj.value) == "") {
                        alert("You need to select document to upload");
                        uplObj.focus();
                        return false;
                    }
                }
                if (trim(uplObj.value) != "") {
                    if (trim(txtObj.value) == "" || trim(txtObj.value) == "Document Name") {
                        alert("Please enter name for document");
                        txtObj.focus();
                        return false;
                    }
                }

                //TEXTBOX 10 & UPLOAD CONTROL 10
                txtObj = document.getElementById("ctl00_ContentPlaceHolder1_TextBox10");
                uplObj = document.getElementById("ctl00_ContentPlaceHolder1_FileUpload10");
                if (trim(txtObj.value) != "" && trim(txtObj.value) != "Document Name") {
                    if (trim(uplObj.value) == "") {
                        alert("You need to select document to upload");
                        uplObj.focus();
                        return false;
                    }
                }
                if (trim(uplObj.value) != "") {
                    if (trim(txtObj.value) == "" || trim(txtObj.value) == "Document Name") {
                        alert("Please enter name for document");
                        txtObj.focus();
                        return false;
                    }
                }
            });
        });
    </script>--%>
    <script type="text/javascript" language="javascript">
//      function disableBackButton() {
//            window.history.forward();
//        }
//        setTimeout("disableBackButton()", 0);
        $(document).ready(function()
         {
            $("#progressbar").progressbar({ value: 75 });
            });
             
        function updateProgress() {            
            var value = $("#progressbar").progressbar("option", "100");
            if (value < 100) {
                $("#progressbar").progressbar("value", value + 1);                
            }
        }
               
    </script>
    <style type="text/css">
        .ErrMsgLbl
        {
        	font-weight:bold;
        	font-style:italic;
        	text-decoration:underline;
        	color:Red;
        	}
        .style5
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 35px;
            padding-left: 5px;
            padding-right: 5px;
            font-weight: bold;
        }
        ol.inlineList 
{ 
margin: 0px; 
} 
li.inlineList { 
border-style: none; 
padding-left: 2px; 
padding-right: 0px; 
padding-bottom: 0px; 
 
margin: 3px; 
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">
                New Unit Registration - Step 5
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
                         <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-3</b></td>
                          <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-4</b></td>
                           <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-5</b></td>
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
                        <td colspan="4" align="right" valign="top" class="rowStep">
                            STEP 5 of 7
                        </td>
                    </tr>
                
                    <tr align="left" valign="top">
                        <td colspan="4" align="left" valign="top" class="rowHeading">
                            UPLOAD REQUIRED DOCUMENTS
                        </td>
                    </tr>
                  <tr>
                    <td colspan="4" align ="center">
                          <span style="color: #FF0000; padding-top: 5px;">Only .jpg, .jpeg, .gif, .png, .pdf format are allowed.</span> <br />
 <span style="color: #FF0000; padding-top: 5px;">
 Size limit for .jpg, .jpeg, .gif, .png is 50KB and Size limit for .pdf is 1MB </span>
                        </td>
                    </tr>
                    <tr>
                    <td colspan ="4">
                   <asp:GridView ID="GrdUpload" runat="server" Width="100%" 
                            AutoGenerateColumns="false" 
                            onrowdatabound="GrdUpload_RowDataBound" onrowcommand="GrdUpload_RowCommand"
                            >
                    <Columns >
                      <asp:BoundField DataField="fGPtbl_MD_DocumentName" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="true" Font-Italic="False" />
                      </asp:BoundField>
                     <asp:TemplateField >
                                <ItemTemplate><table >
                                    <td align="left" valign="top" style="padding-bottom:10px; width:30%;">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="txt_bx" />

                                      <%--  <asp:RequiredFieldValidator ID="rfdPan" runat="server" Display="None" ValidationGroup="1"
                                            ControlToValidate="FileUpload1" ErrorMessage="Upload Scanned Copy of PAN Card"></asp:RequiredFieldValidator>--%>

                            <asp:RegularExpressionValidator
                                ID="RegularExpressionValidator1" runat="server" Display="None" ValidationGroup="1"
                                ErrorMessage="You can upload only .jpg, .jpeg, .gif, .png,  .pdf files"
                                ControlToValidate="FileUpload1" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|gif|GIF|png|PNG|pdf|PDF)$"
                            ></asp:RegularExpressionValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                TargetControlID="RegularExpressionValidator1"
                            >
                            </asp:ValidatorCalloutExtender>
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px; width:25%;">
                            <asp:Label ID="Label1" runat="server" CssClass="ErrMsgLbl" Visible="false"  Text="Document size can't be greater then 3 MB." ></asp:Label>
                              <asp:Button ID="btnBack" runat="server" text="Back" Visible="false" CssClass="btnCls" CausesValidation="false"  onclick="btn_back_click"  />
                        </td>
                        <td align="left" valign="top" style="padding-bottom:10px; width:15%;">
                          
                        
                         <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" 
                                CssClass="btnCls" TabIndex="37" CommandArgument='<%#Eval("fGPtbl_MD_DocumentName") %>'
                                ValidationGroup="1" Width="150px" CommandName="Upload"     Visible="false"  onclick="btnSubmit_Click"  /> <%-- onclick="btnSubmit_Click"--%>
                        </td>
                        </table>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                    
                    </Columns>
                    
                    <RowStyle BackColor="WhiteSmoke" BorderStyle="None" ForeColor="#333333" Height="5px" />
                        <EditRowStyle BackColor="#999999" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="SlateGray" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" BorderColor="#EAEAEA" 
                            BorderStyle="Dotted" BorderWidth="1px" ForeColor="Black" />
                    
                    </asp:GridView>

                    </td>
                    </tr>
                    
                     <tr align="left" valign="top">
                        <td colspan="4" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>    
                                                   
                    
                    
                    
                    
                 
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                        
                        </td>
                        <td align="left" valign="top" class="style5">
                        
                        </td>
                        <td align="left" valign="top">
                        
                        </td>
                    </tr>
                    <tr align="right" valign="top">
                        <td colspan="3">
                           
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
          <td align="left">
            <table cellpadding="2"  cellspacing="2" border="0" width="100%">
               <tr>
                  <td align="left" valign="top" >
                    <b>  Following documents are need to be submitted </b>
                  </td>
                  </tr>
                  <tr>
                  <td align="left" valign="top" style="padding-left:4px;">
                    <b> (A) Applicants should ensure enclosing the following documents in duplicate (attested by Ist Class Magistrate/Notary Public along with Application)   </b>
                  </td>
                  </tr>
                  <tr>
                  <td align="left" valign="top" style="padding-left:8px; ">
                    <p >
                    <ol  class="inlineList">
                       <li class="inlineList">Notarized copy of permanent SSI Certificate of SSI Unit.</li>
                       <li class="inlineList">Proof of ownership of Plant & Machinery and Raw Material (Cash Vouchers or Affidavit) showing date of purchase & Original Purchase Value (NOT DEPRECIATED) of individual machinery (Draft Affidavit at Annexure-‘B’). </li>
                         <li class="inlineList">Performance Statement as per Performa Annexure-G of the application. </li> 
                         <li class="inlineList">Notarized copy of ownership documents of the premises or copy of lease deed.</li>
                         <li class="inlineList">Certificate from the proprietor/partner/directors whether or not they have any link with large scale unit(s) registered with DGS&D/ISI/and/or other manufacturing unit(s) in case of their links with other unit(s), the details thereof to be specified</li>
                         </ol>
                          </p>
                  </td>
                  </tr>
                   <tr>
                  <td align="left" valign="top" style="padding-left:4px;">
                    <b> (B) Following documents/statements duly signed by the authorized person may be furnished:-   </b>
                  </td>
                  </tr>
                 <tr>
                  <td align="left" valign="top" style="padding-left:8px; ">
                    <p >
                    <ol  class="inlineList">
                       <li class="inlineList">List of technical personnel employed in production and services.</li>
                       <li class="inlineList">Item for which registration required with detailed specification. </li>
                         <li class="inlineList">Write-up on quality control measurement adopted by the firm for ensuring quality of raw material brought out item assembly and sub-assembly and in process and finished products quality control </li> 
                         <li class="inlineList">List of quality control equipment and testing facility available in factory.</li>
                         <li class="inlineList">Copy of type test report from Independent lab, where applicable as mentioned in relevant standard. </li>
                         <li class="inlineList">Latest Electricity Bill Copy. </li>
                         <li class="inlineList">Audited Balance Sheet, Trading Account and profit & Loss Account for the last 3 years duly signed by the authorized person under his seal.  </li>
                         <li class="inlineList">Statement showing the Results of Operation for the last 3years duly signed by authorized person under his seal (Draft at Annexure ‘C’). </li>
                          <li class="inlineList">Bankers’ Report giving details of financial status of the applicant firm as per Performa (Draft at Annexure ‘F’). </li>
                           <li class="inlineList">Copy of Permanent Account No. (PAN No.) </li>
                         </ol>
                          </p>
                  </td>
                  </tr>
                  
                   <tr>
                  <td align="left" valign="top" style="padding-left:4px;">
                    <b> 
(C) Documentary proof of the status of the firm:
   </b>
                  </td>
                  </tr>
                  <tr>
                     <td align="left" valign="top" style="padding-left:12px; ">
                        <b>
                             1. ADDITIONAL DOCUMENTS TO BE SUBMITTED IN CASE OF PARTNERSHIP CONCERN.
                        </b>
                        <p>
                            <ul >
                              <li style="list-style-type:Lower-roman;">  General Power of Attorney in favour of one of the partners.</li>
                               <li style="list-style-type:Lower-roman;">  Partnership Deed.</li>
                               <li style="list-style-type:Lower-roman;">  Form ‘A’ from Registrar of Firms showing the names of the partners.</li>
                            </ul>
                        </p>
                     </td>
                  </tr>
                  
                  <tr>
                     <td align="left" valign="top" style="padding-left:12px; ">
                        <b>
                            2. ADDITONAL DOCUMENTS TO BE SUBMITTED IN CASE OF LIMITED  COMPANIES.
                        </b>
                        <p>
                            <ul >
                              <li style="list-style-type:Lower-roman;">  Certificate of Incorporation duly authenticated.</li>
                               <li style="list-style-type:Lower-roman;">  Memorandum and Articles of Association duly authenticated.</li>
                               <li style="list-style-type:Lower-roman;"> Names of sitting Directors, their addresses and share & holdings. </li>
                                <li style="list-style-type:Lower-roman;"> Board Resolution in favour of the Signatory of the application and documents.  </li>
                            </ul>
                        </p>
                     </td>
                  </tr>
                    <tr>
                     <td align="left" valign="top" style="padding-left:12px; ">
                        <b>
                            3. ADDITIONAL DOUCMNETS TO BE SUBMITTED IN CASE OF COOPERATIVE SOCITIES
                        </b>
                        <p>
                            <ul >
                              <li style="list-style-type:Lower-roman;">  Certificate of Registration of Societies.</li>
                               <li style="list-style-type:Lower-roman;">  Society’s Bye-Laws/Regulations etc.</li>
                               <li style="list-style-type:Lower-roman;"> Names of Members, their addresses and share-holding.  </li>
                                <li style="list-style-type:Lower-roman;"> Current Certificate from Registrar of Societies that the Society is still functioning and its working is satisfactory.   </li>
                                <li style="list-style-type:Lower-roman;"> Details of authorized share capital and subscribed share capital.   </li>
                                <li style="list-style-type:Lower-roman;"> Details of movable as well as immovable property owned by the Society.    </li>
                                <li style="list-style-type:Lower-roman;"> Resolution of society of seeking registration under Government Purchase Program.    </li>
                                <li style="list-style-type:Lower-roman;"> Resolution in favour of Signatory of the application & documents.   </li>
                            </ul>
                        </p>
                     </td>
                  </tr>
                  
                  
            </table>
          </td>
        
        </tr>
           <tr>
                  <td align="left" valign="top" style="padding-left:4px;">
                    <b> (D) ADDITIONAL DOCUMENTS FOR PAINTS & VARNISH SUPPLIES TO RAILWAYS    </b>
                  </td>
                  </tr>
                  <tr>
                    <td align="left" valign="top" style="padding-left:8px; ">
                    <p >
                    
                      The firm manufacturing Paints shall approach to the General Manager, Integral Coach Factory. Perambur, Chennai for Paints for Coaching Stock and the Director General, Research Design & Standards Organization, Alam Bagh, Lucknow for Paints for Wagon/bridges and other applications for registration as approved suppliers for supply of Paints to Railways, after the units have been registered with NSIC for general supplies.  
                    
                    </p>
                    </td>
                  </tr>
                  <tr>
                  <td align="left" valign="top" style="padding-left:4px;">
                    <b> (E) OTHER INFORMATION    </b>
                  </td>
                  </tr>
                    <tr>
                  <td align="left" valign="top" style="padding-left:8px; ">
                    <p >
                    <ol  class="inlineList">
                       <li class="inlineList">Two copies of declaration signed by the applicant SSI Unit accepting conditions of registration as per Annexure D & E.</li>
                       <li class="inlineList">List of raw materials and finished goods in stock </li>
                         <li class="inlineList">Copy of BIS license, if applicable.</li> 
                         <li class="inlineList">Copy of ISO 9000 (Optional).</li>
                         <li class="inlineList">Copy of Registration Certificate if registered with DGS&D or other Govt. Organizations. </li>
                         <li class="inlineList">List of places where after sales services facilities (if applicable) are available. </li>
                         
                         </ol>
                          </p>
                  </td>
                  </tr>
                   <tr>
                  <td align="left" valign="top" style="padding-left:4px;">
                  Note:- At the time of renewal after two years only the documents at Sr. No. (A) 1, 3, (B) 7, 8, 10 and complete list of Plant and Machinery are required. 
                  </td>
                  </tr>
                  
                  
        <tr>
            <td style="width:100%; text-align:left;" class="MandatoryMessage">
                <span class="MandatoryField">*</span> Mandatory Fields
            </td>
        </tr>
    </table>
</asp:Content>