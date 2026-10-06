<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Online_BAPendingMonetaryLimit.aspx.cs" Inherits="appAdministrator_Online_BAPendingMonetaryLimit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<meta http-equiv="x-ua-compatible" content="IE=8" />
<script type="text/javascript" language="javascript">
    function FunResetMonetary()
    {
        //document.getElementById("ctl00_ContentPlaceHolder1_txtMonetary").value = "";ggggg
        var grid = document.getElementById('<%= grdTurnOver.ClientID %>');
        if (grid != null) 
        {
            var Inputs = document.getElementsByTagName("INPUT"); ;
            for (i = 0; i < Inputs.length; i++) 
            {
                if (Inputs[i].type == 'text') 
                {
                    if (Inputs[i].value != "") 
                    {
                    // alert("Enter values,blank is not allowed");
                     Inputs[i].value = "";
                     return false;
                     }
                    
                  }//if
            }//for
            return true;
        }//if
    }//function
    
    function onlyNumbers(e) {
        
         var unicode = e.charCode ? e.charCode : e.keyCode;
        if (unicode == 8 || unicode == 9 || (unicode >= 48 && unicode <= 57) || unicode == 46) {
           return true;
        }
        else {
          return false;
        }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="left">
        <asp:Label ID="lblMonetary" runat="server" Text="Monetary limit to be set" CssClass="headerAll"></asp:Label>
    </div>
    
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvMonetary" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewMonetaryNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="center" valign="top" style="width:100%;">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                   </table>       
                  </asp:View>
                <asp:View ID="ViewMonetaryDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                               <%if(monetaryAction==1){ %>
                                                
                                                    <div id="divAction" runat="server" class="Action_Div" style="background-color:#ECF8FB; height:20px;">
                                                        <div id="Action_DivInner_1" align="center">
                                                            <asp:Label ID="LabelAction" runat="server" CssClass="MasterAction"></asp:Label>
                                                        </div>
                                                        <div id="Action_DivInner_2" align="center">
                                                            <img id="imgAction" onclick="FunZoneActionClose()" src="../Images/Admin/delete.png" border="0" width="16px" height="15px" alt="CLOSE" style="cursor:pointer;" />
                                                        </div>
                                                    </div>
                                                    <cc1:RoundedCornersExtender
                                                        ID="RoundedCornersExtender2" runat="server"
                                                        TargetControlID="divAction"
                                                        BorderColor="#22518F"
                                                        Radius="10"
                                                        Corners="All"
                                                    ></cc1:RoundedCornersExtender>
                                                <%} %>
                                            </td>
                                            <td valign="top" align="right" style="width: 30%;">
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </td>
                            </tr>
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="left" valign="top" style="width:100%;">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                <div align="center">
                    <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="left" valign="top" style="width:100%;">
                                    <asp:HiddenField ID="HiddenFieldCompanyId" runat="server" />
                                    <asp:HiddenField ID="HFieldformtypeid" runat="server" />
                                    
                                    <asp:HiddenField ID="hdnfactcnt" runat="server" />
                                    <asp:HiddenField ID="hdnprovisionl" runat="server" />
                                    <asp:GridView ID="gvMonetaryDetails" runat="server" AutoGenerateColumns="false"
                                    Width="100%" BorderWidth="0px" CellPadding="5" CellSpacing="2"  
                                        onrowcommand="gvMonetaryDetails_RowCommand" 
                                        onrowdatabound="gvMonetaryDetails_RowDataBound"  EnableModelValidation="false" 
                                        EnableSortingAndPagingCallbacks="True">
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                           <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="6%">
                                    <ItemTemplate><%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                             
                                            <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderStyle-Width="10%" HeaderText="PAN No."/>
                                          
                                            <%--<asp:TemplateField HeaderText="Set Monetary Limit" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbButton" runat="server" ImageUrl="~/Images/Admin/edit.png" CausesValidation="false"
                                                    AlternateText="SET" CommandName="edp" ToolTip="Set monetary limit" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>'  EnableViewState="true"/>
                                             </ItemTemplate><HeaderStyle Width="40px" /><ItemStyle HorizontalAlign="Center" /></asp:TemplateField>--%>
                                             <asp:TemplateField HeaderText="Set Monetary Limit" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbcalculate" runat="server" CommandArgument='<%# Eval("fGPtbl_Company_Id") %>' CausesValidation="false">Set</asp:LinkButton>
                                                </ItemTemplate>
                                             </asp:TemplateField>
                                                                                          
                                             <asp:BoundField DataField="RejectReason" HeaderStyle-Width="35%" HeaderText="Re-evaluate Reason" />


                                             <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--<asp:Button ID="btnReject" runat="server" CssClass="btnCls" CausesValidation="false"
                            Text="Reject" Width="60px" CommandName="reject" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' />--%>
                        <asp:HiddenField ID="hdncid" runat="server" Value='<%#Eval("fGPtbl_Company_Id")%>' />
                        <asp:HiddenField ID="hdnbranch" runat="server" Value='<%#Eval("Branchname")%>' />

                         <asp:HiddenField ID="hdnbranchmail" runat="server" Value='<%#Eval("fGPtbl_MU_Email")%>' />
                            <asp:HiddenField ID="hdncompanyemail" runat="server" Value='<%#Eval("fGPtbl_Company_HeadOffice_Email")%>' />
                            
                         <asp:HyperLink ID="lnkmail" runat="server" Text="Send Mail" Visible="false" Target="_blank" NavigateUrl=""></asp:HyperLink>

                            <asp:HyperLink ID="lnkview" runat="server" Target="_blank" Text="View Form" NavigateUrl="" ></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

