<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="CertificateList_OneMonth.aspx.cs" Inherits="appAdministrator_CertificateList_OneMonth" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> <%--check--%>

 

     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:HiddenField runat ="server" ID ="hid_val" Value ="0" />
<div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll" Text="Registered Companies List for Renewal"></asp:Label>
        </div>
        
        <div align="left" style="height:10px;">
            
        </div>
        
        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
           <div style="height:10px;">
        
        </div>
        <div align="center" class="clsrowdiv">
            <div>
           

                 <div class="coloncls" style ="width:100%;" ><asp:RadioButtonList ID="rdbDuration"  runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbDuration_SelectedIndexChanged">
                     <asp:ListItem Text="30 Days" Selected="True" Value="1" ></asp:ListItem>
                     <asp:ListItem Text="6 Months" Value="6"></asp:ListItem>
                                                            </asp:RadioButtonList> </div>
        </div>
            </div> 
             <div align="left" class="clsrowdiv" >
            <div class="leftext" style ="width:7%;" >Search</div>
            <div class="coloncls" style ="width:22%;" ><asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL">
                <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
                <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
               <%-- <asp:ListItem Text="Reference No." Value="3"></asp:ListItem>
                <asp:ListItem Text="Registration No." Value="4"></asp:ListItem>--%>
            </asp:DropDownList></div>
             <div class="txtdiv" style ="width:25%;"><asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox></div>
            <div class="btndiv" style ="width:40%;">
              <table cellspacing="2" cellpadding="2" border="0">
                 <tr>
                    <td align="left" valign="top">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CssClass="btnClsdivSearch" onclick="btnSearch_Click" />
                    </td>
                    
                    <td align="left" valign="top">
                    
                        <div id="container">
                <b><span style="color:Red; text-align:right"> <asp:Label ID="lbl_count_text" runat="server" Text=""></asp:Label></span></b>
                  </div>
                    </td>
                 </tr>
              </table>
            
                    
              
                    
         
        </div>
              
              
          <div class="txtlbl">
              <asp:Button Visible="false" ID="btnSend" runat="server" Text="Send Mail"  onclick="btnSend_Click"/>
       <%-- <b><span style="color:Red; text-align:right"> <asp:Label ID="lbl_count_text" runat="server" Text=""></asp:Label></span></b>--%>
         </div>   
        <div style="height:10px;">
        
        </div>
        
        <div align="left" style="padding-left: 10px;padding-top:25px; padding-right: 10px;">
        <asp:GridView ID="gvCertificateDetails" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvCertificateDetails_RowDataBound"
            Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                onpageindexchanging="CertificateListPageindexChanging" AllowPaging="True" AllowSorting="True" PageSize="10"> 
            <HeaderStyle CssClass="dynRowHeader" />
            
            <RowStyle CssClass="dynRowEven"  HorizontalAlign="Center"/>
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
              
                <asp:BoundField DataField="fGPtbl_Company_Name"  HeaderText="Company Name" />
           
                         <asp:TemplateField HeaderText="PAN No.">
                            <ItemTemplate>
                                   <asp:Label ID="lbl_pan_text" runat="server" Text='<%#Eval("fGPtbl_Company_PAN") %>'></asp:Label>
                                 
                            </ItemTemplate>
                         </asp:TemplateField>

                       <asp:TemplateField HeaderText="Expired On">
                            <ItemTemplate>
                                   <asp:Label ID="lbl_Validity_Range" runat="server" Text='<%#Eval("ExpiryDate_Registration_Date_Minus_One") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Validity">
                            <ItemTemplate>
                                   <asp:Label ID="lbl_Validity_Range" runat="server" Text='<%#Eval("Validity_Range") %>'></asp:Label>
                            </ItemTemplate>
                         </asp:TemplateField>
                 <%--<asp:TemplateField HeaderText="Expire In -- Days" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                   <asp:Label ID="lbl_Validity_Range" runat="server" Text='<%#Eval("Validity_gap") %>'></asp:Label>
                            </ItemTemplate>
                         </asp:TemplateField>--%>
                <asp:BoundField DataField="Validity_gap"  HeaderText="Expire In " HeaderStyle-HorizontalAlign="Center"/>


                 
                  
               
               </Columns>
        </asp:GridView>    
        
    </div>
    </div>
    
  
    </div>
    
    
    
    </asp:Content>
