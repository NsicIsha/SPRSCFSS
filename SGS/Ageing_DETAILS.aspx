<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/SCST.master" AutoEventWireup="true" CodeFile="Ageing_DETAILS.aspx.cs" Inherits="SGS_Ageing_DETAILS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
          .text-label 
          {
            color: #ddf1f7;
            font-weight: bold;
           }  
           
           .grdserchkk
           {
           	  vertical-align:top;
           	  text-align:left;
           }
  </style>

    

    
    
</asp:Content>
 


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 
 
 <div align="center" >
    <table border ="1" >
    
   <%-- <tr class="clDropDown_report" ><td>Status: </td><td><asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true"  CssClass = "clDropDown_report" >
    <asp:ListItem Text ="No Selection" Value ="" Class = "clDropDown_report"></asp:ListItem>
   
    <asp:ListItem Text ="Inspection Pending" Value ="IPe" Class = "clDropDown_report"></asp:ListItem>
   
    
    </asp:DropDownList></td></tr>--%>
  <%--  <tr><td>Ageing in months: </td><td><asp:DropDownList ID="ddlWhere" runat="server" AutoPostBack="true"  Class = "clDropDown_report" >
    <asp:ListItem Text ="No Selection" Value ="" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Less than 2 month" Value ="<2" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="More than 2 month" Value =">2" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="More than 3 month" Value ="3" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Less than 6 month" Value ="<6" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="More than 6 month" Value ="6" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="More than 1 year" Value ="1y" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="More than 2 year" Value ="2y" Class = "clDropDown_report"></asp:ListItem>
    </asp:DropDownList></td></tr>--%>
    <%--<tr><td>Financial Year: </td><td><asp:DropDownList ID="ddlFY" runat="server" AutoPostBack="true"  Class = "clDropDown_report" >
    <asp:ListItem Text ="No Selection" Value ="0" Class = "clDropDown_report"></asp:ListItem>
      <asp:ListItem Text ="2015-16" Value ="2015" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="2016-17" Value ="2016" Class = "clDropDown_report"></asp:ListItem>
    </asp:DropDownList></td></tr>--%>
   <%-- <tr><td>Order By: </td><td><asp:DropDownList ID="ddlOrd" runat="server" AutoPostBack ="true"  Class = "clDropDown_report" >
    <asp:ListItem Text ="No Selection" Value ="" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Age" Value ="Age" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="NSICOffice" Value ="Office" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="NSIC Fee" Value ="Fee" Class = "clDropDown_report"></asp:ListItem>
    <asp:ListItem Text ="Cheque Amt" Value ="CAmt" Class = "clDropDown_report"></asp:ListItem>
         </asp:DropDownList></td></tr>--%>
    </table> 
    <%-- <asp:Button ID="Button1" runat="server" Text="Report"  OnClick ="Button1_Click" />--%>
     
         <br />    <br />
    <asp:Label ID="lbl_Title" runat="server" Text="SPR cases underprocess with ageing and Fee details" Font-Bold ="true" ></asp:Label>
  </div>      <br />    <br />
       <asp:Panel runat="server"   BorderColor ="gray"  ID="pnl_Macro" Visible="true"   >
           
            <asp:Label ID="lbl_Macro" runat="server" Text="" Visible ="true"  Font-Bold="true"    ></asp:Label>
            
             <asp:GridView ID="gdv_Macro" AutoGenerateColumns="false" runat="server" CssClass ="MyGridView" CellPadding  = "2"  Width ="50%" HorizontalAlign ="Center"  RowStyle-HorizontalAlign ="center"    ShowFooter ="true"   >
            <AlternatingRowStyle BackColor ="#d3d3d3"   />
            <Columns>
            <asp:TemplateField HeaderText="S.No">
            <ItemTemplate>
            <%#Container.DataItemIndex+1 %></ItemTemplate>
            </asp:TemplateField> 
            
             <asp:TemplateField HeaderText="Company Name">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("fGPtbl_Company_Name") %>'></asp:Label>
                     </ItemTemplate>
                   </asp:TemplateField>
            
            
           <asp:TemplateField HeaderText="Branch Name">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_Branch" runat="server" Text='<%#Eval("fGPtbl_MB_Name") %>'></asp:Label>
                     </ItemTemplate>
                   </asp:TemplateField>

                  <asp:TemplateField HeaderText="Created Date">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_CreatedDate" runat="server" Text='<%#Eval("fGPtbl_CUnit_Cdate") %>'></asp:Label>
                     </ItemTemplate>
                   </asp:TemplateField>

                  <asp:TemplateField HeaderText="Application Status">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_AppStatus" runat="server" Text='<%#Eval("ApplicationStatus") %>'></asp:Label>
                     </ItemTemplate>
                   </asp:TemplateField>

                 
               
                 <asp:TemplateField HeaderText="Inspection Allotment Date">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_InspectionDate" runat="server" Text='<%#Eval("fGPtbl_IS_DateStatus2") %>'></asp:Label>
                     </ItemTemplate>
                   </asp:TemplateField>

                 <asp:TemplateField HeaderText=" Age From Inspection Alloted">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_AgeInspectionDate" runat="server" Text='<%#Eval("Age_From_Inspection_Alloted") %>'></asp:Label>
                     </ItemTemplate>
                   </asp:TemplateField>

                 <asp:TemplateField HeaderText="Agency Name">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_AgencyName" runat="server" Text='<%#Eval("fGPtbl_MIA_Name") %>'></asp:Label>
                     </ItemTemplate>
                   </asp:TemplateField>
            <asp:TemplateField HeaderText="Agency User Name">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_AgencyUserName" runat="server" Text='<%#Eval("fGPtbl_TIAB_Name") %>'></asp:Label>
                     </ItemTemplate>
                   </asp:TemplateField>

                  <%--<asp:TemplateField HeaderText="Agency User Name">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_AgencyUserName" runat="server" Text='<%#Eval("fGPtbl_CUnit_RegistrationStatus") %>'></asp:Label>
                     </ItemTemplate>
                   </asp:TemplateField>--%>
                
             </Columns> 
            
            


            </asp:GridView>
       </asp:Panel>
      
      <hr />
       
        <asp:Panel runat="server"    ID="pnl_PartsDetails" Visible ="false"   >
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
        
         </asp:Panel>
    
    
   
 
  <br/> <br/> <br/> <br/>
   
</asp:Content> 
