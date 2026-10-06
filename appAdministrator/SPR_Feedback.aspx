<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminNew.master" AutoEventWireup="true" CodeFile="SPR_Feedback.aspx.cs" Inherits="appAdministrator_SPR_Feedback" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center">
        <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
            <tr align="left" valign="top" style="width: 100%;">
                <td align="left" valign="top" style="width: 100%;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr style="width: 100%;">
                            <td align="left" class="headerAll" style="padding-left: 10px; padding-right: 10px;">Feedback
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                                        </td>
                                    </tr>
                                     
                                    <tr id="trbranch2" runat="server" align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="center" valign="top" style="height: 10px; font-size: 14px; font-weight: bold; color: Red;">
                                            <asp:Literal ID="ltlProvisionalNotice" runat="server" Text=""></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr id="trbranch3" runat="server" align="left" valign="top">
                                        <td colspan="3" align="left" valign="top" class="rowHeading">&nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                                        </td>
                                    </tr>

                                    <tr align="left" valign="top">


                                        
                                        <td align="center" valign="top" style="width:9%;" class="leftHeading" colspan="3">
                                             Fetch Fiiled Data by  :: 
                                            <asp:RadioButtonList ID="rdbType" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Pan Number"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Registration  No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            
                                        </td>
                                        <%--<td align="left" valign="top" style="width: 50%;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <div style="float: left;">
                                                            <asp:TextBox ID="txt_1" runat="server" CssClass="txt_bx" MaxLength="10" TabIndex="1"
                                                                ValidationGroup="1" AutoPostBack="true" Style="text-transform: uppercase;" OnTextChanged="txt_1_TextChanged"
                                                                AutoCompleteType="Disabled"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None"
                                                                ErrorMessage="Please Enter Valid Pan Number" ControlToValidate="txt_1" SetFocusOnError="true"
                                                                ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomRight"
                                                                TargetControlID="RegularExpressionValidator2">
                                                            </asp:ValidatorCalloutExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                                ErrorMessage="Please Enter Pan Number" ControlToValidate="txt_1" SetFocusOnError="true"
                                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" PopupPosition="BottomRight"
                                                                TargetControlID="RequiredFieldValidator1">
                                                            </asp:ValidatorCalloutExtender>
                                                            &nbsp;
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                                <ProgressTemplate>
                                                                    <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                            <asp:Label ID="lblPANmsg" runat="server" CssClass="errMessage" Text="PAN already exists !"
                                                                Visible="false"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lbl_branchName_chek" runat="server" CssClass="errMessage" Text=""
                                                                Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>--%>
                                    </tr>
                                    


                                  



                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">
                                        </td>
                                        <td align="right" valign="top" style="width: 45%;" class="leftHeading">
                                             PAN Number:
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txt_bx" MaxLength="10" TabIndex="1"
                                                ValidationGroup="1"></asp:TextBox>
                                            
                                          
                                              
                                        </td>
                                    </tr>

                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">
                                        </td>
                                        <td align="right" valign="top" style="width: 45%;" class="leftHeading">
                                             Registration Number:
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:TextBox ID="txtregistration" runat="server" CssClass="txt_bx" MaxLength="50" TabIndex="1"
                                                ValidationGroup="1"></asp:TextBox>
                                            
                                          
                                              
                                        </td>
                                    </tr>

                                     <tr align="left" valign="top">
                                        <td align="center"  colspan="3">
                                            <div>
                                                        <div style="float: centre;">
                                                            <asp:Button ID="btnFetchdata" runat="server" Text="Fetch Data" CssClass="btnCls"
                                                                ValidationGroup="1" Width="150px" OnClick="btnFetchdata_Click" TabIndex="54" />
                                                        </div>
                                                         
                                                    </div>
                                        </td>
                                       
                                    </tr>
                                      
                              
                           <tr align="left" valign="top">
                                        <td colspan="3">
                                            <asp:Panel runat ="server"  ID ="pnl_content" ScrollBars ="Auto" Width ="700px">
  <table cellspacing="0" cellpadding="0" rules="all" border="0"  style="border-width:0px;width:100%;border-collapse:collapse;">
				<tr class="dynRowHeader">
					<th scope="col">Year</th><th scope="col">Turnover</th><th scope="col">Manpower</th>
				</tr><tr class="dynRowEven">
					<td><asp:TextBox ID="txtYear1" CssClass="txt_bx" runat="server"></asp:TextBox></td><td><asp:TextBox ID="txtTOYear1" CssClass="txt_bx" runat="server"></asp:TextBox></td><td><asp:TextBox ID="txtManPowerYear1" CssClass="txt_bx" runat="server"></asp:TextBox> </td>
				</tr><tr class="dynRowOdd">
					<td><asp:TextBox ID="txtYear2" CssClass="txt_bx" runat="server"></asp:TextBox></td><td><asp:TextBox ID="txtTOYear2" CssClass="txt_bx" runat="server"></asp:TextBox></td> <td><asp:TextBox ID="txtManPowerYear2" CssClass="txt_bx" runat="server"></asp:TextBox> </td>
				</tr>
      <tr class="dynRowEven">
					<td><asp:TextBox ID="txtYear3" CssClass="txt_bx" runat="server"></asp:TextBox></td><td><asp:TextBox ID="txtTOYear3" CssClass="txt_bx" runat="server"></asp:TextBox></td> <td><asp:TextBox ID="txtManPowerYear3" CssClass="txt_bx" runat="server"></asp:TextBox> </td>
				</tr>

     <tr class="dynRowHeader">
          <td colspan="3">Forecast Year Details </td>
      </tr>

      <tr class="dynRowEven">
					<td><asp:TextBox ID="txtFCYear1" CssClass="txt_bx" runat="server"></asp:TextBox></td><td><asp:TextBox ID="txtFYTO" CssClass="txt_bx" runat="server"></asp:TextBox></td> <td><asp:TextBox ID="txtFYManPower" CssClass="txt_bx" runat="server"></asp:TextBox></td>
				</tr>


			</table>

                                                </asp:Panel>
                                        </td>
                                    </tr>


                        

  <tr align="left" valign="top">
                                        <td align="center"  colspan="3">
                                            <div>
                                                        <div style="float: centre;">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btnCls"
                                                                ValidationGroup="1" Width="150px" OnClick="btnSubmit_Click" TabIndex="54" />
                                                        </div>
                                                         
                                                    </div>
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
                    <asp:HiddenField ID="hdnCompany" runat="server" />
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="left">&nbsp;
                </td>
            </tr>
            <tr>
                <td align="left">&nbsp;
                </td>
            </tr>
        </table>
    </div>


    
</asp:Content>

