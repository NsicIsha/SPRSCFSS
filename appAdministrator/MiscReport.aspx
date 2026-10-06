<%@ Page Title="" Language="C#"   AutoEventWireup="true" CodeFile="MiscReport.aspx.cs" Inherits="appAdministrator_MiscReport" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta name="description" content="nsic certificate,government purchase,single point registration scheme,

government stores purchase programme,public procurement policy,gazette notification,free tenders,

exemption from emd,micro & small enterprises (MSEs) registration,provisional registration certificate,

single point registration scheme certificate validity is for the period of one year,

government purchase enlistment certificate" />

 

<meta name="keywords" content="nsic certificate,gp,government purchase,spr,SPRS Certificate,single point registration scheme,

government stores purchase programme,public procurement policy,gazette notification,free tenders,

exemption from emd,micro & small enterprises (MSEs) registration,provisional registration certificate,

single point registration scheme certificate validity is for the period of one year,

government purchase enlistment certificate" />


     <title>Single Point Registration Scheme (SPRS)</title>

    <script language="javascript" type="text/javascript" src="../JScript/jquery-1.7.1.js"></script>

    <link type="text/css" rel="Stylesheet" href="CSS/Style.css" />
     <script src="SlidShow/jquery-1.2.6.min.js" type="text/javascript"></script>

    <script src="SlidShow/JScript.js" type="text/javascript"></script>
    
     <style type="text/css">
        #slideshow
        {
            position: relative;
            height: 223px;
            width: 975px;
        }
        #slideshow IMG
        {
            position: absolute;
            top: 0;
            left: 0;
            z-index: 8;
            opacity: 0.0;
        }
        #slideshow IMG.active
        {
            z-index: 10;
            opacity: 1.0;
        }
        #slideshow IMG.last-active
        {
            z-index: 9;
        }
    </style>
    

</head>
    <body>
    <center>
        <form id="form1" runat="server">
 
  <div align="center">
  
  <div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text="Dashboard Report" CssClass="headerAll"></asp:Label>
      <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnBackView_Click"/>
    </div>
    <div style="height:10px;"></div>


      <asp:GridView ID="grdData" runat="server" CellPadding="5"   CellSpacing="5"
               
               GridLines="Both"  PageIndex="0" EnableViewState="false" EnableSortingAndPagingCallbacks="true"
                  AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" 
                    Width="100%" >
                
                   
                   <Columns>
                        <asp:BoundField HeaderText="Zone Name" DataField="ZoneName" /> 
                        <asp:BoundField HeaderText="Branch Name" DataField="BranchName" /> 
                        <asp:BoundField HeaderText="Company Name" DataField="Company_Name" /> 
                        <asp:BoundField HeaderText="PAN" DataField="PAN_Card_Number" /> 
                       <%-- <asp:BoundField HeaderText="Constitution Name" DataField="Step2_FIRM_Constitution_Name" /> --%>

                        <asp:BoundField HeaderText="Address" DataField="HEAD_OFFICE_Address" /> 

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
                       <asp:BoundField  HeaderText="Product" DataField="fGPtbl_CUnit_Store_DescriptionStoresProducts" /> 
                       

                        
                   </Columns>
                   
              </asp:GridView>


  
