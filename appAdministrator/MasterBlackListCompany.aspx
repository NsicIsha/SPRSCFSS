<%@Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="~/appAdministrator/MasterBlackListCompany.aspx.cs" Inherits="appAdministrator_MasterBlackListCompany" Title="NSIC::SPR COMPANIES LIST" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function FunActiveConfirm(obj) {
                if (obj.title == 'Activate This') {
                    if (confirm('Are you sure you want to ACTIVATE this company?') == true) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {

                    if (confirm('Are you sure you want to revert back?') == true) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }

            function validateform() {
                if (document.getElementById("<%=txt_authority_text.ClientID%>").value == "") {
                    alert("Please enter Authority Text");
                    document.getElementById("<%=txt_authority_text.ClientID%>").focus();
                    return false;
                }

                if (document.getElementById("<%=txt_iomcertificate.ClientID%>").value == "") {
                    alert("Please Enter IOM Certificate No");
                    document.getElementById("<%=txt_iomcertificate.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txt_reason.ClientID%>").value == "") {
                    alert("Please enter Reason");
                    document.getElementById("<%=txt_reason.ClientID%>").focus();
                    return false;
                }

                return true;

            }
   /*     sd*/
       </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll" Text="Companies List"></asp:Label>
        </div>
        
        <div align="left" style="height:10px;">
            
        </div>
        
        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
           <div style="height:10px;">
        
        </div>
        
             <div align="left" class="clsrowdiv" >
            <div class="leftext">Search</div>
            <div class="coloncls"><asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL">
                <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
                <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
                <asp:ListItem Text="Reference No." Value="3"></asp:ListItem>
                <asp:ListItem Text="Registration No." Value="4"></asp:ListItem>
            </asp:DropDownList></div>
            <div class="txtdiv"><asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox></div>
            <div class="btndiv">
                <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CssClass="btnClsdivSearch" onclick="btnSearch_Click" /></div>
        </div>
        <div style="height:10px;">
        
        </div>
        
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:GridView ID="gvBlacklist" runat="server" AutoGenerateColumns="False"
            Width="100%" BorderWidth="0px" CellPadding="5" CellSpacing="1" 
                AllowPaging="True" AllowSorting="True" 
                onrowdatabound="gvBlacklist_RowDataBound" 
                onrowcommand="gvBlacklist_RowCommand" 
                onpageindexchanging="gvBlacklist_PageIndexChanging" 
                EmptyDataText="No Data! found"> 
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                        <asp:HiddenField runat ="server" ID ="hdn_previousrole" Value ='<%# Eval("CompanyId")%>' />
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fGPtbl_CUnit_RegistrationNo" HeaderText="SPR No." />
                <asp:BoundField DataField="fGPtbl_Company_GPReferenceNo" 
                    HeaderText="Reference No" />
                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
                <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                
                <asp:TemplateField HeaderText="BlackList">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnActivateDe" runat="server" border="0" 
                     ImageUrl="~/Images/Admin/edit.png"      CommandName ="blacklist"  CommandArgument='<%# Eval("CompanyId").ToString()%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  
                 <asp:TemplateField HeaderText="Activate/ Deactivate" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                <asp:HiddenField runat ="server" ID ="hdn_blacklist" Value = '<%# Eval("CompanyBlackList").ToString()%>' />
                                                <asp:LinkButton ID ="lnkAct" runat ="server" CommandName = "Act" CommandArgument='<%# Eval("CompanyId").ToString()%>'
                                                  OnClientClick="return FunActiveConfirm(this);"> Revert Back </asp:LinkButton>
                                                </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
               </Columns>
        </asp:GridView>    
        
    </div>
    
    <asp:Panel ID="pnlOut" runat="server" CssClass="outerPopup" >
      <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
               <tr>
                <td > <asp:HiddenField runat ="server" ID ="hdn_previousstatus" /> 
                 <asp:HiddenField runat ="server" ID ="hdn_companyid" />
                </td>
              <td align="right" valign="middle">
                                                            <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnBack" runat="server"
                                                                ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                        </td>
               </tr>
                <tr>
                    <td class="leftHeading">
                        * Authority Name
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_authority_text" runat="server" CssClass="txt_bx"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="reqqauthortitytext" runat="server" Display="None"
                             ValidationGroup ="pop"    ErrorMessage="Please Enter Authority Name" ControlToValidate="txt_authority_text"
                                SetFocusOnError="true" ></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vc1" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="reqqauthortitytext">
                            </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                <td class="leftHeading">&nbsp;* IOM/Certificate no:  </td>
                <td align="left"> <asp:TextBox runat ="server" ID ="txt_iomcertificate" CssClass="txt_bx">
                </asp:TextBox> 
                   <asp:RequiredFieldValidator ID="reqiomcertificate" runat="server" Display="None"
                             ValidationGroup ="pop"    ErrorMessage="Please Enter IOM/Certificate no" ControlToValidate="txt_iomcertificate"
                                SetFocusOnError="true" ></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vc2" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="reqiomcertificate">
                            </cc1:ValidatorCalloutExtender>
                </td>
               </tr>
                 <tr>
                <td class="leftHeading">&nbsp;* BlackList Reason </td>
                <td align="left"> <asp:TextBox runat ="server" ID ="txt_reason" TextMode="MultiLine" CssClass="txt_bx"></asp:TextBox> 
                
                 <asp:RequiredFieldValidator ID="reqreason" runat="server" Display="None"
                             ValidationGroup ="pop"    ErrorMessage="Please Enter BlackList Reason " 
                             ControlToValidate="txt_reason"
                                SetFocusOnError="true" ></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vc3" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="reqreason">
                            </cc1:ValidatorCalloutExtender>
                
                </td>
              
              
              
               </tr>
                <tr>
                    <td class="leftHeading">
                        </td>
                    <td align="left">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btnCls" 
                            ValidationGroup ="pop"  Text="Submit" 
                            onclick="btnSubmit_Click"  />
                    </td>
                </tr>
            </table>
           </asp:Panel>
     </asp:Panel>
    
  <%--  <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOut"
        DropShadow ="true" TargetControlID="LinkButton1" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
 --%>
    <asp:LinkButton ID="LinkButton1" runat="server" style="display:none;"></asp:LinkButton>
    
    
    

<!-- ModalPopupExtender -->
<cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlOut" TargetControlID="LinkButton1"
  CancelControlID ="btnBack"  BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
    <div style="height: 100px">
        Do you like this product?&nbsp;
        
    </div>
    <asp:Button ID="btnClose" runat="server" Text="Close" />
</asp:Panel>
    
    
    
    
    </div>
</asp:Content>

