<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="LoginHistoryHead.aspx.cs" Inherits="appAdministrator_LoginHistoryHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <table cellpadding="0" cellspacing="0" style="width:100%; margin-left:10px;" >
        <tr>
          <td align="left">
            <asp:Label ID="lblCertificateRequest" runat="server" Text="Login History Details" CssClass="headerAll"></asp:Label>
          </td>
        </tr>
        <tr>
           <td align="left" style="height:10px;"></td>
        </tr>
          <tr>
            <td align="left" valign="top">
              <table cellpadding="2" cellspacing="2" border="0">
                <tr>
                  <td align="left" valign="top"> Search:  </td>
                  <td  align="left" valign="top"> <asp:TextBox ID="txt_serach" runat="server" Text="" CssClass="txt_bx"></asp:TextBox>  </td>
                   <td align="left" valign="top"><asp:Button ID="btn_Search" runat="server" 
                           Text="Search" CssClass="btnCls" onclick="btn_Search_Click" /></td>
                   <td align="left" valign="top"><asp:Button ID="btn_cancle" runat="server" 
                           Text="Clear" CssClass="btnCls" onclick="btn_cancle_Click" /></td>
                
                </tr>
              
              </table>
            
            </td>
          
          </tr>
        
           
          <tr>
            <td align="left" valign="top">
              <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                  <td align="left" valign="top">
                  <asp:GridView id="grd_history" runat="server"  AutoGenerateColumns="false"
             EnableViewState="false" AllowPaging="true"  Width="100%"
                                    BorderWidth="0" CellPadding="10" CellSpacing="1"  PageIndex="0" 
                          onpageindexchanging="grd_history_PageIndexChanging" 
                          onrowdatabound="grd_history_RowDataBound"  >
                                    
                                    <HeaderStyle CssClass="dynRowHeader" />
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    
                                  <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                    
             
              <Columns>  
                      
                      <asp:TemplateField  HeaderText="Login Name">
                        <ItemTemplate>
                          <asp:Label ID="lbl_Login_Name" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                        
                        </ItemTemplate>
                      
                      </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                         <asp:Label ID="lbl_nametext" runat="server" Text='<%#Eval("Nametext") %>'></asp:Label>
                        
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="IP Address">
                        <ItemTemplate>
                           <asp:Label ID="lbl_ipAddress" runat="server" Text='<%#Eval("LoginIP") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      
                      <asp:TemplateField  HeaderText="Login Date">
                      <ItemTemplate>
                        <asp:Label ID="lbl_Login_date" runat="server" Text='<%#Eval("LoginDate") %>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>
              </Columns>   
                  <EmptyDataTemplate>NO Records Found..</EmptyDataTemplate>  
                                    
                                    </asp:GridView>
                  
                  </td>
                
                </tr>
              
              
              </table>
            
            
            </td> 
          
          </tr>
        </table>

</asp:Content>

