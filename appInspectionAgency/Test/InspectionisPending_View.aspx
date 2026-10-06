<%@ Page Title="View-Inspection is pending" Language="C#" MasterPageFile="~/appMaster/AgencyUpload.master" AutoEventWireup="true" CodeFile="InspectionisPending_View.aspx.cs" Inherits="appInspectionAgency_InspectionisPending_View" %>
<%@Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .ErrMsgLbl
        {
        	font-weight:bold;
        	font-style:italic;
        	text-decoration:underline;
        	color:Red;
        	}
    </style>
    
    <script language="javascript" type="text/javascript">
        var validFilesTypes = ["jpg", "jpeg", "gif", "png", "pdf"]
        function FunResetUpload() {
         //$("#ctl00_ContentPlaceHolder1_FileUpload1").val('');
            document.getElementById("ctl00_ContentPlaceHolder1_FileUpload1").value="";
            document.getElementById("ctl00_ContentPlaceHolder1_FileUpload1").focus();
        }

        function CheckExtension(file) {
//            var file = document.getElementById('<%=FileUpload1.ClientID%>');
//            alert(file);
//            var fileSize = file.files[0].size;
//            alert(fileSize);
            /*global document: false */
            var path = file.value;
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
            var isValidFile = false;
          
                    for (var i = 0; i < validFilesTypes.length; i++) 
                    {
                        if (ext == validFilesTypes[i]) 
                        {
                            isValidFile = true;
                            break;
                        }
                    }
                    if (!isValidFile) 
                    {
                        alert("Invalid File. " + "Valid extensions are:\n\n" + validFilesTypes.join(", "));
                    }
            return isValidFile;
        }


        function validateFileSize(file) 
        {
            /*global document: false */
            //getting size of the file

            var uploadedFile = document.getElementById("<%=FileUpload1.ClientID %>");


            alert(uploadedFile.files[0].size);
           // alert(file.size);
            
       var cc = file.files[0].size;
            var isValidFile = false;

            alert(fileSize);
            
          
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();

            if (ext == "pdf") {
                if (fileSize !== 0 && fileSize <= 10485760) {
                    isValidFile = true;
                }
            }
            else {
                if (fileSize !== 0 && fileSize <= 51200) {
                    isValidFile = true;
                }
            }
            
           
            if (!isValidFile) {
                alert("50 kb size allowed for Image and In case Pdf only 10 mb");
            }
            return isValidFile;
        }
         
         
         
         
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div align="left">
    
    <div align="left" class="headerAll">
        View Companies pending for Inspection
    </div>  
    <div align="left" style="height:20px;">
    </div> 
    
    <div align="left" style="padding-left:10px; padding-right:10px;">
        <table border="0" cellpadding="0" cellspacing="0" width="500px">
            <tr align="left" valign="top" style="padding-bottom:10px;">
                <td align="right" valign="top" colspan="3">
                    <asp:HiddenField ID="hdnuserid" runat="server" />
                    <asp:LinkButton ID="lbBack" runat="server" CssClass="headerLink" onclick="lbBack_Click"
                    style="padding-right:10px;" >&lt;&lt; BACK</asp:LinkButton>
                </td>
            </tr>
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading" style="width:110px;">
                    Type 
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top" style="width:320px;">
                    <asp:Label ID="lblType" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading">
                    Company 
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblCompany" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading">
                    SSI Registration No. 
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblRegno" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading">
                    PAN 
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblPan" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading">
                    Inspection date 
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblRequest" runat="server"></asp:Label>
                </td>
            </tr>
            <tr align="left" valign="top">
                <td colspan="3" style="height:20px;">
                    &nbsp;
                </td>
            </tr>
            <%if (uploadAction == 1) { %>
            
            <tr valign="top" align="left">
                <td align="left" valign="top" style="padding-bottom:10px;" colspan="3">
                    <asp:Label ID="Label1" runat="server" CssClass="ErrMsgLbl"></asp:Label>
                </td>
             </tr>

             <tr align="left" valign="top" style="color:red">
                <td align="left" class="modalTDtextBOLDS"  colspan="3" style="padding:10px" >
                   Instruction for Uploading Inspection Report <br />

                    <b> &#x2022; .pdf  file are allowed.</b>
                    <br />
                     <b>&#x2022;  Size limit for .pdf file is maximum 20 MB</b>
                </td>
                
            </tr>
            
            <tr align="left" valign="top">
                <td align="left" class="modalTDtextBOLD">
                    Upload Report:
                </td>
                <td align="left" valign="top" style="padding-bottom:10px;">
                    <asp:FileUpload ID="FileUpload1" runat="server"                   
                     CssClass="txt_bx" />

                    <asp:RequiredFieldValidator ValidationGroup="UPIA"  ID="RFDFileUpload1" Display="Dynamic" runat="server" ControlToValidate="FileUpload1" ErrorMessage="Inspection Report Required"></asp:RequiredFieldValidator>


                    <%-- <br />  <br />
                     <span style="color: #FF0000; padding-top: 5px;">Only  .pdf format are allowed.</span> <br />
                    <span style="color: #FF0000; padding-top: 5px;"> Maximum Size limit for .pdf is 20 MB</span>
                      --%><%-- <br />  <br />
                     <span style="color: #FF0000; padding-top: 5px;">Only  .pdf format are allowed.</span> <br />
                    <span style="color: #FF0000; padding-top: 5px;"> Maximum Size limit for .pdf is 20 MB</span>
                      --%>
                </td>
                <td align="left" class="modalTDtextBOLD">
                     <asp:Button ID="btnInspectionReport" UseSubmitBehavior="false" runat="server" 
                            Text="Upload Inspection Report" CssClass="btnCls" Width="200px" 
                            ValidationGroup="UPIA" onclick="btnInspectionReport_Click" />
                </td>
            </tr>
            
            


            <tr align="left" valign="top" id="trUploadedDocument" runat="server" visible="false">
                <td align="left" class="modalTDtextBOLD">
                    &nbsp;</td>
                <td align="left" valign="top" style="padding-bottom:10px;" colspan="2">
                    <asp:HyperLink ID="HLName" runat="server" Visible="false"  ForeColor="Red" Font-Bold="true"   Text="View Uploaded Inspection Report" Target="_blank"></asp:HyperLink>
                    &nbsp;</td>
                
            </tr>
            
            


            <tr align="center" valign="top">
                <td align="center" class="modalTDtextBOLDS"  colspan="3"  style="padding:10px">
                 <b style="font-weight:bold;font-size:large">   Upload Geotag Image</b></td>
                
            </tr>

            <tr align="left" valign="top" style="color:red">
                <td align="left" class="modalTDtextBOLDS"  colspan="3"  style="padding:10px">
                   Instruction for Geotag Image <br />

                    <b> &#x2022; Only .jpg, .jpeg, .png  format are allowed.</b>
                    <br />
                     <b>&#x2022;  Size limit for each image .jpg, .jpeg, .png is 2.5 MB</b>
                </td>
                
            </tr>
            
            


            <tr align="left" valign="top" >
                <td align="left" class="modalTDtextBOLD" colspan="1" style="padding:5px">
                    Image 1</td>
                <td align="left" valign="top" style="padding:5px">
                     <asp:FileUpload ID="FileUpload2" runat="server"                   
                     CssClass="txt_bx" />
                    <asp:RequiredFieldValidator ValidationGroup="UPGTImage1"  ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ControlToValidate="FileUpload2" ErrorMessage="Please Upload valid Geotag Image"></asp:RequiredFieldValidator>
                    </td>

                <td align="left" class="modalTDtextBOLD" colspan="1" style="padding:5px">
                    <asp:Button ID="btnGeoTagImg1" UseSubmitBehavior="false" runat="server" 
                            Text="Upload Image" CssClass="btnCls" Width="200px" 
                            ValidationGroup="UPGTImage1" onclick="btnGeoTagImg1_Click"  />
                    </td>
            </tr>

            <tr align="left" valign="top" id="trgeotagimag1" runat="server" visible="false">
                <td align="left" class="modalTDtextBOLD" colspan="1" style="padding:5px">
                   </td>
                <td align="left" valign="top" style="padding:5px;" colspan="2" >
                    <asp:HyperLink ID="btnGeoTag1" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"   Text="View Uploaded Inspection Report" Target="_blank"></asp:HyperLink>
                    </td>
            </tr>
            
            


            <tr align="left" valign="top">
                <td align="left" class="modalTDtextBOLD" style="padding:5px">
                    Image 2</td>
                <td align="left" valign="top" style="padding:5px">
                     <asp:FileUpload ID="FileUpload3" runat="server"                   
                     CssClass="txt_bx" />
                    <asp:RequiredFieldValidator ValidationGroup="UPGTImage2"  ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ControlToValidate="FileUpload3" ErrorMessage="Please Upload valid Geotag Image"></asp:RequiredFieldValidator>
                </td>
                 <td align="left" class="modalTDtextBOLD" style="padding:5px">
                      <asp:Button ID="Button3" UseSubmitBehavior="false" runat="server" 
                            Text="Upload Image" CssClass="btnCls" Width="200px" 
                            ValidationGroup="UPGTImage2" onclick="btnGeoTagImg2_Click"  />
                     </td>
            </tr>

            <tr align="left" valign="top" id="trgeotagimag2" runat="server" visible="false">
                <td align="left" class="modalTDtextBOLD" colspan="1" style="padding:5px">
                   </td>
                <td align="left" valign="top" style="padding:5px;" colspan="2" >
                    <asp:HyperLink ID="btnGeoTag2" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"   Text="View Uploaded Inspection Report" Target="_blank"></asp:HyperLink>
                    </td>
            </tr>
            
            


            <tr align="left" valign="top">
                <td align="left" class="modalTDtextBOLD" style="padding:5px">
                    Image 3</td>
                <td align="left" valign="top" style="padding:5px">
                     <asp:FileUpload ID="FileUpload4" runat="server"                   
                     CssClass="txt_bx" />
                     <asp:RequiredFieldValidator ValidationGroup="FileUpload4"  ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ControlToValidate="FileUpload4" ErrorMessage="Please Upload valid Geotag Image"></asp:RequiredFieldValidator>
                    </td>
                 <td align="left" class="modalTDtextBOLD" style="padding:5px">
                     <asp:Button ID="Button4" UseSubmitBehavior="false" runat="server" 
                            Text="Upload Image" CssClass="btnCls" Width="200px" 
                            ValidationGroup="FileUpload4" onclick="btnGeoTagImg3_Click" />
                    </td>
            </tr>

             <tr align="left" valign="top" id="trgeotagimag3" runat="server" visible="false">
                <td align="left" class="modalTDtextBOLD" colspan="1" style="padding:5px">
                   </td>
                <td align="left" valign="top" style="padding:5px;" colspan="2" >
                    <asp:HyperLink ID="btnGeoTag3" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"   Text="View Uploaded Inspection Report" Target="_blank"></asp:HyperLink>
                    </td>
            </tr>
            <tr align="left" valign="top">
                <td align="left" class="modalTDtextBOLD" style="padding:5px">
                    Image 4</td>
                <td align="left" valign="top" style="padding:5px">
                     <asp:FileUpload ID="FileUpload5" runat="server"                   
                     CssClass="txt_bx" />
                    <asp:RequiredFieldValidator ValidationGroup="UPGTImage4"  ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ControlToValidate="FileUpload5" ErrorMessage="Please Upload valid Geotag Image"></asp:RequiredFieldValidator>

                    </td>

                 <td align="left" class="modalTDtextBOLD" style="padding:5px">
                     <asp:Button ID="Button5" UseSubmitBehavior="false" runat="server" 
                            Text="Upload Image" CssClass="btnCls" Width="200px" 
                            ValidationGroup="UPGTImage4" onclick="btnGeoTagImg4_Click" />
                    </td>

            </tr>

             <tr align="left" valign="top" id="trgeotagimag4" runat="server" visible="false">
                <td align="left" class="modalTDtextBOLD" colspan="1" style="padding:5px">
                   </td>
                <td align="left" valign="top" style="padding:5px;" colspan="2" >
                    <asp:HyperLink ID="btnGeoTag4" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"   Text="View Uploaded Inspection Report" Target="_blank"></asp:HyperLink>
                    </td>
            </tr>
            
            


            <tr align="left" valign="top">
                <td align="left" class="modalTDtextBOLD" style="padding:5px">
                    Image 5</td>
                <td align="left" valign="top" style="padding:5px">
                     <asp:FileUpload ID="FileUpload6" runat="server"                   
                     CssClass="txt_bx" />
                    <asp:RequiredFieldValidator ValidationGroup="UPGTImage5"  ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ControlToValidate="FileUpload6" ErrorMessage="Please Upload valid Geotag Image"></asp:RequiredFieldValidator>
                     </td>
                 <td align="left" class="modalTDtextBOLD" style="padding:5px">
                     <asp:Button ID="Button6" UseSubmitBehavior="false" runat="server" 
                            Text="Upload Image" CssClass="btnCls" Width="200px" 
                            ValidationGroup="UPGTImage5"  onclick="btnGeoTagImg5_Click" />
                    </td>
            </tr>
            
             <tr align="left" valign="top" id="trgeotagimag5" runat="server" visible="false">
                <td align="left" class="modalTDtextBOLD" colspan="1" style="padding:5px">
                   </td>
                <td align="left" valign="top" style="padding:5px;" colspan="2" >
                    <asp:HyperLink ID="btnGeoTag5" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"   Text="View Uploaded Inspection Report" Target="_blank"></asp:HyperLink>
                    </td>
            </tr>


            <tr align="left" valign="top">
                <td align="left" class="modalTDtextBOLD" style="padding:5px">
                    Image 6</td>
                <td align="left" valign="top" style="padding:5px">
                     <asp:FileUpload ID="FileUpload7" runat="server"                   
                     CssClass="txt_bx" />
                    </td>
                 <td align="left" class="modalTDtextBOLD" style="padding:5px">
                     <asp:Button ID="Button7" UseSubmitBehavior="false" runat="server" 
                            Text="Upload Image" CssClass="btnCls" Width="200px" 
                            ValidationGroup="Up7"  onclick="btnGeoTagImg6_Click"/>
                   </td>
            </tr>
            
            <tr align="left" valign="top" id="trgeotagimag6" runat="server" visible="false">
                <td align="left" class="modalTDtextBOLD" colspan="1" style="padding:5px">
                   </td>
                <td align="left" valign="top" style="padding:5px;" colspan="2" >
                    <asp:HyperLink ID="btnGeoTag6" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"   Text="View Uploaded Inspection Report" Target="_blank"></asp:HyperLink>
                    </td>
            </tr>


            <tr align="left" valign="top">
                <td align="left" class="modalTDtextBOLD" style="padding:5px">
                    Image 7</td>
                <td align="left" valign="top" style="padding:5px">
                     <asp:FileUpload ID="FileUpload8" runat="server"                   
                     CssClass="txt_bx" />
                    </td>
                 <td align="left" class="modalTDtextBOLD" style="padding:5px">
                     <asp:Button ID="Button8" UseSubmitBehavior="false" runat="server" 
                            Text="Upload Image" CssClass="btnCls" Width="200px" 
                            ValidationGroup="Up8"  onclick="btnGeoTagImg7_Click"/>
                    </td>
            </tr>
            
             <tr align="left" valign="top" id="trgeotagimag7" runat="server" visible="false">
                <td align="left" class="modalTDtextBOLD" colspan="1" style="padding:5px">
                   </td>
                <td align="left" valign="top" style="padding:5px;" colspan="2" >
                    <asp:HyperLink ID="btnGeoTag7" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"   Text="View Uploaded Inspection Report" Target="_blank"></asp:HyperLink>
                    </td>
            </tr>


            <tr align="left" valign="top">
                <td align="left" class="modalTDtextBOLD" style="padding:5px" >
                    Image 8</td>
                <td align="left" valign="top" style="padding:5px">
                     <asp:FileUpload ID="FileUpload9" runat="server"                   
                     CssClass="txt_bx" />
                    </td>
                 <td align="left" class="modalTDtextBOLD" style="padding:5px">
                     <asp:Button ID="Button9" UseSubmitBehavior="false" runat="server" 
                            Text="Upload Image" CssClass="btnCls" Width="200px" 
                            ValidationGroup="Up9"  onclick="btnGeoTagImg8_Click" />
                    </td>
            </tr>

            <tr align="left" valign="top" id="trgeotagimag8" runat="server" visible="false">
                <td align="left" class="modalTDtextBOLD" colspan="1" style="padding:5px">
                   </td>
                <td align="left" valign="top" style="padding:5px;" colspan="2" >
                    <asp:HyperLink ID="btnGeoTag8" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"   Text="View Uploaded Inspection Report" Target="_blank"></asp:HyperLink>
                    </td>
            </tr>

            <tr align="left" valign="top">
                <td align="left" class="modalTDtextBOLD" >
                     Inspection Video:
                </td>
                <td align="left" valign="top" style="padding-bottom:10px;">
                    <asp:FileUpload ID="FileUpload10" runat="server"                   
                     CssClass="txt_bx" />

             
                    <asp:RequiredFieldValidator ValidationGroup="1"  ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="FileUpload10" ErrorMessage="Inspection Video Required"></asp:RequiredFieldValidator>

                   <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator7"  
                     runat="server" ControlToValidate="fileUploadVideo"  
                     ErrorMessage="Only .avi, .mpg, .wav, .mid, .wmv, .asf and .mpeg Video formats are allowed." ForeColor="Red"  
                     ValidationExpression="/^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.avi|.AVI|.WMV|.wmv|.wav|.WAV|.mpg|.MPG|.mid|.MID|.asf|.ASF|.mpeg|.MPEG)$/"  
                     ValidationGroup="PartnerProfileUpdate" SetFocusOnError="true"></asp:RegularExpressionValidator>  
                    --%>
                    </td>

                <td align="left" class="modalTDtextBOLD">
                    <asp:Button ID="Button10" UseSubmitBehavior="false" runat="server" 
                            Text="Upload Image" CssClass="btnCls" Width="200px" 
                            ValidationGroup="Up10"  onclick="btnVideoUpload_Click" />
                    </td>
            </tr>
             <tr align="left" valign="top" id="trVideo9" runat="server" visible="false">
                <td align="left" class="modalTDtextBOLD" colspan="1" style="padding:5px">
                   </td>
                <td align="left" valign="top" style="padding:5px;" colspan="2" >
                    <asp:HyperLink ID="btnGeoTag9" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"   Text="View Uploaded Inspection Report" Target="_blank"></asp:HyperLink>
                    </td>
            </tr>

                
            <tr align="left" valign="top">
                <td align="left" class="modalTDtextBOLD" colspan="2">
                    Inspection Status:
                </td>
                <td align="left" valign="top" style="padding-bottom:10px;">
                <asp:RadioButtonList AutoPostBack="true" ID="rdbInspectionStatus" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbInspectionStatus_SelectedIndexChanged">
                    <asp:ListItem Value="1" Selected="True" Text="Recomended"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Not Recomended"></asp:ListItem>
                </asp:RadioButtonList>
                </td>
            </tr>
              <tr align="left" valign="top" runat="server" visible="false" id="trRemarks">
                <td align="left" class="modalTDtextBOLD" colspan="2">
                    Remarks:
                </td>
                <td align="left" valign="top" style="padding-bottom:10px;">
               <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server"> </asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="1"  ID="Rfdremarks" Display="Dynamic" runat="server" ControlToValidate="txtRemarks" ErrorMessage="Remarks Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            
                <tr align="left" valign="top">
                    <td align="left" style="font-weight: bold;" colspan="2">
                        &nbsp;</td>
                    <td align="left" class="modalTDbutton">
                        <asp:Button ID="btnSubmitUpload" UseSubmitBehavior="false" runat="server" 
                            Text="Submit" CssClass="btnCls" 
                            ValidationGroup="1" onclick="btnSubmitUpload_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="btnResetEdit" type="button" value="Reset" class="btnCls" onclick="FunResetUpload();" />
                    </td>
                </tr>


            <tr align="center" valign="top">
                    <td align="left" style="font-weight: bold;" colspan="3">
                        <asp:Label ID="lblMsgbtn" runat="server" ForeColor="Red" Text="Submit button will be Visible only if you will upload valid Inspection Report, atleast 5 Geotag Images and Video"></asp:Label>
                    </td>
                     
                </tr>


             <%} %>
            
        </table>
    </div> 
    
</div>
</asp:Content>

