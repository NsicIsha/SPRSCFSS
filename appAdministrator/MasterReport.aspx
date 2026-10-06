<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterReport.aspx.cs" Inherits="appAdministrator_MasterReport" Title="NSIC SPR :: Reports Access Mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script type="text/javascript" language="javascript">
    function showmessage()
      {
        var Message=$("#ctl00_ContentPlaceHolder1_lblMessage").text();
           if(Message!=null)
            {
              alert(Message);
            }
              else
              {
                 
              }
      }
      
        function ShowDiv()
         {
           $("#DivMain").style.Visibility=true;
         }
         
        
  
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div  id="divSuper" style="padding-left:20px;"><%--gfdg--%>
<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
<div id="DivFetchAllList" >
   <table cellpadding="0" cellspacing="0" border="0" width="100%">
   
    <tr>
     <td align="left">
      <asp:Label ID="lblList" runat="server" Text="Reports Access Mapping Master" CssClass="headerAll"></asp:Label></td>
     
      
     
     </tr>
    <tr>
       <td align="left">
          </td>
     
     </tr>
   
    <tr>
      <td align="right">
         <%-- <input type="button" class="btnCls" value="Add" />--%>
         
          <asp:Button ID="btn_Add_Grid" runat="server" Text="ADD" class="btnCls" 
              onclick="btn_Add_Grid_Click" />
      </td>
    </tr>
     <tr>
       <td align="left">
          </td>
     
     </tr>
     <tr>
     <td align="left"  style="width:100%;">
<asp:GridView ID="gvMasterDetails" runat="server" AutoGenerateColumns="false"
                                        Width="100%" BorderWidth="1" CellPadding="4" CellSpacing="4" 
                                        onrowcommand="gvReportDetails_RowCommand" 
                                        onrowdatabound="gvReptDetails_RowDataBound">
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RoleName" HeaderText="ROLE  NAME" />
                                            <asp:BoundField DataField="RoleDescription" HeaderText="DESCRIPTION" />
                                            
                                            <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgButton" runat="server" ImageUrl="~/Images/Admin/edit.png" AlternateText="Edit" CommandName="edp" ToolTip="Edit department" ImageAlign="Middle" CommandArgument='<%#Eval("RoleId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                           </Columns>
                                       </asp:GridView>
     </td>
     </tr>
       
       
      
   
   </table>

</div>




  <div id="DivMain"   style="padding-left:20px;" runat="server"  visible="false">
    
     <table cellpadding="0" cellspacing="0" border="0" >
        
        <tr style="height:10px;">
         <td colspan="3">&nbsp;&nbsp;&nbsp;
         
            
         
         </td>
       </tr>
         
       <tr>
         <%--<td align="left" class="leftHeading"> Role </td>
           <td></td>
       <td align="left"><asp:TextBox ID="txt_Role" runat="server" MaxLength="55" TabIndex="1">
       
       
       </asp:TextBox>
         
 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                ErrorMessage="Please Enter Role"  ControlToValidate="txt_Role"
                                SetFocusOnError="true" ></asp:RequiredFieldValidator>
                            
       
       </td>--%>
        <td>&nbsp;</td>
       <td align="left" valign="middle" style="width:80px;" class="reportCell">
                                                Role :
                                            </td>
                                            <td align="left" valign="middle" style="width:195px;" class="reportCell">
                                                <asp:DropDownList ID="DropDownListRole" runat="server" CssClass="clsDDL" 
                                                    CausesValidation="True" TabIndex="1" ValidationGroup="1"  AutoPostBack="true"
                                                    onselectedindexchanged="DropDownListRole_SelectedIndexChanged">
                                                     
                                                    
                                                </asp:DropDownList>
                                                <asp:Label ID="lblSetValue" runat="server" Text=""></asp:Label>
                                                </td>
       
       </tr>
       <tr style="height:10px;">
         <td colspan="3">&nbsp;&nbsp;&nbsp;
         
            
         
         </td>
       </tr>
  
     <tr style="margin-top:10px;">
       <td colspan="3">&nbsp;&nbsp;&nbsp;
         
       </td>
     
     </tr>
     <tr style="margin-top:10px;">
       <td colspan="3">
          <asp:GridView ID="grdchek_Report" runat="server"  AutoGenerateColumns="false" 
               CellPadding="5" CellSpacing="5" BorderWidth="5" Width="550" 
               onrowdatabound="grdchek_Report_onRowdataBound">
            <HeaderStyle CssClass="dynRowHeader" />
             <RowStyle CssClass="dynRowEven" />
         <AlternatingRowStyle CssClass="dynRowOdd" />
          <Columns>
            
            
            <asp:TemplateField HeaderText="Report Name">
              <ItemTemplate>
                 <asp:Label ID="lbl_id" runat="server" Text='<%#Eval("ReportID") %>' Visible="false"></asp:Label>
                 <asp:Label ID="lbl_report_name" runat="server" Text='<%#Eval("ReportName")%>'></asp:Label>
              
              </ItemTemplate>
            
            </asp:TemplateField>
          
             <asp:TemplateField HeaderText="Allow Report">
                   <ItemTemplate>
                      <asp:CheckBox ID="myCheckBox" runat="server" />
                </ItemTemplate>
             
             </asp:TemplateField>  
          
          </Columns>
          
          </asp:GridView>
       
       </td>
     
     </tr>
     
     <tr style="margin-top:10px;">
       <td colspan="3">&nbsp;&nbsp;&nbsp;</td>
     </tr>
     
      <tr style="margin-top:10px;">
         <td colspan="2"></td>
           <td align="right">
             <table cellpadding="0" cellspacing="0" border="0">
               <tr>
                 <td align="left"></td>
                   <td> &nbsp;</td>
                     <td align="left"><asp:Button ID="btn_submit" runat="server" Text="Add" 
                             CssClass="btnCls" onclick="btn_submit_Click" /></td>
               
               </tr>
             
             </table>
           
           </td>
        </tr>
     </table>
  
  
  </div> 
  
  </div>
  
</asp:Content>

