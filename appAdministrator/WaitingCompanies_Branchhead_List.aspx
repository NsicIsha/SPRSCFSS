<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="WaitingCompanies_Branchhead_List.aspx.cs" Inherits="appAdministrator_WaitingCompanies_Branchhead_List" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="left">
    <div align="left">
        <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll"></asp:Label>
    </div>
    <div align="left" style="height:10px;">
        
    </div>
  <%--  <div align="center">
        <%if(companyAction==1){ %>
        <div style="height:30px;">
            <div id="divAction" runat="server" class="Action_Div" align="center" style="background-color:#ECF8FB; height:30px;">
                <div id="Action_DivInner_1" align="center">
                    <asp:Label ID="LabelAction" runat="server" CssClass="MasterAction"></asp:Label>
                </div>
                <div id="Action_DivInner_2" align="center">
                    <img id="imgAction" onclick="FunZoneActionClose()" src="../Images/Admin/delete.png" border="0" width="16px" height="15px" alt="CLOSE" style="cursor:pointer;" />
                </div>
            </div>
        </div>
        <cc1:roundedcornersextender
            ID="RoundedCornersExtender2" runat="server"
            TargetControlID="divAction"
            BorderColor="#22518F"
            Radius="10"
            Corners="All">
         </cc1:roundedcornersextender>
         <%} %>
    </div>--%>
    <div align="center">
        <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
    </div>
     <div align="center">
    </div>
    
    
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:GridView ID="gvRegistrationDetails" runat="server" AutoGenerateColumns="false"
            Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
            OnRowDataBound ="gvRegistrationDetails_RowDataBound" onrowcommand="gvRegistrationDetails_RowCommand"
            > 
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <EmptyDataTemplate>
                <center>No data found</center>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <asp:Literal ID="ltlSno" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="fGPtbl_Company_GPReferenceNo" HeaderText="GP Referenceno." />--%>
                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
                <%--<asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory Address" />--%>
                <%--<asp:TemplateField HeaderText="SEND MAIL" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                    <ItemTemplate>
                           <asp:HiddenField ID="hdnbranchmail" runat="server" Value='<%#Eval("fGPtbl_MU_Email")%>' />
                            <asp:HiddenField ID="hdncompanyemail" runat="server" Value='<%#Eval("fGPtbl_Company_HeadOffice_Email")%>' />
                            
                            
                            <asp:HyperLink ID="lnkmail" runat="server" Text="Send Mail" Target="_blank" NavigateUrl=""></asp:HyperLink>
                        
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Accept" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="btnAccept" runat="server" CssClass="btnCls" CausesValidation="false"
                            Text="Accept" Width="60px" CommandName="accept" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                  <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center" Visible ="false" >
                    <ItemTemplate>
                        <asp:Button ID="btnReject" runat="server" CssClass="btnCls" CausesValidation="false"
                            Text="Reject" Width="60px" CommandName="reject" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
              <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--<asp:Button ID="btnReject" runat="server" CssClass="btnCls" CausesValidation="false"
                            Text="Reject" Width="60px" CommandName="reject" CommandArgument='<%#Eval("fGPtbl_CUnit_Id") %>' />--%>
                        <asp:HiddenField ID="hdncid" runat="server" Value='<%#Eval("fGPtbl_Company_Id")%>' />
                        <asp:HiddenField ID="hdnbranch" runat="server" Value='<%#Eval("Branchname")%>' />
                            <asp:HyperLink ID="lnkview" runat="server" Target="_blank" Text="View Form" NavigateUrl="" ></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
               </Columns>
        </asp:GridView>    
        
    </div>
  
    
    
</div>

</asp:Content>

