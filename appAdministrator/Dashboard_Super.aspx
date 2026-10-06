<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Dashboard_Super.aspx.cs" Inherits="appAdministrator_Dashboard_Super" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="center" valign="top" style="width: 100%; height: 10px;">
                        <table cellpadding="2" cellspacing="0" border="1">
                            <tr>
                                <td align="left">From</td>
                                <td align="left">
                                    <asp:TextBox ID="txt_from_date" Enabled="false" runat="server"  placeholder="MM/dd/yyyy"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="withdate" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_from_date"></asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_from_date" Format="MM/dd/yyyy" PopupButtonID="imgbtnCalendar" runat="server" />
                                             
                                </td>
                                <td align="left">To</td>
                                <td align="left">
                                    <asp:TextBox ID="txt_todate" runat="server" Enabled="false" placeholder="MM/dd/yyyy"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="withdate" ID="rfd1" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_todate"></asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_todate" Format="MM/dd/yyyy" PopupButtonID="ImageButton1" runat="server" />
                                    
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbStatus"  runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbDuration_SelectedIndexChanged">
                     <asp:ListItem Text="Live" Value="1" ></asp:ListItem>
                     <asp:ListItem Text="Expired" Value="2"></asp:ListItem>
                                        <asp:ListItem  Selected="True" Text="Both" Value="3"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                </td>
                                <td align="left">
                                    <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <img alt="progress" src="../Images/Admin/loading.gif" />
                                            Processing...            
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                     
                                     <asp:Button ID="btn_status_track" runat="server"  ValidationGroup="withdate"
        Text="View Details" Width="120" CssClass="btnCls"  OnClientClick="javascript:return validate();" 
        onclick="btn_status_track_Click"  />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                 
                 <tr >
            <td colspan="4"  align="left" valign="top" style="padding-left:5px; padding-right:5px;" >
             <table border="1"   cellpadding="0" cellspacing="0" width="100%" style="margin-top:20px" >
               
                  <tr align="center">
                       
                <td colspan="5" class="leftHeadingReport" align="center" style="font-size:20px; font-family:Arial Baltic; font-weight:bold; padding-top:25px; padding-bottom:15px">
               Status from:  <b><asp:Label ID="lblCurrentStatus" Font-Size="20px" ForeColor="Green" runat="server"></asp:Label></b>
                </td>
               </tr>
                
              
                 <tr align="left" valign="top">                   
                 <td align="left" valign="top" class="leftHeading">
                     1.
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblUnitName" Font-Bold="true" Font-Size="Larger" runat="server" Text="Total Count"></asp:Label></b>
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom" colspan="3">
                   <asp:Label ID="lblTotalCount" Font-Bold="true" Font-Size="Large" runat="server" Text=""></asp:Label>
                 </td>
                      
                 </tr>                 
                  <tr align="left" valign="top">                   
                 <td align="left" valign="top" class="leftHeading">
                     2.
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblLiveUnit" Font-Bold="true" Font-Size="Larger" runat="server" Text="Live Units"></asp:Label></b>
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                   <asp:Label ID="lblLiveUnitValue" Font-Bold="true" Font-Size="Large" runat="server" Text=""></asp:Label>
                 </td>
                     <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblSocialCategory" Font-Bold="true" Font-Size="Larger" runat="server" Text="Expired Units"></asp:Label></b>
                 </td>
                   <td align="left" valign="top" class="rightHeadingBottom">
                      <asp:Label ID="lblExUnitsValue" Font-Bold="true" Font-Size="Large" runat="server" Text=""></asp:Label>
                   </td>
                 </tr>                  
                  <tr align="left" valign="top">                   
                 <td align="left" valign="top" class="leftHeading">
                     3.
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblEnterprisecategory" Font-Bold="true" Font-Size="Larger" runat="server" Text="Micro"></asp:Label></b>
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                   <asp:Label ID="lblMicroValue" Font-Bold="true" Font-Size="Large" runat="server" Text=""></asp:Label>    (<asp:Label ID="lblMicroPer"  ForeColor="Red" Font-Bold="true" Font-Size="Large" runat="server" Text=""></asp:Label>)
                   
                 </td>
                     <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblApplicationdate" Font-Bold="true" Font-Size="Larger" runat="server" Text="Small"></asp:Label></b>
                 </td>
                   <td align="left" valign="top" class="rightHeadingBottom">
                      <asp:Label ID="lblSmallValue" Font-Bold="true" Font-Size="Large" runat="server" Text=""></asp:Label> (<asp:Label ID="lblSmallPer"  ForeColor="Red" Font-Bold="true" Font-Size="Large" runat="server" Text=""></asp:Label>)
                   </td>
                 </tr>
                 <tr align="left" valign="top">                   
                 <td align="left" valign="top" class="leftHeading">
                     4.
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblPan" runat="server" Font-Bold="true" Font-Size="Larger" Text="SC Count"></asp:Label></b>
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                   <asp:Label ID="lblSCValue" Font-Bold="true" Font-Size="Large" runat="server" Text=""></asp:Label>
                 </td>
                     <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblUAM" runat="server" Font-Bold="true" Font-Size="Larger" Text="ST Count"></asp:Label></b>
                 </td>
                   <td align="left" valign="top" class="rightHeadingBottom">
                      <asp:Label ID="lblSTValue" Font-Bold="true" Font-Size="Large" runat="server" Text=""></asp:Label>
                   </td>
                 </tr>
                  </caption>
                         
             </table>
            </td>
        </tr>
                
                
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    
    
    </asp:Content>
