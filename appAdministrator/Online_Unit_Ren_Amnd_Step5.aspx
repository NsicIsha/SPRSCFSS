<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminUploadOnline.master" AutoEventWireup="true" CodeFile="Online_Unit_Ren_Amnd_Step5.aspx.cs" Inherits="appAdministrator_Online_Unit_Ren_Amnd_Step5" EnableEventValidation="false" ValidateRequest="false" %>

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
    <%--   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>

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
                    <td colspan ="4">
                         <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
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
                   <asp:GridView ID="GrdUpload" runat="server" Width="100%" DataKeyNames="Link"
                            AutoGenerateColumns="false" 
                            onrowdatabound="GrdUpload_RowDataBound" onrowcommand="GrdUpload_RowCommand"
                            >
                    <Columns >

                        <asp:TemplateField>
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>






                      <asp:BoundField DataField="fGPtbl_MD_DocumentName" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="true" Font-Italic="False" />
                      </asp:BoundField>

                         <asp:BoundField DataField="Size" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="true" Font-Italic="False" />
                      </asp:BoundField>

                        
                          <asp:BoundField DataField="Link" Visible="true"  ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide">
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
                        <td colspan="4" align="left" valign="top" style="height:10px;">
                            <asp:CheckBox ID="ckbDeclaration" runat="server" Checked="true" Enabled="false" Text="I will return original enlistment centificate issued by NSIC." />
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
            <td style="width:100%; text-align:left;" class="MandatoryMessage">
                <span class="MandatoryField">*</span> Mandatory Fields
            </td>
        </tr>
    </table>
</asp:Content>

