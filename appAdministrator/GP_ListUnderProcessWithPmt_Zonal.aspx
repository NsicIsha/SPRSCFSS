<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="GP_ListUnderProcessWithPmt_Zonal.aspx.cs" Inherits="appAdministrator_GP_ListUnderProcessWithPmt_Zonal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> <%--check--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center">
        <table border="1">
            <tr class="clDropDown_report">
                <td>Zone :
                </td>
                <td>
                    <asp:dropdownlist id="ddlZone" runat="server" width="150px" onselectedindexchanged="ddlZone_SelectedIndexChanged"
                        cssclass="clsDDL" autopostback="true">
                            </asp:dropdownlist>
                    <asp:requiredfieldvalidator id="rfdZone" runat="server" controltovalidate="ddlZone" initialvalue="0" errormessage="Required"></asp:requiredfieldvalidator>

                </td>

            </tr>
            <tr align="left" class="clDropDown_report" valign="top" style="width: 100%; visibility: collapse">
                <td align="center" valign="top" style="width: 100%;" colspan="2">
                    <div align="center" style="vertical-align: top;">
                        Branch :
                            <asp:dropdownlist id="ddlBranch" runat="server" width="150px" datavaluefield="fGPtbl_MB_Id" datatextfield="fGPtbl_MB_Name"
                                cssclass="clsDDL">
                            </asp:dropdownlist>
                        <asp:requiredfieldvalidator id="rfdBranch" runat="server" controltovalidate="ddlBranch" initialvalue="0" errormessage="Required"></asp:requiredfieldvalidator>

                    </div>
                </td>
            </tr>
            <tr class="clDropDown_report">
                <td>Status: </td>
                <td>
                    <asp:dropdownlist id="ddlStatus" runat="server" autopostback="true" cssclass="clDropDown_report">
    <asp:ListItem Text ="No Selection" Value ="" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Pending:FreshDocVerification" Value ="DA" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Pending:RenewalDocVerification" Value ="RV" Class = "clDropDown_report"></asp:ListItem>
        <asp:ListItem Text ="Pending:RenlAmendDocVerification" Value ="RAV" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Inspection Pending" Value ="IPe" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Inspection Passed" Value ="IPs" Class = "clDropDown_report"></asp:ListItem>
    
    </asp:dropdownlist>
                </td>
            </tr>
            <tr style="visibility: collapse">
                <td>Ageing in months: </td>
                <td>
                    <asp:dropdownlist id="ddlWhere" runat="server" autopostback="true" class="clDropDown_report">
    <asp:ListItem Text ="No Selection" Value ="" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Less than 2 month" Value ="<2" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="More than 2 month" Value =">2" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="More than 3 month" Value ="3" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Less than 6 month" Value ="<6" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="More than 6 month" Value ="6" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="More than 1 year" Value ="1y" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="More than 2 year" Value ="2y" Class = "clDropDown_report"></asp:ListItem>
    </asp:dropdownlist>
                </td>
            </tr>
            <tr style="visibility: collapse">
                <td>Financial Year: </td>
                <td>
                    <asp:dropdownlist id="ddlFY" runat="server" autopostback="true" class="clDropDown_report">
    <asp:ListItem Text ="No Selection" Value ="0" Class = "clDropDown_report"></asp:ListItem>
      <asp:ListItem Text ="2015-16" Value ="2015" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="2016-17" Value ="2016" Class = "clDropDown_report"></asp:ListItem>
    </asp:dropdownlist>
                </td>
            </tr>
            <tr style="visibility: collapse">
                <td>Order By: </td>
                <td>
                    <asp:dropdownlist id="ddlOrd" runat="server" autopostback="true" class="clDropDown_report">
    <asp:ListItem Text ="No Selection" Value ="" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Age" Value ="Age" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="NSICOffice" Value ="Office" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="NSIC Fee" Value ="Fee" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Cheque Amt" Value ="CAmt" Class = "clDropDown_report"></asp:ListItem>
         </asp:dropdownlist>
                </td>
            </tr>
        </table>

        <asp:updateprogress id="update_progress_search" runat="server" associatedupdatepanelid="update1_grid">
                             <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
                          
                          </asp:updateprogress>
        <asp:updatepanel id="update1_grid" runat="server">
                          <ContentTemplate>


     <asp:Button ID="Button1" runat="server" Text="Report"  OnClick ="Button1_Click" />

                               </ContentTemplate>
                        
                        </asp:updatepanel>

        <br />
        <br />
        <asp:label id="lbl_Title" runat="server" text="SPR cases" font-bold="true"></asp:label>
    </div>
    <br />
    <br />
    <asp:panel runat="server" bordercolor="gray" id="pnl_Macro" visible="true">
           
            <asp:Label ID="lbl_Macro" runat="server" Text="" Visible ="true"  Font-Bold="true"    ></asp:Label>
            
             <asp:GridView ID="gdv_Macro" runat="server" CssClass ="MyGridView" CellPadding  = "2"  Width ="50%" HorizontalAlign ="Center"  RowStyle-HorizontalAlign ="center"    ShowFooter ="true"   >
            <AlternatingRowStyle BackColor ="#d3d3d3"   />
            <Columns>
            <asp:TemplateField HeaderText="S.No">
            <ItemTemplate>
            <%#Container.DataItemIndex+1 %></ItemTemplate>
            
            
            
             <FooterTemplate>
             <%--<table border ="1"><tr><td><div style="padding:0 0 1px 0">
             <asp:Label ID="Label1" Text="Total" runat="server"  /></div></td><td></td><td></td><td></td><td><div style="padding:0 0 1px 0"><asp:Label ID="lblTotal" runat="server"    /></div></td></tr></table> 
             --%>
             
             
             </FooterTemplate>

             <FooterStyle     />
            </asp:TemplateField> 
          
             </Columns> 
            
            


            </asp:GridView>
       </asp:panel>

    <hr />

    <asp:panel runat="server" id="pnl_PartsDetails" visible="false">
        <asp:Label ID="lbl_PartsDetails" runat="server" Text="" Visible ="true"   Font-Bold="true"    ></asp:Label>
           
            <asp:GridView ID="gdv_PartsDetails" runat="server"   CellPadding  = "2" BackColor="AliceBlue" CssClass ="MyGridView"  Width ="80%" HorizontalAlign ="center"     >
            <AlternatingRowStyle BackColor ="#d3d3d3"   />
            <Columns>
            <asp:TemplateField HeaderText="S.No">
            <ItemTemplate>
            <%#Container.DataItemIndex+1 %></ItemTemplate>
            </asp:TemplateField> 
            </Columns> 
            </asp:GridView>
        
         </asp:panel>




    <br />
    <br />
    <br />
    <br />

</asp:Content>

