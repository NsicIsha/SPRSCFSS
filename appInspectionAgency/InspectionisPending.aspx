<%@ Page Title="Inspection Date already Issued but Inspection is Pending" Language="C#" MasterPageFile="~/appMaster/Agency.master" AutoEventWireup="true" CodeFile="InspectionisPending.aspx.cs" Inherits="appInspectionAgency_InspectionisPending" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .ErrMsgLbl
        {
        	font-weight:bold;
        	font-style:italic;
        	text-decoration:underline;
        	color:Red;
        	}
    </style>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">
                Companies waiting for Inspection
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" height="400" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="viewMessage" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width: 100%;">
                                <td align="center" valign="top" style="width: 100%;">
                                    <asp:HiddenField ID="HiddenFieldUnit" runat="server" />
                                    <asp:HiddenField ID="HiddenFieldFormtype" runat="server" />
                                    <asp:Label ID="lblMsg" runat="server" CssClass="reportCenterText"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="viewData" runat="server">
                    
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                               <%if(pendingAction==1){ %>
                                                
                                                    <div id="divAction" runat="server" class="Action_Div" style="background-color:#ECF8FB; height:20px;">
                                                        <div id="Action_DivInner_1" align="center">
                                                            <asp:Label ID="LabelAction" runat="server" CssClass="MasterAction"></asp:Label>
                                                        </div>
                                                        <div id="Action_DivInner_2" align="center">
                                                            <img id="imgAction" onclick="FunZoneActionClose()" src="../Images/Admin/delete.png" border="0" width="16px" height="15px" alt="CLOSE" style="cursor:pointer;" />
                                                        </div>
                                                    </div>
                                                    <asp:RoundedCornersExtender
                                                        ID="RoundedCornersExtender2" runat="server"
                                                        TargetControlID="divAction"
                                                        BorderColor="#22518F"
                                                        Radius="10"
                                                        Corners="All"
                                                    ></asp:RoundedCornersExtender>
                                                <%} %>
                                            </td>
                                            <td valign="top" align="right" style="width: 30%;">
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </td>
                            </tr>
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="left" valign="top" style="width:100%;">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    
                        <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                            <tr align="left" valign="top" style="width: 100%;">
                                <td align="left" valign="top" style="width: 100%;">
                                    <asp:GridView ID="gvPending" runat="server" Width="100%" BorderWidth="0" CellPadding="3"
                                        CellSpacing="1" BackColor="#FFFFFF" AutoGenerateColumns="false"
                                        DataKeyNames="fGPtbl_IS_UnitId"
                                        AllowSorting="false" AllowPaging="false" 
                                       
                                         onrowdatabound="gvPending_RowDataBound" >
                                        <HeaderStyle CssClass="GrdviewHeader" />
                                        <RowStyle CssClass="GrdviewRowOdd" />
                                        <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate><%#Container.DataItemIndex+1 %> 
                                                    <asp:HiddenField runat="server" ID="hdnUnitId" Value='<%# Eval("fGPtbl_IS_UnitId") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnAgencyId" Value='<%# Eval("fGPtbl_IS_AgencyId") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnFormtypeId" Value='<%# Eval("fGPtbl_IS_FormtypeId") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnCompanyId" Value='<%# Eval("fGPtbl_CUnit_CompanyId") %>' />
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_MF_Name"
                                                HeaderText="Type" />
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_Company_Name"
                                                HeaderText="Company" />
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_CUnit_SSIRegistrationNo"
                                                HeaderText="SSI Registration No." />
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_Company_PAN"
                                                HeaderText="PAN" />
                                            <asp:BoundField HtmlEncode="false" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_IS_DateStatus4"
                                                HeaderText="Inspection<br>Date" />
                                            <asp:TemplateField HeaderText="View<br>Details"><ItemTemplate><asp:ImageButton ID="btnView" runat="server" ImageUrl="~/Images/Admin/view.png" border="0" CommandName="ViewDetails"
                                                        AlternateText="VIEW UNIT DETAILS"
                                                        ToolTip="VIEW UNIT DETAILS" /></ItemTemplate></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload<br>report">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" ToolTip="Upload Inspection Report" CommandName="upload"
                                                    CommandArgument='<%# Eval("fGPtbl_IS_UnitId") %>' CssClass="btnCls" CausesValidation="false"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
    </table>
    
    
    
</asp:Content>

