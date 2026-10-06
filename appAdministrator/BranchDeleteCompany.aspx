<%@ Page Title="Branch Delete Company" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="BranchDeleteCompany.aspx.cs" Inherits="appAdministrator_BranchDeleteCompany" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField runat ="server" ID ="hid_comapnyId" />
   <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll">Delete Incomplete Companies</asp:Label>
        </div>
        
        <div align="left" style="height:10px;">
            
        </div>
        
        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
           <div style="height:10px;">
        
        </div>
        
          <div align="left" class="clsrowdiv">
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
                    CssClass="btnCls" onclick="btnSearch_Click" />
                 
                    </div>
                    
                    
        </div> <br />
         <div class="txtlbl">
         <b><span style="color:Red;"> <asp:Label ID="lbl_count_text" runat="server" Text=""></asp:Label></span></b>
         </div>
        
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            <asp:GridView ID="gv_company2" runat="server" AutoGenerateColumns="False"
            Width="100%"
               AllowPaging="True"
                AllowSorting="True"  
                onpageindexchanging="gv_company_PageIndexChanging" 
                onrowcommand="gv_company_RowCommand" 
                onrowdeleting="gv_company2_RowDeleting" >   <%--DataKeyNames ="fGPtbl_Company_Id"--%>
                <EmptyDataTemplate>
                                                <div align="center"><strong style="font-size:18px; font-weight:bold; color:Red;"> Record not found!!!</strong></div>
                           </EmptyDataTemplate>
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" CssClass="Pagging" />
           <Columns>
           <%-- <asp:TemplateField>
      
        <HeaderTemplate>
          <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />
        </HeaderTemplate> 
       <ItemTemplate>
         <asp:CheckBox ID="chk_select" runat="server"  onclick = "Check_Click(this)" />
       </ItemTemplate> 
    </asp:TemplateField>  --%>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
                <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                <asp:BoundField DataField="fGPtbl_Company_GPReferenceNo" 
                    HeaderText="Reference No" />
                <asp:BoundField DataField="regis" HeaderText="Registration No." />
                <asp:BoundField DataField="status" HeaderText="Status" />
                
                 <%-- <asp:TemplateField HeaderText="Options">
                                            <ItemTemplate>
                                            
                                                 <asp:LinkButton ID="lnk_delete" runat="server" CommandArgument='<%# Bind("fGPtbl_Company_Id") %>'
                                                CommandName="delete" CausesValidation="false">
                                                   Delete
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>--%>
                                             <asp:TemplateField HeaderText="Options" 
                    HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_delete" runat="server" CommandName="delete"
                            CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="DELETE">  Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                
</asp:TemplateField>
               
               </Columns>
        </asp:GridView>       
        
    </div>
    <br /> <center>
      <asp:Button runat ="server" ID ="btn_delete" Text ="Delete" CssClass ="btnCls" 
                onclick="btn_delete_Click" Visible="False" />
    </center>
  <br /> 
    
    
<asp:Panel ID="pnlpanOut" runat="server"  CssClass="outerPopup">
        <asp:Panel ID="pnlpanInner" runat="server" Width="400px" CssClass="innerPopup">
            <center>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top" align="left" style="width:100%;">
                        <td valign="top" align="left" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr valign="top" align="left" style="width:100%">
                                    <td valign="top" align="right" colspan="2" style="padding-right: 5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr align="left" valign="top" style="width:100%;">
                                                    <td align="center" valign="top" class="updateInnerHeader">
                                                        &nbsp;&nbsp;</td>
                                                    <td align="right" valign="top" style="width: 20px;">
                                                        <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImageButton1" runat="server"
                                                            ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                    </td>
                                                </tr>
                                            </table>
                                    </td>
                                </tr>
                                
                                 <tr align="left" valign="top" style="width:100%;">
                                    <td colspan="2" align="center" valign="top">
                                       <asp:Label runat ="server" ID ="lblmsg" CssClass ="red_link"></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" valign="top">
                                   <td align="left" valign="top" class="updateInnerColumn">
                               
                                   <asp:HiddenField ID="lbl_company_id_onpnl" runat="server" />
                                   Region:
                                   </td>
                                   <td align="left" valign="top">
                                     <asp:TextBox ID="txt_reson_delete" runat="server" TextMode="MultiLine"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="req_pan" runat="server" ControlToValidate="txt_reson_delete"
                SetFocusOnError="true" ErrorMessage="Please Fill Region" Display="None" ValidationGroup="2"></asp:RequiredFieldValidator>
                
                  <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="req_pan"
                runat="server">
            </cc1:ValidatorCalloutExtender>
                                     
                                   </td>
                                </tr>
                                
                                 <tr valign="top" align="left">
                                    <td align="left" valign="top" style="height:10px;">
                                    </td>
                                 </tr>
                                 <tr valign="top" align="center" >
                                    <td valign="top" align="left">
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:Button ID="btnSubmitpan" runat="server" Text="Submit" TabIndex="3" 
                                            ValidationGroup="2" CssClass="btnCls" onclick="btnSubmitpan_Click"
                                             />
                                            &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                        </table>
                    </td>
                </tr>
                </table>
            </center>
         
        </asp:Panel>
    </asp:Panel>
    
     <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlpanOut"
        TargetControlID="LinkButton2" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender2" TargetControlID="pnlpanInner"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </cc1:RoundedCornersExtender>
    <asp:LinkButton ID="LinkButton2" runat="server" style="display:none;"></asp:LinkButton>
    </div>
</asp:Content>

