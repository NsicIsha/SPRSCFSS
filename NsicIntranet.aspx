<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NsicIntranet.aspx.cs" Inherits="NsicIntranet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
  <%-- <asp:Table ID="Table2" runat="server" border="1" CellPadding="0" CellSpacing="0" Width="100%" CssClass="clsOutlineDyntblBorder">
                            </asp:Table>--%>
        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
            onrowdatabound="gvMain_RowDataBound"
            >
             <Columns> 

                 <asp:TemplateField> 
                      <ItemTemplate>
 <asp:Label ID="lblfInFTrack_1GeneratedNo" runat="server" Text='<%# Eval("fInFTrack_1GeneratedNo")%>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>

                  <asp:TemplateField> 
                      <ItemTemplate>
 <asp:Label ID="lblName" runat="server" Text='<%# Eval("fInFTrack_Name")%>'></asp:Label>
                          <br />
                          <asp:Label ID="lblAadhaar" runat="server" Text='<%# Eval("fInFTrack_Aadhaar")%>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>

                 <asp:TemplateField> 
                      <ItemTemplate>
 <asp:Label ID="Label1" runat="server" Text="C"></asp:Label> : <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("fInFTrack_Category")%>'></asp:Label>
                          <br />
                      <asp:Label ID="Label2" runat="server" Text="DoB"></asp:Label> :    <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>


            <asp:TemplateField >
                <ItemTemplate>
                   
                   
                     <asp:Repeater ID="rpEmpSearch" runat="server" >
                         <HeaderTemplate >
                             <table  style="width: 100%;" >
                                                        <tr>
                                                            <%--<td style="width:50px">
                                                                
                                                                    Examination Passed:
                                                                   
                                                               
                                                            </td>
                                                           <td style="width:50px">
                                                                
                                                                    Year of passing:
                                                                    
                                                                 
                                                            </td>
                                                            <td style="width:50px">
                                                                  University Name:
                                                                 
                                                                   
                                                               
                                                            </td>
                                                            <td style="width:50px">                                                                
                                                                  Exam Subjects:
                                                            </td>
                                                            <td style="width:50px">                                                                
                                                                  Regular/Part time/Distance:
                                                            </td>

                                                            <td style="width:50px">                                                                
                                                                  Percentage:
                                                            </td>--%>

                                                            <td style="width:50px">                                                                
                                                                  Education Type
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                  Attendance Type
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                 Exam Name
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                  Exam Subjects   
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                    Year Passing   
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                   University Name   
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                   Percentage
                                                            </td>


                                                             <%--<td style="width:50px">                                                                
                                                                  ProfEd. Education Type
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                  ProfEd. Attendance Type
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                 ProfEd. Exam Name
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                  ProfEd. Exam Subjects   
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                   ProfEd. Year Passing   
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                  ProfEd. University Name   
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                  ProfEd Percentage
                                                            </td>--%>
                                                             


                                                        </tr>
                                                         
                         </HeaderTemplate>
                                                <itemtemplate>
                                                    <table  style="width: 100%;" >
                                                         
                                                        <tr>
                                                            <td style="width:50px">
                                                                 <asp:Label ID="lblOrgName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Ed_EducationType")%>'>
                                                                    </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Ed_AttendanceType")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                            <td style="width:50px">
                                                                 <asp:Label ID="lblIndustry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Ed_ExamName")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                            <td style="width:50px">
                                                                 <asp:Label ID="lblYearOfpassing" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Ed_ExamSubjects")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                            <td style="width:50px">
                                                                 <asp:Label ID="lblRegular" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Ed_YearPassing")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                            <td style="width:50px">
                                                                 <asp:Label ID="lblPercentage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Ed_UniversityName")%>'>
                                                                     </asp:Label>
                                                            </td>

                                                            <td style="width:50px">
                                                                 <asp:Label ID="lblEd_Percentage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Ed_Percentage")%>'>
                                                                     </asp:Label>
                                                            </td>


                                                             <%--<td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_EducationType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_EducationType")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_AttendanceType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_AttendanceType")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_ExamName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_ExamName")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_ExamSubjects" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_ExamSubjects")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_YearPassing" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_YearPassing")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_UniversityName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_UniversityName")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_Percentage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_Percentage")%>'>
                                                                     </asp:Label>
                                                            </td>--%>

                                                        </tr>
                                                    </table>
                                                </itemtemplate>
                                            
                                        
                         </asp:Repeater>

                </ItemTemplate>
                
            </asp:TemplateField>



                  <asp:TemplateField >
                <ItemTemplate>
                   
                   
                     <asp:Repeater ID="rpEmpPrpEd" runat="server" >
                         <HeaderTemplate >
                             <table  style="width: 100%;" >
                                                        <tr>
                                                             <td style="width:50px">                                                                
                                                                  ProfEd. Education Type
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                  ProfEd. Attendance Type
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                 ProfEd. Exam Name
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                  ProfEd. Exam Subjects   
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                   ProfEd. Year Passing   
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                  ProfEd. University Name   
                                                            </td>
                                                             <td style="width:50px">                                                                
                                                                  ProfEd Percentage
                                                            </td>
                                                             


                                                        </tr>
                                                         
                         </HeaderTemplate>
                                                <itemtemplate>
                                                    <table  style="width: 100%;" >
                                                         
                                                        <tr>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_EducationType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_EducationType")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_AttendanceType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_AttendanceType")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_ExamName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_ExamName")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_ExamSubjects" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_ExamSubjects")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_YearPassing" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_YearPassing")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_UniversityName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_UniversityName")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblProfEd_Percentage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_ProfEd_Percentage")%>'>
                                                                     </asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </itemtemplate>
                                            
                                        
                         </asp:Repeater>

                </ItemTemplate>
                
            </asp:TemplateField>


                  <asp:TemplateField >
                <ItemTemplate>
                   
                   
                     <asp:Repeater ID="rpEmpEmp" runat="server" >
                         <HeaderTemplate >
                             <table  style="width: 100%;" >
                                                        <tr>
                                                            <td style="width:50px">
                                                                
                                                                    Name of Employer:
                                                                   
                                                               
                                                            </td>
                                                           <td style="width:50px">
                                                                
                                                                    Position Held:
                                                                    
                                                                 
                                                            </td>
                                                            <td style="width:50px">
                                                                
                                                                  Salary:
                                                                    
                                                               
                                                            </td>
                                                            <td style="width:50px">                                                                
                                                                  From Date:
                                                            </td>
                                                            <td style="width:50px">                                                                
                                                                  To Date:
                                                            </td>

                                                            
                                                        </tr>
                                                         
                         </HeaderTemplate>
                                                <itemtemplate>
                                                    <table  style="width: 100%;" >
                                                         
                                                        <tr>
                                                            <td style="width:50px">
                                                                 <asp:Label ID="lblEmployerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Expe_EmployerName")%>'>
                                                                    </asp:Label>
                                                            </td>
                                                             <td style="width:50px">
                                                                 <asp:Label ID="lblPosition" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Expe_Position")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                            <td style="width:50px">
                                                                 <asp:Label ID="lblSalaryy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Expe_Sal")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                            <td style="width:50px">
                                                                 <asp:Label ID="lblFromDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Expe_FromDt")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                            <td style="width:50px">
                                                                 <asp:Label ID="lblToDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fInFTrack_Expe_ToDt")%>'>
                                                                     </asp:Label>
                                                            </td>
                                                             
                                                        </tr>
                                                    </table>
                                                </itemtemplate>
                                            
                                        
                         </asp:Repeater>

                </ItemTemplate>
                
            </asp:TemplateField>
                 </Columns>
        </asp:GridView>
    </form>
</body>
</html>
