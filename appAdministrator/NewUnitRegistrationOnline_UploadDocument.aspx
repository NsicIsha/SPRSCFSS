<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminOnline.master" AutoEventWireup="true" CodeFile="NewUnitRegistrationOnline_UploadDocument.aspx.cs" Inherits="appAdministrator_NewUnitRegistrationOnline_UploadDocument" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">        .hiddencol {
            display: none;
        } </style>

    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll"></td>
        </tr>
        <tr style="width: 95%;">
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">



                    <tr style="width: 100%;">



                        <td id="step2" style="width: 100%;">

                             <%--<asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
            <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
            <asp:ListItem Value="FeMale" Text="FeMale"></asp:ListItem>
        </asp:DropDownList>--%>

                        </td>
                    </tr>


                </table>
            </td>
        </tr>





        <tr>
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">

                    <tr>
                        <td align="left" colspan="4" class="headerAll" style="width: 170px;">Upload Document
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="4" align="left" valign="top" style="height: 10px; padding-top: 20px" class="leftHeading">Use Combination of UDYAM Registration Number & PAN to Retrive Unit Details for Uploading Documents
                        </td>
                    </tr>


                    <tr align="left" valign="top" width="100%">
                        <td colspan="4" style="padding: 10px">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>


                                    <table border="1" cellpadding="2" cellspacing="2" width="100%">
                                        <tr>
                                            <td align="left" valign="top" class="leftHeading"><span class="MandatoryField">*</span>आपके पास उदयम पंजीकरण संख्या / <%--Do--%> You have UDYAM Registration Number</td>
                                            <td align="left" valign="top" class="leftHeading" colspan="2">
                                                <asp:RadioButtonList ID="rdbMsmeRegtype" Width="250px" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbMsmeRegtype_SelectedIndexChanged">
                                                   <%-- <asp:ListItem Value="1" Text="UAM"></asp:ListItem>--%>
                                                    <asp:ListItem Value="2" Text="Udyam Registration"></asp:ListItem>
                                                </asp:RadioButtonList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic"
                                                    ErrorMessage="Please Select Unit have UAM or Udyami Registration Number" ControlToValidate="rdbMsmeRegtype" SetFocusOnError="true"
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



                                                        <asp:Label ID="Label1" runat="server" CssClass="errMessage" Text="Aahar already exists !"
                                                            Visible="false"></asp:Label>
                                                    </div>
                                            </td>
                                            <td align="left" valign="top" class="leftHeading">
                                                <span class="MandatoryField">*</span>पैन कार्ड संख्या/ PAN Card Number:   
                                                        <asp:TextBox ID="txtPan" runat="server" MaxLength="10" ValidationGroup="validate" Style="text-transform: uppercase;" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPan" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPan"
                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$" ValidationGroup="validate"></asp:RegularExpressionValidator>
                                            </td>
                                            <td align="left" valign="top" class="leftHeading" style="padding-top: 15px;">
                                                <asp:Button ID="btnf" runat="server" Text="Fetch" CssClass="btnCls" OnClick="btnf_Click" ValidationGroup="validate" />


                                            </td>

                                        </tr>



                                        <tr>
                                            <td colspan="3" align="center" class="leftHeading">
                                                <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel3">

                                                    <ProgressTemplate>
                                                        <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                                        <span style="color: red; font-size: medium">Please wait while we are processing your request <%--Please wait, while your request is being processed--%></span>
                                                    </ProgressTemplate>

                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
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


                            <asp:GridView ID="gvDocumentDetail" runat="server" Width="100%"  
                                AutoGenerateColumns="false" BorderWidth="0" CellPadding="0" CellSpacing="1" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GrdUpload_RowCommand">
                                <HeaderStyle CssClass="GrdviewHeader" Height="40px" />
                                <EmptyDataTemplate>
                                    <center><strong>No document provided</strong></center>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="GrdviewRowOdd" />
                                <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />

                                <%-- <HeaderStyle CssClass="dynRowHeader" />

                                <RowStyle CssClass="dynRowEven" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="dynRowOdd" />
                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Left" CssClass="Pagging" />--%>
                                <Columns>

                                    <asp:TemplateField HeaderText="S. No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="DU_Namee" DataField="fGPtbl_DU_Name" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"></asp:BoundField>
                                    <asp:TemplateField HeaderText="NAME" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLName" runat="server" Visible="true" Text='<%#Eval("fGPtbl_DU_Name") %>' Target="_blank"></asp:HyperLink>
                                            <asp:HiddenField ID="HiddenFieldpath" runat="server" Value='<%#Eval("fGPtbl_DU_FilePath") %>' />
                                            <asp:HiddenField ID="HiddenFieldname" runat="server" Value='<%#Eval("fGPtbl_DU_FileName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <table>
                                                <td align="left" valign="top" style="padding-bottom: 10px; width: 30%;">
                                                     

                                                    <asp:UpdatePanel runat="server" ID="updatepanel1">
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="txt_bx" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>


                                                    <asp:RegularExpressionValidator
                                                        ID="RegularExpressionValidator1" runat="server" Display="None" ValidationGroup="1"
                                                        ErrorMessage="You can upload only .jpg, .jpeg, .gif, .png,  .pdf files"
                                                        ControlToValidate="FileUpload1" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|gif|GIF|png|PNG|pdf|PDF)$"></asp:RegularExpressionValidator>
                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                        TargetControlID="RegularExpressionValidator1">
                                                    </asp:ValidatorCalloutExtender>
                                                </td>
                                                <td align="left" valign="top" style="padding-bottom: 10px; width: 25%;">
                                                    <asp:Label ID="Label1" runat="server" CssClass="ErrMsgLbl" Visible="false" Text="Document size can't be greater then 3 MB."></asp:Label>
                                                </td>
                                                <td align="left" valign="top" style="padding-bottom: 10px; width: 15%;">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save"
                                                        CssClass="btnCls" TabIndex="37" CommandArgument='<%#Eval("fGPtbl_DU_Name") %>'
                                                        ValidationGroup="1" Width="150px" CommandName="Upload" Visible="false" OnClick="btnSubmit_Click" />

                                                </td>
                                            </table>
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

