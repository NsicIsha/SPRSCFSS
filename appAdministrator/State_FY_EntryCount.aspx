<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="State_FY_EntryCount.aspx.cs" Inherits="appAdministrator_State_FY_EntryCount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                <table style="width: 100%">
                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">State:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="clsDDL"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                                                                ErrorMessage="Please Select State" ControlToValidate="ddlState"
                                                                SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">Financial Year:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                             <asp:DropDownList id="ddlFY" runat="server">
                                 <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                  <asp:ListItem Value="1" Text="2010-11"></asp:ListItem>
                                  <asp:ListItem Value="2" Text="2011-12"></asp:ListItem>
                                  <asp:ListItem Value="3" Text="2012-13"></asp:ListItem>
                                  <asp:ListItem Value="4" Text="2013-14"></asp:ListItem>
                                  <asp:ListItem Value="5" Text="2014-15"></asp:ListItem>
                                  <asp:ListItem Value="6" Text="2015-16"></asp:ListItem>
                                  <asp:ListItem Value="7" Text="2016-17"></asp:ListItem>
                                  <asp:ListItem Value="8" Text="2017-18"></asp:ListItem>
                                  <asp:ListItem Value="9" Text="2018-19"></asp:ListItem>
                                  <asp:ListItem Value="10" Text="2019-20"></asp:ListItem>
                             </asp:DropDownList>

                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                                ErrorMessage="Please Select Financial Year" ControlToValidate="ddlFY"
                                                                SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
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
        
                    
                            
                        </td>
                    </tr>
                </table>
                   
                  
                    </td>
                    <td align="left" valign="top">
                     
                    </td>
         
         </tr>
           
         
         
         
         <tr>
            <td colspan="4" >
                <span  style="color:Red;"> Total Company Registration  </span>:  <asp:Label ID="lbl_total_Count" runat="server" Text=""></asp:Label>
                    
            </td>
         </tr>
         
         </table>
</asp:Content>

