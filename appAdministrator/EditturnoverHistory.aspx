<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Report.master" AutoEventWireup="true" CodeFile="EditturnoverHistory.aspx.cs" Inherits="appAdministrator_EditturnoverHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <style type="text/css">
    .class
      {
      	 color:Red;
      	  color:Blue;
      }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <table cellspacing="4"  cellpadding="4" border="0">
    <tr>
            <td align="left" class="headerAll">
              History  TurnOver  Changes
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
          <tr>
            <td align="right" valign="top">
              <div align="right" style="width:100%;">
                               <%-- <div style="float:right;">
                                    Shows Changes
                                    <div class="clsLegends_Left_Deactivate" style="background-color:Red;float:right;"></div>
                                    <div style="float:right;">Uttam</div>
                                </div>--%>
                                  <table cellpadding="0" cellspacing="0" border="0">
                                  <tr>
                                      
                                    <td align="right"><b>Shows Changes: &nbsp;&nbsp;</b></td>
                                      
                                      <td><div class="clsLegends_Left_Deactivate" style="background-color:Red;float:right;">&nbsp;&nbsp;&nbsp;</div></td>
                                        <td><b>&nbsp;&nbsp;&nbsp;&nbsp;New Value&nbsp;&nbsp;&nbsp;&nbsp;</b></td>
                                      <td><div class="clsLegends_Left_Deactivate" style="background-color:Blue;float:right;">&nbsp;&nbsp;&nbsp;</div></td>
                                      <td><b>&nbsp;&nbsp;&nbsp;OLD Value&nbsp; </b></td> 
                                      
                                  </tr>
                                  
                                  </table>
                            </div>
            
            </td>
          </tr>
         <tr>
            
            <td align="left" valign="top" style="padding-left:10px; padding-right:10px;">
            
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                           (7.a) व्यापार की मात्रा (लाख रूपयों में)<br /> Turnover (in lacs):- 
                        </td>
                    </tr>
                    
                    <tr>
                       <td align="left" valign="top" colspan="3">
                         <asp:GridView ID="grdTurnOver_old" runat="server" AutoGenerateColumns="true"
                        Width="100%"  CellPadding="10" CellSpacing="1"  BackColor="Blue">
                          <EmptyDataTemplate>
                                    <center><strong>No information found</strong></center>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                                <RowStyle CssClass="GrdviewRowOdd" />
                                <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                <Columns>
                                   
                                    
                                </Columns>
                        
                        </asp:GridView>
                       </td>
                    
                    </tr>
                    
                    <tr align="left" valign="top">
                    
                        <td align="left" valign="top" class="leftHeading"  colspan="3">
                            <asp:GridView ID="grdTurnOver" AutoGenerateColumns="true" runat="server" 
                                Width="100%"  CellPadding="10" CellSpacing="1" 
                                BackColor="Red">    <%-- onrowdatabound="grdturnover_rowDatbound"--%>
                                <EmptyDataTemplate>
                                    <center><strong>No information found</strong></center>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="GrdviewHeader_new" Height="50" />
                                <RowStyle CssClass="GrdviewRowOdd_new" />
                                <AlternatingRowStyle CssClass="GrdviewRowEven_new" />
                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                <Columns>
                                   
                                    
                                </Columns>
                             </asp:GridView>
                         </td>
                     </tr>
                     
                    
                    </table>
                    </td>
                    </tr>
   
   </table>
   


</asp:Content>

