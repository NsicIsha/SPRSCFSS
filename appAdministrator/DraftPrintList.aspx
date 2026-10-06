<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="DraftPrintList.aspx.cs" Inherits="appAdministrator_DraftPrintList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll" Text=" Companies List"></asp:Label>
        </div>
        
        <div align="left" style="height:10px;">
            
        </div>
        
        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
           <div style="height:10px;">
        
        </div>
        
             <div align="left" class="clsrowdiv" >
            <div class="leftext">Search</div>
            <div class="coloncls"><asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL">
                <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
                <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
                <asp:ListItem Text="Reference No." Value="3"></asp:ListItem>
                <asp:ListItem Text="Registration No." Value="4"></asp:ListItem>
            </asp:DropDownList></div>
            <div class="txtdiv"><asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox></div>
            <div class="btndiv"><asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CssClass="btnClsdivSearch" onclick="btnSearch_Click" /></div>
        </div>
        <div style="height:10px;">
        
        </div>
        
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:GridView ID="gvCertificateDetails" runat="server" AutoGenerateColumns="false"
            Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
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
                <asp:TemplateField HeaderText="Print<br>Certificate" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbnPrint" runat="server" CommandName="print"
                        CausesValidation="false" ToolTip="Print Certificate">DraftPrint</asp:LinkButton>&nbsp;
                       
                        <asp:HiddenField ID="HiddenFieldCompanyId" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                     <%--<asp:HiddenField ID="HiddenFormType" runat="server" Value='<%#Eval("FormTypeId")%>' />--%>
                    </ItemTemplate>
                </asp:TemplateField>
              
                  
               
               </Columns>
        </asp:GridView>    
        
    </div>
    </div>



</asp:Content>

