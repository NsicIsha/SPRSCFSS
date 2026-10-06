<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="STATUS_REPORT_AS_ON_DATE.aspx.cs" Inherits="appAdministrator_STATUS_REPORT_AS_ON_DATE" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
     .clsDDLhidden
{
	width:130px;
	background-color:#dddddd;
	border:#999999 solid 1px;
	padding:3px;
	display:none;
}
.btnclsfincial
{
font-family: Arial, Helvetica, sans-serif;
    font-size: 13px;
    background-color:White;
    color: Black;
    font-weight: bold;
    width: 160px;
}
 </style>
 
   <script type="text/javascript" language="javascript">

       function validdatefrom() {
           var txt = "Please Fill Required Date";
           var opt = 0;
           var Datefrom = document.getElementById("<%=txt_fromdate.ClientID %>").value;
           var dateto = document.getElementById("<%=txt_todate.ClientID %>").value;
           var fromdateval = new Date(Datefrom);
           if (Datefrom == "") {
               txt += "\n - From date.";
               opt = 1;
           }
           if (fromdateval <= new Date('03/31/2015')) {
               txt += "\n- Incorrect date. \n - Any date before 31 March, 2015 is not valid.";
               opt = 1;


           }
         
           if (dateto == "") {
               txt += "\n -  To Date.";
               opt = 1;
           }
               
         
           if (opt == "1") {
               alert(txt);
               document.getElementById("<%=txt_fromdate.ClientID %>").value = "";
               document.getElementById("<%=txt_fromdate.ClientID %>").focus();
               return false;
           }
           
       }
   
   
   
   
   
   
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text=" Report" CssClass="headerAll"></asp:Label>
    </div>
     <table cellpadding="2" cellspacing="2" border="0" style="padding-left:10px; width:100%; margin-top:20px;">
       <tr>
        <td colspan="4"><asp:HiddenField ID="Btn_value" runat="server" Value="" /></td>
       
       
       </tr>
       
         <tr >
           <td  align="left" valign="top" colspan="3"  >
            
           <%-- Fromdate <asp:TextBox ID="txt_fromdate" runat="server" Text=""></asp:TextBox> 
              <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fromdate" MaskType="Date"
                    Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                    ClearTextOnInvalid="true">
                    </asp:MaskedEditExtender>
                    <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlToValidate="txt_fromdate" ControlExtender="MaskedEditExtender1"
                    InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                    TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required" ></asp:MaskedEditValidator>
                  Todate <asp:TextBox ID="txt_todate" runat="server" Text=""></asp:TextBox>
                  
                  <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_todate" MaskType="Date"
                    Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                    ClearTextOnInvalid="true">
                    </asp:MaskedEditExtender>
                    <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlToValidate="txt_todate" ControlExtender="MaskedEditExtender3"
                    InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                    TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required" ></asp:MaskedEditValidator>
                    --%> 
                   
                <table style="width: 100%">

                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">From Date:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:TextBox ID="txt_fromdate" runat="server" Text=""></asp:TextBox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fromdate" MaskType="Date"
                                Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                                ClearTextOnInvalid="true">
                            </asp:MaskedEditExtender>
                            <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlToValidate="txt_fromdate" ControlExtender="MaskedEditExtender1"
                                InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                                TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>

                        </td>

                    </tr>

                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">To Date:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">

                             <asp:TextBox ID="txt_todate" runat="server" Text="" ></asp:TextBox>

                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_todate" MaskType="Date"
                    Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                    ClearTextOnInvalid="true">
                </asp:MaskedEditExtender>
                <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlToValidate="txt_todate" ControlExtender="MaskedEditExtender3"
                    InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                    TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>

                        </td>

                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">सामाजिक श्रेणी की उद्यम/ Enterprise Social Category:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:DropDownList ID="ddl_1e" runat="server" AutoPostBack="false"
                                TabIndex="52" ValidationGroup="1">
                            </asp:DropDownList>

                        </td>
                    </tr>

                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">उद्यमी की विशेष श्रेणी/ Any Special Category of Enterpreneur:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:DropDownList ID="ddl_1d" runat="server" AutoPostBack="false"
                                TabIndex="51" ValidationGroup="1">
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">श्रेणी यूनिट का आकार/Category Unit Size:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:DropDownList ID="DdlCategoryUnitSize" runat="server" AutoPostBack="false"
                                TabIndex="53">
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            कार्रवाई की प्रकृति/Nature of Operation :
                        </td>
                        <td align="left" valign="top">
                            <asp:RadioButtonList ID="rdo_manufacturlist" runat="server" RepeatDirection="Horizontal">
                                <%--<asp:ListItem Selected="True" Value="-1">None</asp:ListItem>--%>
                                <asp:ListItem  Value="1" >Manufacturing</asp:ListItem>
                                <asp:ListItem Value="2">Service</asp:ListItem>
                                <asp:ListItem Value="-1" Selected="True">Both</asp:ListItem>
                            </asp:RadioButtonList>


                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">Status:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            
                            <asp:RadioButtonList ID="rdo_Status" runat="server" RepeatDirection="Horizontal" Width="162px">
                               
                                <asp:ListItem  Value="1">Live</asp:ListItem>
                                <asp:ListItem Value="4">Expired</asp:ListItem>
                                 <asp:ListItem Selected="True" Value="-1">Both</asp:ListItem>
                            </asp:RadioButtonList>


                        </td>
                    </tr>

                 <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">State:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:UpdatePanel ID="Update_Panel_Statewise" runat="server">
                                <ContentTemplate>

                                    <asp:DropDownList ID="ddlState" runat="server"
                                        CssClass="clsDDlReport" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                        </td>
                    </tr>

                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">City:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">

                            <asp:UpdatePanel ID="Update_City_Wise" runat="server">
                                <ContentTemplate>

                                    <asp:DropDownList ID="Ddl_City_Wise" runat="server"
                                        CssClass="clsDDlReport" AutoPostBack="True">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                    </tr>


                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading"></td>
                        <td align="left" valign="top">
                             <asp:UpdatePanel ID="Update_bton"  runat="server" >
                        <ContentTemplate>
                    <asp:Button ID="btn_search" runat="server" Text="Search" 
                  onclick="btn_search_Click" CausesValidation="false" CssClass="btnCls" />
          
                  
                  </ContentTemplate>
                  </asp:UpdatePanel>
                  
                     <br />
                     <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="Update_bton"
        runat="server">
            <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
        </asp:UpdateProgress>
        
                    <asp:UpdateProgress ID="UpdateProgress1"
        AssociatedUpdatePanelID="Update_button_1"
        runat="server">
            <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
        </asp:UpdateProgress>
                            
                        </td>
                    </tr>
                </table>
                   
                  
                    </td>
                    <td align="left" valign="top">
                     
                    </td>
         
         </tr>
          <tr>
               <td colspan="4" align="left">
                   <table width="100%">
                        
                       <tr>
                           <td><asp:UpdatePanel ID="Update_button_1"  runat="server" >
                        <ContentTemplate>
                <asp:Button ID="btn_Serach_fourApril" runat="server" 
                            Text="Financial year 2014-15"  CssClass="btnclsfincial" 
                            CausesValidation="false" onclick="btn_Serach_fourApril_Click" />
                            </ContentTemplate>
                            </asp:UpdatePanel></td>
                           <td style="text-align:right">
                                 <asp:Button ID="btn_chek" runat="server" Text="Genarate Excel"
                  OnClick="btn_chek_Click"  CausesValidation="false"/>

                           </td>
                       </tr>
                   </table>
                

                  

                 </td>
              </tr>
         <tr>
           <td colspan="4">
             
              
           </td>
         </tr>
         
           <tr>
           <td colspan="4" align="left">
           
           
             <asp:GridView ID="grd_report" runat="server"  CellPadding="5" 
              CellSpacing="1" GridLines="Both" AutoGenerateColumns="false" Width="96%" 
              BorderWidth="0" HeaderStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Left" 
            HeaderStyle-HorizontalAlign="Center" ShowFooter="false" AllowPaging="false"
                   
                   onrowdatabound="grd_report_Databound" EmptyDataText="No data found!" OnRowCreated="grd_report_RowCreated" 
                  >
             <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven" />
                   <AlternatingRowStyle CssClass="dynRowOdd" />
                   <FooterStyle CssClass="dynRowHeader" />
                   
                   <Columns>
                    <%--  <asp:TemplateField HeaderText="S.No." >
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Zone">
                     <ItemTemplate>
                       <asp:Label ID="lbl_Zonename" runat="server" Text='<%#Eval("ZoneName") %>'></asp:Label>
                     <asp:Label ID="lbl_zonename_id" runat="server" Text='<%#Eval("ZoneID") %>' Visible="false"></asp:Label>
                     </ItemTemplate>
                      <FooterTemplate>
                    <asp:Label ID="lbl_totaltext_footer" runat="server" Text="Total"></asp:Label>
                  
                  </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch">
                   <ItemTemplate>
                     <asp:Label ID="lbl_Branch_name" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                      <asp:HiddenField ID="hdn_branch_id" runat="server" Value='<%#Eval("BranchID") %>' />
                   </ItemTemplate>
                
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fresh Registration">
                   <ItemTemplate>
                     <asp:Label ID="lbl_register_Company" runat="server" Text='<%#Eval("CompanyFresh") %>'></asp:Label>
                   </ItemTemplate>
                   <FooterTemplate>
                     <asp:Label ID="lbl_fresh_register_company" runat="server" Text=""></asp:Label>
                   </FooterTemplate>
                </asp:TemplateField>


                        <asp:TemplateField HeaderText="
                            Registration Deleted">
                   <ItemTemplate>
                     <asp:Label ID="lbl_Dregister_Company" runat="server" Text='<%#Eval("CompanyFreshDeleted") %>'></asp:Label>
                   </ItemTemplate>
                   <FooterTemplate>
                     <asp:Label ID="lbl_fresh_Dregister_company" runat="server" Text=""></asp:Label>
                   </FooterTemplate>
                </asp:TemplateField>



                <asp:TemplateField  HeaderText="Registration Backlog">
                   <ItemTemplate>
                     <asp:Label ID="lbl_Back_lock_company" runat="server" Text='<%#Eval("CompanyBacklog") %>'></asp:Label>
                   </ItemTemplate>
                   <FooterTemplate>
                     <asp:Label ID="lbl_total_back_lock_company" runat="server" Text=""></asp:Label>
                   </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amedment ">
                  <ItemTemplate>
                    <asp:Label ID="lbl_amed_Company" runat="server" Text='<%#Eval("CompanyAmedment") %>'></asp:Label>
                  </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lbl_total_amedment" runat="server" Text=""></asp:Label>
                </FooterTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField  HeaderText="Renewal ">
                   <ItemTemplate>
                     <asp:Label ID="lbl_Renewal_Company" runat="server" Text='<%#Eval("CompanyRenewal") %>'></asp:Label>
                   
                   </ItemTemplate>
                   
                   <FooterTemplate>
                     <asp:Label ID="lbl_total_renewal" runat="server" Text=""></asp:Label>
                   </FooterTemplate>
                
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total" ItemStyle-ForeColor="Red" FooterStyle-ForeColor="Red">
                  <ItemTemplate>
                    <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label>
                  
                  </ItemTemplate>
                  <FooterTemplate>
                    <asp:Label ID="lbl_total_footer" runat="server" Text=""></asp:Label>
                  
                  </FooterTemplate>
                
                </asp:TemplateField>
                   
                   
                   
                   </Columns>
            
            
            </asp:GridView>
           
           </td>
         
         </tr>
         <tr>
            <td colspan="4" >
                <span  style="color:Red;"> Total Fresh Company Register  </span>:  <asp:Label ID="lbl_total_fresh_backlog" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hdn_total_fresh_backlog" runat="server" Value="" />
            </td>
         </tr>
         
         </table>
</asp:Content>

