<%@ Page Title=" SPR :: MissingCompanyList " Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MissingCompanyList.aspx.cs" Inherits="appAdministrator_MissingCompanyList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
  .Message
    {
       color:Red;
       font-family:Arial;
       font-size:16px;
        font-weight:bold;
        text-align:center;
        text-decoration:none;
        pitch-range:50;
        
    }


</style>
  
<script language="javascript" type="text/javascript" >
        function FunResetcertificate() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtRequest").value = "";
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll" Text="Update Missing Info. of Registered Companies"></asp:Label>
        </div>
        
        <div align="left" style="height:10px;">
            
        </div>
        
      
           <div style="height:10px;">
        
        </div>
        
             <div align="left" class="clsrowdiv" >
            <div class="leftext">Search</div>
            <div class="coloncls">
            <asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL">
            <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
             
            </asp:DropDownList>
            </div>
            <div class="txtdiv"><asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox></div>
            <div class="btndiv"><asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CssClass="btnClsdivSearch" onclick="btnSearch_Click" /></div>
        </div>
        <div style="height:10px;">
        
        </div>
        
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:GridView ID="gvCertificateDetails" runat="server" AutoGenerateColumns="false"
            Width="100%" BorderWidth="0" CellPadding="3" CellSpacing="3" 
                onrowdatabound="gvCertificateDetails_RowDataBound" 
                onrowcommand="gvCertificateDetails_RowCommand"    onpageindexchanging="CertificateListPageindexChanging" AllowPaging="True" AllowSorting="True" PageSize="10"> 
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fGPtbl_CUnit_RegistrationNo" HeaderText="SPR No." />
                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
                <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                <asp:TemplateField HeaderText="view" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                     
                            <asp:LinkButton ID="lnkview" runat="server" Text="View"></asp:LinkButton>
                        <asp:HiddenField ID="HiddenFieldCompanyId" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                     
                    </ItemTemplate>
                </asp:TemplateField>
               
                
                  
               
               </Columns>
        </asp:GridView>    
        
    </div>
    </div>
    
  
      <div align="center">
  
           <asp:Label ID="ltlMessage" runat="server" CssClass="Message"></asp:Label>
        </div>

   
    <asp:LinkButton ID="LinkButton1" runat="server" style="display:none;"></asp:LinkButton>
    


   

</asp:Content>

