<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="PointOfInterest_Admin.aspx.cs" Inherits="appAdministrator_PointOfInterest_Admin" %>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
              
         <table cellpadding="0" cellspacing="0" border="0"  width="100%">
            
           <tr>
                  <td align="center"valign="top" style="width:100%; height:10px;" >
                       <table cellpadding="2" cellspacing="0" border="1" width="50%">
         <tr>
            
             


             <td width="100%">
                 Status : <asp:DropDownList ID="ddlFormType" runat="server">
                     <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                     <asp:ListItem Value="1" Text="Open"></asp:ListItem>
                     <asp:ListItem Value="2" Text="Closed"></asp:ListItem>
                      
                             </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0"  runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlFormType"></asp:RequiredFieldValidator>


             </td>
                        <td align="left" >

                          
                       
                           <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btnCls" 
                                onclick="Btn_Search_Click"  />
                                 <asp:Button ID="btn_chek" CssClass="btn-primary" runat="server"  Text="Generate Excel" OnClick="btn_chek_Click"  />
                                
                       
                        </td>

         </tr>
                  </table>
                  </td>
               </tr>
             
           <tr>
           
           
             <td align="left" valign= "bottom" style="padding-left:20px; width:100%; margin-top:10px;">
             <table cellpadding="0" cellspacing="0" border="0" width="100%">
               <tr>
                  <td align="left" valign="top" style="padding-left:20px;">
                    <b><span style="color:Red;"> <asp:Label ID="lbl_searchcompany" runat="server" Text=""></asp:Label> </b>
                  </td>
                  <td align="right" valign="top" style="padding-right:50px;">
                     <b><span style="color:Red;"><asp:Label ID="lbl_totalcount_buying" runat="server" Text=""></asp:Label></b>
                  
                  
                      </td>
               
               </tr>
               
                
               
             </table>
             
              
             
             </td>
           
           </tr>
            
             
             
           <tr><td style="height:10px;"></td></tr>
            
             <tr>
               <td  valign="top">
               
                
               
                  
                

                 <asp:GridView ID="grd_search_result" runat="server"  CellPadding="3" 
                       CellSpacing="3" GridLines="Both" 
                  AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="500"
                Width="100%" BorderWidth="0" onpageindexchanging="grdserch_pageindex_changing" 
                      
                   onrowcommand="gv_company_RowCommand" 


                       HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" class="grdserchkk">
                  <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven" />
                   <AlternatingRowStyle CssClass="dynRowOdd" />
                   <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                  <EmptyDataTemplate>
                                                <div align="center"><strong style="font-size:18px; font-weight:bold; color:Red;"> No  Data  Found !!!</strong></div>
                           </EmptyDataTemplate>
                 <Columns>
                                        <asp:TemplateField HeaderText="S.NO">
                      <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                      
                      </ItemTemplate>
                    </asp:TemplateField>
                 
                   <asp:TemplateField HeaderText="Company Name">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("fGPtbl_POI_Name") %>'></asp:Label>
                     </ItemTemplate>
                   </asp:TemplateField>
                   <%--<asp:TemplateField HeaderText="PAN Number"  >  
                    <ItemTemplate>
                    <asp:Label ID="lbl_location" runat="server" Text='<%#Eval("fGPtbl_POI_PAN") %>'></asp:Label>
                    </ItemTemplate>
                   
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="UAM">
                      <ItemTemplate>
                      <asp:Label ID="lbl_product" runat="server" Text='<%#Eval("fGPtbl_POI_UAM") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>

                      
                   <%-- <asp:TemplateField HeaderText="GST">
                      <ItemTemplate>
                      <asp:Label ID="lbl_productG" runat="server" Text='<%#Eval("fGPtbl_POI_GST") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>
                  
                    <asp:TemplateField HeaderText="Nature of Operation" >  
                      <ItemTemplate>
                        <asp:Label ID="lbl_registration_number" runat="server" Text='<%#Eval("NatureOperation")%>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="Firm Constitution">
                      <ItemTemplate>
                      <asp:Label ID="lbl_old_registration_no" runat="server" Text='<%#Eval("FirmConstitution") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField  HeaderText="Authorised Person">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberAP" runat="server" Text='<%#Eval("fGPtbl_POI_PersonName") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField  HeaderText="Social Category">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberSC" runat="server" Text='<%#Eval("SocialCategory") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField  HeaderText="Email">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberE" runat="server" Text='<%#Eval("fGPtbl_POI_Email") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField  HeaderText="Mobile">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberM" runat="server" Text='<%#Eval("fGPtbl_POI_Mobile") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField  HeaderText="State">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberState" runat="server" Text='<%#Eval("fGPtbl_MS_Name") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField  HeaderText="Branch">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBranch" runat="server" Text='<%#Eval("fGPtbl_MB_Name") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>

                        <asp:TemplateField  HeaderText="Point of Interest Date">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberDate" runat="server" Text='<%#Eval("fGPtbl_POI_CDate") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>


                     <%--  <asp:TemplateField  HeaderText="Product">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberProduct" runat="server" Text='<%#Eval("fGPtbl_POI_ProductDetails") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>
                     
                    <%--  <asp:TemplateField  HeaderText="EM-II">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberEM2" runat="server" Text='<%#Eval("fGPtbl_POI_EM2") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField  HeaderText="Inspection Agency">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberIA" runat="server" Text='<%#Eval("fGPtbl_MIA_Name") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>

                      <%-- <asp:TemplateField HeaderText="Action">
                           <ItemTemplate>
                               <asp:LinkButton   ID="btnAction" runat="server" Text="Actionn" CommandName="ActionPOI"  CommandArgument='<%#Eval("fGPtbl_POI_Id") %>'></asp:LinkButton>
                           </ItemTemplate>
                       </asp:TemplateField>--%>

                      <asp:TemplateField HeaderText="VIEW" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton CausesValidation="false" ID="imbView" runat="server" ImageUrl="~/Images/Admin/view.png" AlternateText="VIEW"
                                                    CommandName="ActionPOI" CommandArgument='<%#Eval("fGPtbl_POI_Id")%>' ToolTip="View content" ImageAlign="Middle"/>
                                                     
                                                </ItemTemplate>
                                            </asp:TemplateField>
                 </Columns>
                </asp:GridView>   
                
                
               
               </td>
             
             </tr>
              
      
          
         </table>
       


                  


            
             

        
       </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