<%--        <asp:GridView ID="grdData" runat="server" CellPadding="5"   CellSpacing="5"
               
               GridLines="Both"  PageIndex="0" EnableViewState="false" EnableSortingAndPagingCallbacks="true"
                  AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" 
                    Width="100%" >
                
                   
                   <Columns>
                        <asp:BoundField HeaderText="Branch Name" DataField="BranchName" /> 
                        <asp:BoundField HeaderText="PAN Card Number" DataField="PAN_Card_Number" /> 
                        <asp:BoundField HeaderText="Bank Status" DataField="MSME_Data_Bank_Status"  />
                        <asp:BoundField  HeaderText="Under_IT_Act_2000" DataField="Under_IT_Act_2000" /> --%>

                        <%--<asp:BoundField HeaderText="Company_AdharNumber" DataField="Company_AdharNumber" /> 
                        <asp:BoundField HeaderText="GSTNumber" DataField="GSTNumber" /> 
                        <asp:BoundField HeaderText="SC_ST_Hub_Initiative" DataField="SC_ST_Hub_Initiative"  />
                        <asp:BoundField  HeaderText="Company_Name" DataField="Company_Name" /> 
                       <asp:BoundField HeaderText="Company_ApplicationDatee" DataField="Company_ApplicationDate" /> 
                        <asp:BoundField HeaderText="HEAD_OFFICE_Address" DataField="HEAD_OFFICE_Address" /> 
                        <asp:BoundField HeaderText="HEAD_OFFICE_Pin_Code" DataField="HEAD_OFFICE_Pin_Code"  />
                        <asp:BoundField  HeaderText="HEAD_OFFICE_City" DataField="HEAD_OFFICE_State" /> 
                       <asp:BoundField HeaderText="HEAD_OFFICE_Phone" DataField="HEAD_OFFICE_Phone" /> 
                        <asp:BoundField HeaderText="HEAD_OFFICE_Mobile" DataField="HEAD_OFFICE_Mobile" /> 
                        <asp:BoundField HeaderText="HEAD_OFFICE_Fax" DataField="HEAD_OFFICE_Fax"  />
                        <asp:BoundField  HeaderText="HEAD_OFFICE_Email" DataField="HEAD_OFFICE_Email" /> 
                       <asp:BoundField HeaderText="BRANCH_OFFICE_Address" DataField="BRANCH_OFFICE_Address" /> 
                        <asp:BoundField HeaderText="BRANCH_OFFICE_Pin_Code" DataField="BRANCH_OFFICE_Pin_Code" /> 
                        <asp:BoundField HeaderText="BRANCH_OFFICE_State" DataField="BRANCH_OFFICE_State"  />
                        <asp:BoundField  HeaderText="BRANCH_OFFICE_City" DataField="BRANCH_OFFICE_City" /> --%>






                        <%--<asp:BoundField HeaderText="Company_AdharNumber" DataField="Company_AdharNumber" /> 
                        <asp:BoundField HeaderText="GSTNumber" DataField="GSTNumber" /> 
                        <asp:BoundField HeaderText="SC_ST_Hub_Initiative" DataField="SC_ST_Hub_Initiative"  />
                        <asp:BoundField  HeaderText="Any_Special_Category_of_Enterpreneur" DataField="Any_Special_Category_of_Enterpreneur" /> 
                       <asp:BoundField HeaderText="Category_Unit_Size" DataField="Category_Unit_Size" /> 
                        <asp:BoundField HeaderText="Step2_Factory_Address" DataField="Step2_Factory_Address" /> 
                        <asp:BoundField HeaderText="Step2_SSIRegistrationNo" DataField="Step2_SSIRegistrationNo"  />
                        <asp:BoundField  HeaderText="Step2_Udyog_Aadhar_Memorandum_Date" DataField="Step2_Udyog_Aadhar_Memorandum_Date" /> 
                       <asp:BoundField HeaderText="Step2_Date_of_Incorporation" DataField="Step2_Date_of_Incorporation" /> 
                        <asp:BoundField HeaderText="Step2_Date_of_Commencement" DataField="Step2_Date_of_Commencement" /> 
                        <asp:BoundField HeaderText="Step2_FIRM_Constitution_Name" DataField="Step2_FIRM_Constitution_Name"  />
                        <asp:BoundField  HeaderText="Step3_FIRM_RegisteredUnder_Name" DataField="Step3_FIRM_RegisteredUnder_Name" /> 
                       <asp:BoundField HeaderText="Step3_FIRM_CUnit_3E" DataField="Step3_FIRM_CUnit_3E" /> 
                        <asp:BoundField HeaderText="Step3_FIRM_CUnit_3F" DataField="Step3_FIRM_CUnit_3F" /> 
                        <asp:BoundField HeaderText="Step3_FIRM_CUnit_3G" DataField="Step3_FIRM_CUnit_3G"  />
                        <asp:BoundField  HeaderText="Step3_FIRM_CUnit_3H" DataField="Step3_FIRM_CUnit_3H" /> 



                        <asp:BoundField HeaderText="Step3_FIRM_CUnit_3I" DataField="Step3_FIRM_CUnit_3I" /> 
                        <asp:BoundField HeaderText="Step3_FIRM_CUnit_4A" DataField="Step3_FIRM_CUnit_4A" /> 
                        <asp:BoundField HeaderText="Step3_FIRM_CUnit_4B" DataField="Step3_FIRM_CUnit_4B"  />
                        <asp:BoundField  HeaderText="Description_Stores_Products_Services" DataField="Description_Stores_Products_Services" /> 
                       <asp:BoundField HeaderText="Ref_BIS_Specification" DataField="Ref_BIS_Specification" /> 
                        <asp:BoundField HeaderText="Model_Brand" DataField="Model_Brand" /> 
                        <asp:BoundField HeaderText="Limiting_Size_Capacity_Rating" DataField="Limiting_Size_Capacity_Rating"  />
                        <asp:BoundField  HeaderText="Monthly_Production_Capacity_per_shift" DataField="Monthly_Production_Capacity_per_shift" /> 
                       <asp:BoundField HeaderText="No_of_shifts_factory_normally_works" DataField="No_of_shifts_factory_normally_works" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_1B_Factory_Address" DataField="fGPtbl_CUnit_1B_Factory_Address" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_3D" DataField="fGPtbl_CUnit_3D"  />
                        <asp:BoundField  HeaderText="fGPtbl_CUnit_5A1" DataField="fGPtbl_CUnit_5A1" /> 
                       <asp:BoundField HeaderText="fGPtbl_CUnit_5A2" DataField="fGPtbl_CUnit_5A2" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_5BB1" DataField="fGPtbl_CUnit_5BB1" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_5BB2" DataField="fGPtbl_CUnit_5BB2"  />
                        <asp:BoundField  HeaderText="fGPtbl_CUnit_5C" DataField="fGPtbl_CUnit_5C" /> 



                       <asp:BoundField HeaderText="fGPtbl_CUnit_6A1" DataField="fGPtbl_CUnit_6A1" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_6A2" DataField="fGPtbl_CUnit_6A2" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_6A3" DataField="fGPtbl_CUnit_6A3"  />
                        <asp:BoundField  HeaderText="fGPtbl_CUnit_6B" DataField="fGPtbl_CUnit_6B" /> 
                       <asp:BoundField HeaderText="fGPtbl_CUnit_6B2" DataField="fGPtbl_CUnit_6B2" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_7A" DataField="fGPtbl_CUnit_7A" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_7B" DataField="fGPtbl_CUnit_7B"  />
                        <asp:BoundField  HeaderText="fGPtbl_CUnit_7C" DataField="fGPtbl_CUnit_7C" /> 
                       <asp:BoundField HeaderText="fGPtbl_CUnit_7D" DataField="fGPtbl_CUnit_7D" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_7E" DataField="fGPtbl_CUnit_7E" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_8" DataField="fGPtbl_CUnit_8"  />
                        <asp:BoundField  HeaderText="fGPtbl_CUnit_9" DataField="fGPtbl_CUnit_9" /> 
                       <asp:BoundField HeaderText="fGPtbl_CUnit_10" DataField="fGPtbl_CUnit_10" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_11" DataField="fGPtbl_CUnit_11" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_12A" DataField="fGPtbl_CUnit_12A"  />
                        <asp:BoundField  HeaderText="fGPtbl_CUnit_12B" DataField="fGPtbl_CUnit_12B" /> 




                        <asp:BoundField HeaderText="fGPtbl_CUnit_12C" DataField="fGPtbl_CUnit_12C" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_13A" DataField="fGPtbl_CUnit_13A" /> 
                        <asp:BoundField HeaderText="fGPtbl_CUnit_13B" DataField="fGPtbl_CUnit_13B"  />
                        <asp:BoundField  HeaderText="Step6_LastYearTurnover" DataField="Step6_LastYearTurnover" /> 
                       <asp:BoundField HeaderText="fGPtbl_PD_UnitAmount" DataField="fGPtbl_PD_UnitAmount" /> 
                        <asp:BoundField HeaderText="fGPtbl_PD_AgencyAmount" DataField="fGPtbl_PD_AgencyAmount" /> 
                        <asp:BoundField HeaderText="fGPtbl_PD_UnitServiceTax" DataField="fGPtbl_PD_UnitServiceTax"  />
                        <asp:BoundField  HeaderText="fGPtbl_PD_AgencyServiceTax" DataField="fGPtbl_PD_AgencyServiceTax" /> 
                       <asp:BoundField HeaderText="fGPtbl_PD_UnitSlabName" DataField="fGPtbl_PD_UnitSlabName" /> 
                        <asp:BoundField HeaderText="fGPtbl_PD_AgencySlabName" DataField="fGPtbl_PD_AgencySlabName" /> 
                        <asp:BoundField HeaderText="fGPtbl_PD_AgencySlabInstruction" DataField="fGPtbl_PD_AgencySlabInstruction"  />
                        <asp:BoundField  HeaderText="fGPtbl_PD_CUser" DataField="fGPtbl_PD_CUser" /> 
                       <asp:BoundField HeaderText="fGPtbl_PD_UnitSlabInstruction" DataField="fGPtbl_PD_UnitSlabInstruction" /> --%>
                         
                        
                 <%--  </Columns>
                   
              </asp:GridView>--%>
          
         
         
        

      

         
         
  
    
    
  
  </div>

 

</form>
    </center>
    
</body>
    </html>