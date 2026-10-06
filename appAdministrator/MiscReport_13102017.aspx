<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MiscReport_13102017.aspx.cs" Inherits="appAdministrator_MiscReport_13102017" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text="Dashboard Report" CssClass="headerAll"></asp:Label>
      <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnBackView_Click"/>
    </div>

    <asp:GridView ID="grdData" runat="server" CellPadding="5"   CellSpacing="5"
               
               GridLines="Both"  PageIndex="0" EnableViewState="false" EnableSortingAndPagingCallbacks="true"
                  AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" 
                    Width="100%" >
                
                   
                   <Columns>
                        <asp:BoundField HeaderText="Zone Name" DataField="ZoneName" /> 
                        <asp:BoundField HeaderText="Branch Name" DataField="BranchName" /> 
                        <asp:BoundField HeaderText="Company Name" DataField="Company_Name" /> 
                        <asp:BoundField HeaderText="PAN" DataField="PAN_Card_Number" /> 
                        <asp:BoundField HeaderText="Constitution Name" DataField="Step2_FIRM_Constitution_Name" /> 

                        <asp:BoundField HeaderText="Unit Size" DataField="Category_Unit_Size" /> 
                        <asp:BoundField HeaderText="Phone" DataField="HEAD_OFFICE_Phone"  />
                        <asp:BoundField  HeaderText="Mobile" DataField="HEAD_OFFICE_Mobile" /> 

                         <asp:BoundField HeaderText="Email" DataField="HEAD_OFFICE_Email"  />
                        <asp:BoundField  HeaderText="Year 3" DataField="fGPtbl_CUnit_TO_Year3" /> 
                       <asp:BoundField  HeaderText="Amount Year 3" DataField="fGPtbl_CUnit_TO_Amount_Year3" /> 
                       <asp:BoundField  HeaderText="Year 2" DataField="fGPtbl_CUnit_TO_Year2" />
                       <asp:BoundField  HeaderText="Amount Year 2" DataField="fGPtbl_CUnit_TO_Amount_Year2" />  
                       <asp:BoundField  HeaderText="Year 1" DataField="fGPtbl_CUnit_TO_Year1" /> 
                       <asp:BoundField  HeaderText="Amount Year 1" DataField="fGPtbl_CUnit_TO_Amount_Year1" /> 


                       <asp:BoundField  HeaderText="Social Category" DataField="Enterprise_Social_Category" /> 
                       <asp:BoundField  HeaderText="Product" DataField="Description_Stores_Products_Services" /> 
                       

                        
                   </Columns>
                   
              </asp:GridView>


</asp:Content>

