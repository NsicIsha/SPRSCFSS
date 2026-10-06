<%@ Page Title="NSIC SPR::Service Tax Master" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterServiceTax.aspx.cs" Inherits="appAdministrator_MasterServiceTax" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript"    language="javascript">
    function FunResetBranch() {

        document.getElementById("<%=txtservice_text.ClientID %>").value = "";
        document.getElementById("<%=txt_proposal_date.ClientID %>").value = "";
        document.getElementById("<%=txt_Proposal_end_Date.ClientID %>").value = "";
    }

    function FunActiveConfirm(obj) {
        if (obj.title == 'Delete') {
            if (confirm('Are you sure you want to this service tax? This will also  Delete!') == true) {
                return true;
            }
            else {
                return false;
            }
        }
    }
</script>
<script type="text/javascript" language="javascript">
    var specialKeys = new Array();

    specialKeys.push(8); //Backspace  

    function numericOnly(elementRef) {

        var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;

        if ((keyCodeEntered >= 48) && (keyCodeEntered <= 57)) {

            return true;

        }

        // '.' decimal point...  

        else if (keyCodeEntered == 46) {

            // Allow only 1 decimal point ('.')...  

            if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))

                return false;

            else

                return true;

        }

        return false;

    }  
</script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
   <div align="left">
        <asp:Label ID="lblBranch" runat="server" Text="Service Tax Master" CssClass="headerAll"></asp:Label>
    </div>
    
    <div align="right">
        <asp:Button ID="btnAddBranch" Text="Add" CausesValidation="false" runat="server"
            CssClass="btnCls" onclick="btnAddBranch_Click"  />
    </div>
    
    
      
    <div align="center" style="padding :10px;">
         <table border="1" width="100%" class="clsOutlineDyntblBorder">
                        <tr align="left" valign="top" style="width: 100%;">
                            <td align="left" valign="top" style="width: 100%;">
                              <asp:GridView ID="grd_servicetax"  runat="server"  AutoGenerateColumns="false"
                              Width="100%" 
                                    BorderWidth="0" CellPadding="3" CellSpacing="4" 
                                    onrowcommand="grd_servicetax_RowCommand" 
                                    onrowdatabound="grd_servicetax_RowDataBound" >
                                       <HeaderStyle CssClass="dynRowHeader" />
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    
                               <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Literal ID="ltlSno" runat="server"></asp:Literal></ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="Servicetax" HeaderText="Service Tax" />
                                          <asp:BoundField DataField="ProposalDate" HeaderText="Proposal Date" />
                                            <asp:BoundField DataField="ProposalEndDate" HeaderText="Proposal End Date" />
                                             <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Images/Admin/edit.png" AlternateText="Edit"
                                                    CommandName="edt" CommandArgument='<%#Eval("servicetaxid") %>' ToolTip="Edit Service Tax" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgAct" runat="server" CommandName="Act" CommandArgument='<%#Eval("servicetaxid") %>'
                                                 ImageUrl="~/Images/images_delete.jpg"  title="Delete"  OnClientClick="return FunActiveConfirm(this);"   />      <!-- OnClientClick="return FunActiveConfirm(this);" -->
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                               </Columns>
                                <EmptyDataTemplate>
                                   <span style="color:Red;"> No Record Found</span>
                                   
                                </EmptyDataTemplate>
                              </asp:GridView>
                            
                            </td>
                            </tr>
                            </table>
    
    </div>
    
        <asp:Panel ID="pnlOut" runat="server" Style="display: none;" CssClass="outerPopup">
           <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup">
           <center>
           <table cellpadding="0" cellspacing="0" border="1" width="100%">
           <tr valign="top" align="left" style="width: 100%;">
                        <td valign="top" align="left" style="width: 100%;">
                         <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr align="left" valign="top">
                                    <td colspan="2" align="left" valign="top" style="width: 100%; padding-bottom: 10px; padding-right:5px;">
                           <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr align="left" valign="top">
                                                <td align="center" valign="top" class="updateInnerHeader">
                                                    <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                        ID="lblBranchName" runat="server" CssClass="header"></asp:Label>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <asp:HiddenField ID="hdnfield" runat="server" />
                                                </td>
                                                <td align="right" valign="top" style="width: 20px;">
                                                    <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnBack" runat="server"
                                                        ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                </td>
                                            </tr>
                                        </table>
                                        
                        </td>
                        </tr>
                        
                        <tr align="left" valign="top" style="width: 100%;">
                                    <td colspan="2" align="right" valign="top">
                                        &nbsp;
                                    </td>
                                </tr>
                                 <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                      Service Tax :
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:TextBox ID="txtservice_text" runat="server" MaxLength="200" TabIndex="1" CssClass="txt_bx" onkeypress = "return numericOnly(this);" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                     Proposal Date:
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:TextBox ID="txt_proposal_date" runat="server" MaxLength="200" TabIndex="1" CssClass="txt_bx"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                    Proposal End Date :
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:TextBox ID="txt_Proposal_end_Date" runat="server" MaxLength="200" TabIndex="1" CssClass="txt_bx"></asp:TextBox>
                                    </td>
                                </tr>
                                   <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8" ValidationGroup="1"
                                         OnClick="btnSubmit_Click"    CssClass="btnCls"  />  <!-- OnClientClick="javascript:return ValidateEmail();" OnClick="btnSubmit_Click" -->
                                        &nbsp;&nbsp;
                                        <input id="btnReset" type="button" value="Reset" tabindex="9" class="btnCls"  onclick="return FunResetBranch();"/> <!-- onclick="return FunResetBranch();" -->
                                    </td>
                                </tr>
                                
                        </table>
                        </td>
                        </tr>
            </table>
            
            <asp:RequiredFieldValidator ID="RFValidatorBranchName" runat="server" ControlToValidate="txtservice_text"
                    SetFocusOnError="true" ErrorMessage="Please Fill Service Tax" Display="None"
                    ValidationGroup="1"></asp:RequiredFieldValidator>
                      <asp:RequiredFieldValidator ID="RequiredFieldProposalDate" runat="server" ControlToValidate="txt_proposal_date"
                    SetFocusOnError="true" ErrorMessage="Please Fill Proposal Date" Display="None"
                    ValidationGroup="1"></asp:RequiredFieldValidator>
                       <asp:RequiredFieldValidator ID="RequiredFieldEndProposalDate" runat="server" ControlToValidate="txt_Proposal_end_Date"
                    SetFocusOnError="true" ErrorMessage="Please Fill End Proposal Date" Display="None"
                    ValidationGroup="1"></asp:RequiredFieldValidator>
                    
                     <asp:CustomValidator ID="CustomValidatorBranchName" runat="server" ErrorMessage="Service Tax"
                    Display="None" ControlToValidate="txtservice_text" ClientValidationFunction="CharacterLimit200"
                    SetFocusOnError="true" ValidationGroup="1"></asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidatorDescription" runat="server" ErrorMessage="Proposal Date"
                    Display="None" ClientValidationFunction="CharacterLimit2000" ControlToValidate="txt_proposal_date"
                    ValidationGroup="1"></asp:CustomValidator>
                        <asp:CustomValidator ID="CustomValidatorAddress" runat="server" ErrorMessage="End Proposal Date"
                    Display="None" ClientValidationFunction="CharacterLimit2000" ControlToValidate="txt_Proposal_end_Date"
                    ValidationGroup="1"></asp:CustomValidator>
                    
                      <cc1:ValidatorCalloutExtender ID="VCExtenderBranchName" TargetControlID="RFValidatorBranchName"
                    runat="server">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="VCExtenderCode" TargetControlID="RequiredFieldProposalDate" runat="server">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="VCExtenderZonelist" TargetControlID="RequiredFieldEndProposalDate"
                    runat="server">
                </cc1:ValidatorCalloutExtender>
                <asp:CompareValidator ID="CValidatortxt_22" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                                    Display="Dynamic" ControlToValidate="txt_proposal_date" Operator="DataTypeCheck" Type="Date"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                                <cc1:CalendarExtender ID="CExtendertxt_22" runat="server" PopupButtonID="txt_proposal_date"
                                                    TargetControlID="txt_proposal_date">
                                                </cc1:CalendarExtender>
                                                <asp:CompareValidator ID="CValidatortxt_21" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                                    Display="Dynamic" ControlToValidate="txt_Proposal_end_Date" Operator="DataTypeCheck" Type="Date"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                                <cc1:CalendarExtender ID="CExtendertxt_21" runat="server" PopupButtonID="txt_Proposal_end_Date"
                                                    TargetControlID="txt_Proposal_end_Date">
                                                </cc1:CalendarExtender>
                                                
                                                <asp:CompareValidator ID="CVtwoDate" runat="server" ErrorMessage="Please enter end Date is greater than start date"
                                                     Display="Dynamic"  ControlToCompare="txt_proposal_date" ControlToValidate="txt_Proposal_end_Date"
                                                        Operator="GreaterThan" Type="Date" ValidationGroup="1"></asp:CompareValidator>
        </center>
           </asp:Panel>
        </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOut"
        TargetControlID="LinkButton1" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    
     <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none;"></asp:LinkButton>
</asp:Content>

