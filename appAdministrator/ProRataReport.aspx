<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="ProRataReport.aspx.cs" Inherits="appAdministrator_ProRataReport" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .clsDDLhidden {
            width: 130px;
            background-color: #dddddd;
            border: #999999 solid 1px;
            padding: 3px;
            display: none;
        }

        .btnclsfincial {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 13px;
            background-color: White;
            color: Black;
            font-weight: bold;
            width: 160px;
        }
    </style>
    <%--sds--%>

    <script type="text/javascript" language="javascript">
        function validdatefrom() {
            var txt = "Please Fill Required Date";
            var opt = 0;
            var Datefrom = document.getElementById("<%=txt_fromdate.ClientID %>").value;
           var dateto = document.getElementById("<%=txt_todate.ClientID %>").value;
           var fromdateval = new Date(Datefrom);
           if (Datefrom == "") {
               txt += "\n - From date.";
               opt = 1;
           }

           if (fromdateval <= new Date('03/31/2015')) {
               txt += "\n- Incorrect date. Any date before 31 March, 2015 is not valid.";
               opt = 1;
           }
           if (dateto == "") {
               txt += "\n -  To Date.";
               opt = 1;
           }
           if (opt == "1") {
               alert(txt);
               document.getElementById("<%=txt_fromdate.ClientID %>").value = "";
               document.getElementById("<%=txt_fromdate.ClientID %>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text=" Report" CssClass="headerAll"></asp:Label>
    </div>
    <table cellpadding="2" cellspacing="2" border="0" style="padding-left: 10px; width: 100%; margin-top: 20px;">
        <tr>
            <td colspan="4">
                <asp:HiddenField ID="Btn_value" runat="server" Value="" />
            </td>


        </tr>

        <tr>
            <td align="left" valign="top" colspan="3">Fromdate
                <asp:TextBox ID="txt_fromdate" runat="server" Text=""></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fromdate" MaskType="Date"
                    Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                    ClearTextOnInvalid="true">
                </asp:MaskedEditExtender>
                <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlToValidate="txt_fromdate" ControlExtender="MaskedEditExtender1"
                    InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                    TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>
                Todate
                <asp:TextBox ID="txt_todate" runat="server" Text=""></asp:TextBox>

                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_todate" MaskType="Date"
                    Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                    ClearTextOnInvalid="true">
                </asp:MaskedEditExtender>
                <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlToValidate="txt_todate" ControlExtender="MaskedEditExtender3"
                    InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                    TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>

            </td>
            <td align="left" valign="top">
                <asp:UpdatePanel ID="Update_bton" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btn_search" runat="server" Text="Search"
                            OnClientClick="javascript:return validdatefrom();" OnClick="btn_search_Click" CausesValidation="false" CssClass="btnCls" />

                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>

        </tr>

        <tr>
            <td colspan="4" align="right">
                <asp:DropDownList ID="ddl_category_cast" runat="server" CssClass="clsDDLhidden">
                </asp:DropDownList>
                <asp:UpdateProgress ID="updProgress"
                    AssociatedUpdatePanelID="Update_bton"
                    runat="server">
                    <ProgressTemplate>
                        <img alt="progress" src="../Images/Admin/loading.gif" />
                        Processing...            
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <%-- <asp:UpdateProgress ID="UpdateProgress1"
    AssociatedUpdatePanelID="btn_search"
    runat="server">
        <ProgressTemplate>            
        <img alt="progress" src="../Images/Admin/loading.gif"/>
           Processing...            
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
            </td>
        </tr>

        <tr>
            <td colspan="4" align="left">
                <%--onrowdatabound="grd_report_Databound"--%>
                <%--                        OnRowDataBound="GridView1_RowDataBound"
    OnDataBound="GridView1_DataBound"--%>
                <%--onrowdatabound="grd_report_Databound"--%>


                <%--            onrowdatabound="grd_report_Databound"--%>
                <asp:GridView ID="grd_report" runat="server" CellPadding="5"
                    CellSpacing="1" GridLines="Both" AutoGenerateColumns="false" Width="96%"
                    BorderWidth="2" HeaderStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Center" ShowFooter="false" AllowPaging="false"
                    OnRowDataBound="grd_report_Databound"
                    EmptyDataText="No data found!"
                    OnSelectedIndexChanged="grd_report_SelectedIndexChanged">
                    <%-- <HeaderStyle CssClass="dynRowHeader" />
               <RowStyle CssClass="dynRowEven" />
               <AlternatingRowStyle CssClass="dynRowOdd" />
               <FooterStyle CssClass="dynRowHeader" />--%>

                    <%--<Columns>--%>
                    <%--  <asp:TemplateField HeaderText="S.No." >
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="Zone">
                 <ItemTemplate>
                   <asp:Label ID="lbl_Zonename" runat="server" Text='<%#Eval("ZoneName") %>'></asp:Label>
                 <asp:HiddenField ID="hdn_Zone_id" runat="server" Value='<%#Eval("ZoneID") %>' />
                 </ItemTemplate>
                  <FooterTemplate>
                <asp:Label ID="lbl_totaltext_footer" runat="server" Text="Total"></asp:Label>
              
              </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Branch">
               <ItemTemplate>
                 <asp:Label ID="lbl_Branch_name" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                  <asp:HiddenField ID="hdn_branch_id" runat="server" Value='<%#Eval("BranchID") %>' />
               </ItemTemplate>
            
            </asp:TemplateField>



            <asp:TemplateField HeaderText="Annual Target">
               <ItemTemplate>
                 <asp:Label ID="lbl_AnnualTarget" runat="server" Text='<%#Eval("AnnualTarget") %>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                 <asp:Label ID="lbl_AnnualTarget_Footer" runat="server" Text=""></asp:Label>
               </FooterTemplate>
            </asp:TemplateField>


            <asp:TemplateField  HeaderText="Pro rata Target (In Lacs)">
               <ItemTemplate>
                 <asp:Label ID="lbl_ProrataTarget" runat="server" Text='<%#Eval("Pro_Rata_Target") %>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                 <asp:Label ID="lbl_ProrataTarget_Footer" runat="server" Text=""></asp:Label>
               </FooterTemplate>
            </asp:TemplateField>
            
            
            <asp:TemplateField  HeaderText="Achievement  (In Lacs)">
               <ItemTemplate>
                 <asp:Label ID="lbl_Achievement" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
               
               </ItemTemplate>
               
               <FooterTemplate>
                 <asp:Label ID="lbl_AchievementFooter" runat="server" Text=""></asp:Label>
               </FooterTemplate>
            
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Achievement Branch Wise (In Lacs) ">
              <ItemTemplate>
                <asp:Label ID="lbl_AchievementBranchWise" runat="server" Text='<%#Eval("Achievement_Branch_Wise") %>'></asp:Label>
              </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lbl_AchievementBranchWiseFooter" runat="server" Text=""></asp:Label>
            </FooterTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Achievement  Zone (In Lacs)" ItemStyle-ForeColor="Red" FooterStyle-ForeColor="Red">
              <ItemTemplate>
                <asp:Label ID="lbl_AchievementZone" runat="server" Text='<%#Eval("Achievement_Zone") %>'></asp:Label>
              
                  <asp:HiddenField ID="hdnZoneId" runat="server"
                    Value='<%# Eval("ZoneID") %>' />
              </ItemTemplate>
              <FooterTemplate>
                <asp:Label ID="lbl_AchievementZoneFooter" runat="server" Text=""></asp:Label>
              
              </FooterTemplate>
            
            </asp:TemplateField>
               
               
               
               </Columns>--%>


                    <Columns>
                        <asp:TemplateField HeaderText="Zone" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Zonename" runat="server" Text='<%#Eval("ZoneName") %>'></asp:Label>
                                <asp:HiddenField ID="hdn_Zone_id" runat="server" Value='<%#Eval("ZoneID") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_totaltext_footer" runat="server" Text="Total"></asp:Label>

                            </FooterTemplate>
                        </asp:TemplateField>


                        <%-- <asp:BoundField DataField="ZoneName" HeaderText="Zone Office" />--%>
                        <asp:BoundField DataField="BranchName" HeaderText="Branch" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="AnnualTarget" HeaderText="Annual Target (In Lacs)" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="Pro_Rata_Target" HeaderText="Pro rata Target (In Lacs)" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Achievement" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="Achievement_Branch_Wise" HeaderText="% Achievement Branch Wise (In Lacs)" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="Achievement_Zone" HeaderText="% Achievement  Zone (In Lacs)" DataFormatString="{0:N2}" />
                    </Columns>


                </asp:GridView>

            </td>

        </tr>

        <%-- <tr>
        <td colspan="4" >
            <span  style="color:Red;"> Total Fresh Company Register  </span>:  <asp:Label ID="lbl_total_fresh_backlog" runat="server" Text=""></asp:Label>
                <asp:HiddenField ID="hdn_total_fresh_backlog" runat="server" Value="" />
        </td>
     </tr>
     <tr>
        <td colspan="4" >
            <span  style="color:Red;"> Total Renewal Company Register  </span>:  <asp:Label ID="lbl_total_Renewal_backlog" runat="server" Text=""></asp:Label>
                <asp:HiddenField ID="hdn_total_Renewal_backlog" runat="server" Value="" />
        </td>
     </tr>

     <tr>
        <td colspan="4" >
            <span  style="color:Red;"> Total Amendment Count  </span>:  <asp:Label ID="lbl_total_Amendment" runat="server" Text=""></asp:Label>
                <asp:HiddenField ID="hdn_total_Amendment" runat="server" Value="" />
        </td>
     </tr>--%>
    </table>
</asp:Content>

