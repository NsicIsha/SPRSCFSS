<%@ Page Title=" SPR:Check Monetary Limit By User " Language="C#" MasterPageFile="~/appMaster/AdminUpload.master" AutoEventWireup="true" CodeFile="CheckMonetryLimitbyUser.aspx.cs" Inherits="appAdministrator_CheckMonetryLimitbyUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function setlabelvalue() {
            var filename = document.getElementById('<%=ftp_upload.ClientID %>').value;
            alert(filename);
            var lastIndex = filename.lastIndexOf("\\");

            if (lastIndex >= 0) {
                filename = filename.substring(lastIndex + 1);
            }

        }
        function FetchLabel(button) {
            document.getElementById("td_submitbutton").style.display = "block";
            var row = button.parentNode.parentNode;
            var label = GetChildControl(row, "lbl_Company_idtest");

            var id = label.innerHTML;


            document.getElementById("<%=lbl_hidden_companyid.ClientID %>").innerHTML = id;


            document.getElementById("<%=hdn_CompanyId.ClientID %>").value = id;

            var k = 1;

            if (id != 0) {
                document.getElementById("nsicradio").style.display = "block";
                document.getElementById("inspection_agencytable_tr").style.display = "block";


            }
            else {
                document.getElementById("nsicradio").style.display = "None";
                document.getElementById("inspection_agencytable_tr").style.display = "None";


            }
            return true;
        };
        function GetChildControl(element, id) {
            var child_elements = element.getElementsByTagName("*");
            for (var i = 0; i < child_elements.length; i++) {
                if (child_elements[i].id.indexOf(id) != -1) {
                    return child_elements[i];
                }
            }
        };


        function getSelectedListItem(radioButtonList) {
            var k = 1;
            var R = 1;
            // var t1 = 0;fdsf

            k = $('#<%=rdl_Nsic.ClientID %> input[type=radio]:checked').val();
            R = $('#<%=rdl_inspaction.ClientID %> input[type=radio]:checked').val()

            document.getElementById("<%=txt_last_date_visited.ClientID%>").value = "";
            document.getElementById("td_submitbutton").style.display = "block";

            if (k == 1 && R == 1) {
                document.getElementById("last_inspection_date").style.display = "block";
                document.getElementById("upload_file_path").style.display = "none";
                document.getElementById("td_submitbutton").style.display = "block";
            }
            else if (k == 1 && R == 2) {
                document.getElementById("last_inspection_date").style.display = "block";
                document.getElementById("upload_file_path").style.display = "none";
                document.getElementById("td_submitbutton").style.display = "block";
            }
            else if (k == 2 && R == 1) {
                document.getElementById("last_inspection_date").style.display = "block";
                document.getElementById("upload_file_path").style.display = "none";
                document.getElementById("td_submitbutton").style.display = "block";
            }
            else if (k == 2 && R == 2) {
                document.getElementById("last_inspection_date").style.display = "none";
                document.getElementById("upload_file_path").style.display = "block";
                document.getElementById("td_submitbutton").style.display = "block";
            }
        }

    </script>
    <style type="text/css">
        .hiddentext {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table cellpadding="2" cellspacing="2" border="0">
        <tr>
            <td align="left" valign="top" class="headerAll">Renewal Monetary Limit >  Rs. 10 crores
       
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="height: 10px;"></td>

        </tr>
        <tr>
            <td align="left" valign="top">
                <table cellpadding="2" cellspacing="2" border="0" width="750px">
                    <tr>
                        <td align="left" valign="top">
                            <asp:GridView ID="grd_checkMonetryLimit" runat="server"
                                AutoGenerateColumns="false" Width="100%"
                                BorderWidth="0" CellPadding="0" CellSpacing="1"
                                AllowPaging="true" PageIndex="0" AllowSorting="true"
                                PageSize="10"
                                OnPageIndexChanging="grd_checkMonetryLimit_PageIndexChanging"
                                OnRowCommand="grd_checkMonetryLimit_RowCommand" OnRowDataBound="grd_checkMonetryLimit_RowDataBound">
                                <HeaderStyle CssClass="dynRowHeader" />
                                <RowStyle CssClass="dynRowEven" />
                                <AlternatingRowStyle CssClass="dynRowOdd" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Company_Name" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                            <asp:Label ID="lbl_Company_idtest" runat="server" Text='<%#Eval("CompanyId") %>' CssClass="hiddentext"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAN No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Pan_no" runat="server" Text='<%#Eval("CompanyPAN") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monetary Limit  (in Lacs)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Monetrylimit" runat="server" Text='<%#Eval("MonetryLimit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <%--<asp:Button ID="imgEdit" runat="server" Text="Accept" ToolTip="Accept" CommandName="edt" CommandArgument='<%#Eval("CompanyId") %>' CausesValidation="false" UseSubmitBehavior="false" CssClass="btnCls" OnClientClick="javascript:return FetchLabel(this);" />--%>

                                            <asp:Button ID="imgEdit" runat="server" Text="Accept" ToolTip="Accept" CommandName="edt" CommandArgument='<%#Eval("CompanyId") %>' CausesValidation="false" UseSubmitBehavior="false"  CssClass="btnCls" />


                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="imbButton_edit" runat="server" Text="View" ToolTip="View Form" Target="_blank"></asp:HyperLink>
                                            <asp:Label ID="hdn_Company_id" runat="server" Text='<%#Eval("CompanyId") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No data found!
                                </EmptyDataTemplate>
                            </asp:GridView>
                        
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <table cellpadding="3" cellspacing="3" border="0" id="nsicradio" style="display: none;">
                                <tr>
                                    <td align="left" valign="middle">1. NSIC Inspection/ Visit in last 1 Year
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:RadioButtonList ID="rdl_Nsic" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Table" AutoPostBack="false" onclick="javascript:getSelectedListItem(this);">
                                            <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="middle">2. Inspection in Last 1 Year
                      
                                    </td>

                                    <td align="left" valign="top">
                                        <asp:RadioButtonList ID="rdl_inspaction" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Table" AutoPostBack="false" onclick="javascript:getSelectedListItem(this);">
                                            <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="2"></asp:ListItem>

                                        </asp:RadioButtonList>

                                    </td>


                                </tr>

                            </table>


                        </td>

                    </tr>



                    <tr>
                        <td align="left" valign="top">
                            <table cellspacing="3" cellpadding="4" border="0" id="inspection_agencytable_tr" style="display: none;">
                                <tr id="last_inspection_date" style="display: block;">
                                    <td align="left" valign="top">Last Inspection / Visit was done on
                                    
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txt_last_date_visited" runat="server" Text=""></asp:TextBox>
                                        <asp:CalendarExtender ID="CExtendertxt_22" Format="MM/dd/yyyy" runat="server" PopupButtonID="txt_22"
                                            TargetControlID="txt_last_date_visited">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr id="upload_file_path" style="display: none;">
                                    <td align="left" valign="top">Uplaod NSIC/ Inspection Report
                                    </td>
                                    <td align="left" valign="top">


                                        <asp:FileUpload ID="ftp_upload" runat="server" CssClass="txt_bx" />
                                        <%--onchange="javascript:setlabelvalue();"--%>
                                        <asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator1" runat="server" Display="None" ValidationGroup="1"
                                            ErrorMessage="You can upload only .jpg, .jpeg, .gif, .png, .doc, .docx, .xls, .xlsx, .pdf, .txt files"
                                            ControlToValidate="ftp_upload" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|gif|GIF|png|PNG|doc|DOC|docx|DOCX|xls|XLSX|pdf|PDF|txt|TXT)$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblFileName" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                                        <%--CssClass="hiddentext"--%>
                                    </td>

                                </tr>


                              <%--  Payment Details Capture --%>

                                 <tr>
     <td align="left" colspan="2" class="header">NSIC Payment Details: 
     </td>
 </tr>


                                 
  
  
 <tr align="left" valign="top">
     <td colspan="2" align="left" valign="top" class="style1">Select Payment Option
     </td>
 </tr>
 <tr align="right" valign="top">
     <td colspan="2" align="center" class="style2">
         <asp:RadioButtonList ID="radPay" runat="server" RepeatDirection="Horizontal"
             Width="70%">
              
             <asp:ListItem Text="DD" Value="2"></asp:ListItem>
             <asp:ListItem Text="Cheque" Value="3"></asp:ListItem>
             <asp:ListItem Text="Online" Selected="True" Value="4"></asp:ListItem>
         </asp:RadioButtonList>
     </td>
 </tr>




  
 <tr>
     <td colspan="3" align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
         <table border="0" cellpadding="0" cellspacing="0" width="100%">
             <tr>
                 <td>
                      
                         <table width="80%">
                             <tr>
                                 <td class="rowStep" colspan="2">
                                     <b>CHEQUE / DD / ONLINE PAYMENT DETAILS </b>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <b>Cheque / DD No. / UTR or IMPS No.</b>
                                 </td>
                                 <td align="left" valign="top" style="padding-bottom: 10px;">
                                     <asp:TextBox ID="txt_chqddNo" runat="server" CssClass="txt_bx"
                                         ValidationGroup="1" MaxLength="50">
                                     </asp:TextBox>

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                         ErrorMessage="Please Enter Cheque or DD Number or UTR or IMPS No."
                                         ControlToValidate="txt_chqddNo"
                                         SetFocusOnError="true"
                                         >
                                     </asp:RequiredFieldValidator>
                                     


                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <b>Bank Name</b>
                                 </td>
                                 <td align="left" valign="top" style="padding-bottom: 10px;">
                                     <asp:TextBox ID="txt_BankName" runat="server" CssClass="txt_bx"
                                          MaxLength="50">
                                     </asp:TextBox>

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                         ErrorMessage="Please Enter Bank Name"
                                         ControlToValidate="txt_BankName"
                                         SetFocusOnError="true"
                                          >
                                     </asp:RequiredFieldValidator>
                                     

                                 </td>
                             </tr>

                             <tr>
                                 <td>
                                     <b>Bank Branch</b>
                                 </td>
                                 <td align="left" valign="top" style="padding-bottom: 10px;">
                                     <asp:TextBox ID="txt_BankBranch" runat="server" CssClass="txt_bx"
                                           MaxLength="50">
                                     </asp:TextBox>

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                         ErrorMessage="Please Enter Bank Branch"
                                         ControlToValidate="txt_BankBranch"
                                         SetFocusOnError="true"
                                          >
                                     </asp:RequiredFieldValidator>
                                    

                                 </td>
                             </tr>

                             <tr>
                                 <td>
                                     <b>Payable At</b>
                                 </td>
                                 <td align="left" valign="top" style="padding-bottom: 10px;">
                                     <asp:TextBox ID="txt_PayableAt" runat="server" CssClass="txt_bx"
                                          MaxLength="50">
                                     </asp:TextBox>



                                 </td>
                             </tr>

                             <tr>
                                 <td>
                                     <b>Cheque / DD / UTR or IMPS Amount <span class="MandatoryField">(With Service Tax)</span></b>
                                 </td>
                                 <td align="left" valign="top" style="padding-bottom: 10px;">
                                     <asp:TextBox ID="txt_chqddAmnt" runat="server" CssClass="txt_bx"
                                          MaxLength="50" > <%--ReadOnly="true"--%>
                                     </asp:TextBox>
                                     <asp:RequiredFieldValidator   ID="RequiredFieldValidator19" runat="server" ErrorMessage="Please enter Amount" ControlToValidate="txt_chqddAmnt" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                     <asp:RegularExpressionValidator  ID="RFVStxtAmount" runat="server" ControlToValidate="txt_chqddAmnt"  ValidationExpression="^\d+$" ErrorMessage="Please enter a valid whole amount." ForeColor="Red" Display="Dynamic"> </asp:RegularExpressionValidator>


                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <b>Cheque / DD / UTR or IMPS Date</b>
                                 </td>
                                 <td align="left" valign="top" style="padding-bottom: 10px;">
                                     <asp:TextBox onkeydown="return false;" onpaste="return false;" ID="txt_chqddate" runat="server" CssClass="txt_bx"
                                         ValidationGroup="1" MaxLength="15">
                                     </asp:TextBox> 
                                     <asp:RequiredFieldValidator ID="RFValidatortxt_chqddate" runat="server" ErrorMessage="Please enter date" ControlToValidate="txt_chqddate"
                                         Display="None" SetFocusOnError="true">
                                     </asp:RequiredFieldValidator>
                                    <%-- <asp:ValidatorCalloutExtender ID="VCExtendertxt_chqddate" runat="server"
                                         TargetControlID="RFValidatortxt_chqddate">
                                     </asp:ValidatorCalloutExtender> --%>
                                     <asp:ImageButton OnClientClick="return false;" ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                     <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_chqddate" Format="MM/dd/yyyy" PopupButtonID="imgbtnCalendar" runat="server" />


                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <b>Upload Scanned Copy of Cheque / DD </b>
                                 </td>
                                 <td align="left">
                                     <asp:FileUpload ID="FileUpload1" runat="server" />
                                     <asp:Label ID="Label2" runat="server" CssClass="ErrMsgLbl"></asp:Label>

                                 </td>
                             </tr>


                             <tr>
                                 <td>
                                     <b>Payment Remarks </b>
                                 </td>
                                 <td align="left">
                                     <asp:TextBox ID="txtChequeDDPaymentRemarks" runat="server" CssClass="txt_bx" TextMode="MultiLine"
                                          MaxLength="500" ReadOnly="false">
                                     </asp:TextBox>

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true"
                                           runat="server" ErrorMessage="Please enter remarks" ControlToValidate="txtChequeDDPaymentRemarks"
                                         Display="Dynamic">
                                     </asp:RequiredFieldValidator>




                                 </td>
                             </tr>

                             <tr>
                                 <td><b>Bank Details </b></td>
                                 <td>
                                     <%-- CssClass="clsDDL" --%>
                                     <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged"></asp:DropDownList>
                                     <asp:HiddenField ID="hdnBranchID" runat="server" />
                                     <asp:HiddenField ID="hdnAccountNumber" runat="server" />
                                     <asp:HiddenField ID="hdnBankName" runat="server" />
                                     <asp:HiddenField ID="hdnIFSCCode" runat="server" />
                                     <asp:HiddenField ID="hdnBranchName" runat="server" />
                                     <asp:HiddenField ID="hdnLedgerID" runat="server" />
                                     <asp:HiddenField ID="hdnLedgerName" runat="server" />
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true"
                                           runat="server" ErrorMessage="Please Select Bank" ControlToValidate="ddlBank" InitialValue="0"
                                         Display="Dynamic">
                                     </asp:RequiredFieldValidator>

                                     

                                 </td>
                             </tr


                              
                         </table>
                    
                 </td>
             </tr>





         </table>
     </td>
 </tr>


                            </table>
                        </td>

                    </tr>

                </table>

            </td>

        </tr>

        <tr>
            <td align="left" valign="top" id="td_submitbutton" style="display: none;">


                <asp:Button ID="btn_submit" runat="server" Text="Submit"
                    CssClass="btnCls" OnClick="btn_submit_Click" />
                <%--UseSubmitBehavior="false"--%>
                <asp:Label ID="lbl_hidden_companyid" runat="server" Text="" CssClass="hiddentext"></asp:Label>
                <asp:HiddenField ID="hdn_CompanyId" runat="server" />
            </td>
        </tr>

         <tr>
     <td colspan="2" align="center">
         <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
     </td>
 </tr>


    </table>




</asp:Content>