<asp:TemplateField HeaderText="View Payment Details" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="btnAccepttt" runat="server" CssClass="btnCls" CausesValidation="false"
                            Text="View" Width="200px" CommandName="ViewPaymentDetails" CommandArgument='<%# Eval("fGPtbl_Company_Id") + "," + Eval("fGPtbl_PD_Id")%>'   />

                        
                    </ItemTemplate>
                </asp:TemplateField>
                                           </Columns>
                                        
                                    </asp:GridView>
                                    
                                    
                                    </td>
                                </tr>
                        </table>
                      <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                       </table>  
                </div>
            </asp:View>
        </asp:MultiView>
        
    </div>
    
    
    <asp:Panel ID="pnlOut" runat="server" CssClass="outerPopup">
        <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup">
            <center>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr valign="top" align="left" style="width:100%;">
                    <td valign="top" align="left" style="width:100%;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr valign="top" align="left" style="width:100%">
                                <td valign="top" align="right" colspan="2" style="padding-bottom:10px; padding-right:5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr align="left" valign="top">
                                                        <td align="center" valign="top" class="updateInnerHeader">
                                                            <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="top" style="width: 20px;">
                                                            <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnBack" runat="server"
                                                                ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                    
                                </td>
                            </tr>
                            <tr>
                              <td colspan ="2" align ="center" valign ="top">
                                <asp:GridView ID ="gv_display" runat ="server" AutoGenerateColumns ="true" 
                                      Visible="False" Width="100%">
                                </asp:GridView>
                              </td>
                            </tr>
                            
                            <tr align="left" valign="top" style="width:100%;">
                                <td colspan="2" align="center" valign="top">
                                    &nbsp;<strong>Enter the turnover in ascending order of the Financial Year.<br />Turnover should be Only NUMERIC value ie 0-9 and dot(.)<br /> (Figures in lakhs)</strong>
                                </td>
                            </tr>
                            <tr align="left" >
                            <td colspan="2">
                            <asp:GridView ID="grdTurnOver" runat="server" AutoGenerateColumns="false" 
                                    onrowdatabound="grdTurnOver_RowDataBound" >
                               <Columns >
                               <asp:TemplateField HeaderText="Year" ItemStyle-Width="80" ControlStyle-Width="80"  >
                               <ItemTemplate >
                               <asp:TextBox ID="txt_year" runat="server" Width="80" ></asp:TextBox>
                               <asp:HiddenField ID="hdnuntid" runat="server" />
                               <asp:Label ID="lbl_limit" runat="server" Visible="false" Width="80" Font-Bold="true" 
                               Font-Size="XX-Small"   ></asp:Label>
                               </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Unit 1" ItemStyle-Width="50" ControlStyle-Width="50">
                               <ItemTemplate >
                               <asp:TextBox ID="txtbx_fact1" runat="server" Width="50" onkeypress="javascript:return onlyNumbers(event);" ></asp:TextBox>
                               <asp:HiddenField ID="hdnfact1" runat="server" Value='<%# Bind("Fact1") %>' />
                               </ItemTemplate>                                                              
                               </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unit 2" ItemStyle-Width="50" ControlStyle-Width="50">
                               <ItemTemplate >
                               <asp:TextBox ID="txtbx_fact2" runat="server" Width="50" onkeypress="javascript:return onlyNumbers(event);"></asp:TextBox>
                               <asp:HiddenField ID="hdnfact2" runat="server" Value='<%# Bind("Fact2") %>' />
                               </ItemTemplate>
                                                              
                               </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unit 3" ItemStyle-Width="50" ControlStyle-Width="50">
                               <ItemTemplate >
                               <asp:TextBox ID="txtbx_fact3" runat="server" Width="50" onkeypress="javascript:return onlyNumbers(event);"></asp:TextBox>
                               <asp:HiddenField ID="hdnfact3" runat="server" Value='<%# Bind("Fact3") %>' />
                               </ItemTemplate>
                                                              
                               </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unit 4" ItemStyle-Width="50" ControlStyle-Width="50">
                               <ItemTemplate >
                               <asp:TextBox ID="txtbx_fact4" runat="server" Width="50" onkeypress="javascript:return onlyNumbers(event);"></asp:TextBox>
                               <asp:HiddenField ID="hdnfact4" runat="server" Value='<%# Bind("Fact4") %>'/>
                               </ItemTemplate>                                                       
                               </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unit 5" ItemStyle-Width="50" ControlStyle-Width="50">
                               <ItemTemplate >
                               <asp:TextBox ID="txtbx_fact5" runat="server" Width="50" onkeypress="javascript:return onlyNumbers(event);"></asp:TextBox>
                               <asp:HiddenField ID="hdnfact5" runat="server" Value='<%# Bind("Fact5") %>' />
                               </ItemTemplate>
                                                              
                               </asp:TemplateField>
                               
                                                      
                               </Columns>
                               <HeaderStyle Font-Size=XX-Small Font-Bold="false" />
                              
                                                           
                            </asp:GridView>
                            </td>
                            
                            </tr>
                            
                            <tr align="left" valign="top" style="width:100%;">
                                <td colspan="2" align="right" valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            
                                                        
                            
                 
                              
                               <tr valign="top" align="center" >
                                <td valign="top" align="left" >
                                </td>
                                <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                    <asp:UpdatePanel ID="UpdatePanelmonetary" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="2" 
                                                ValidationGroup="1" CssClass="btnCls" onclick="btnSubmit_Click"/>
                                                &nbsp;&nbsp;
                                                <input id="btnReset" type="button" value="Reset" tabindex="3" class="btnCls" onclick="return FunResetMonetary();" />
                                         </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelmonetary">
                                        <ProgressTemplate>
                                            <asp:Panel ID="pnlMsg" runat="server">
                                                <table align="center" style="border: 2; background-repeat: no-repeat; background-position: center; position:absolute;
                                                    left: 450px; top: 300px; height: 100px; width: 200px; z-index: 10000; background-color: #a9dceb;">
                                                    <tr>
                                                        <td valign="top" align="center" style="color: Black; font-size: 12px; font-weight: bold;">
                                                            Processing .........
                                                            <br /><br />
                                                            Please wait.....
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
           <%-- <asp:RequiredFieldValidator ID="RFValidatorMonetary" runat="server" ControlToValidate="txtMonetary"
                SetFocusOnError="true" ErrorMessage="Please Fill Monetary Limit" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
                
             <cc1:ValidatorCalloutExtender ID="VCExtenderMonetary" TargetControlID="RFValidatorMonetary"
                runat="server"></cc1:ValidatorCalloutExtender>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RegularExpressionValidator1"
                runat="server"></cc1:ValidatorCalloutExtender>--%>
            
           </center>
        </asp:Panel>
    </asp:Panel>
    
    
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOut"
        TargetControlID="LinkButton1" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" TargetControlID="pnlInner"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </cc1:RoundedCornersExtender>
    <asp:LinkButton ID="LinkButton1" runat="server" style="display:none;"></asp:LinkButton>

</asp:Content>
