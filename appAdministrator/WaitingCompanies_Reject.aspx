<%@ Page Title="GP::Reject Company" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="WaitingCompanies_Reject.aspx.cs" Inherits="appAdministrator_WaitingCompanies_Reject" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Validate(sender, args) {

            var gridView = document.getElementById("<%=gvDocumentDetail.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }/*dfdsf*/
            }
            args.IsValid = false;
        }
    </script>

    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>

    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left" style="padding-left: 10px; padding-right: 10px;">

        <div align="left" class="headerAll">
            Reason of Rejection :
        </div>
        <div align="left" style="height: 20px;">
        </div>
        <div align="left">
            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                <tr align="left" valign="top" style="width: 100%;"  runat="server" id="trRejectionReason" visible="false">
                    <td align="left" valign="top" class="leftHeading">Rejection Type :
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdbRejectionType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbRejectionType_SelectedIndexChanged">
                            <asp:ListItem Value="1" Text="Application Rejection" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Uploaded Document Rejection"></asp:ListItem>
                        </asp:RadioButtonList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1"
                            ErrorMessage="Please Select Rejection Type" ControlToValidate="rdbRejectionType" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>


                    </td>
                </tr>

                <tr align="left" valign="top" style="width: 100%;" runat="server" id="trUploads" visible="false">

                    <td colspan="2">
                        <table border="0" cellpadding="0" cellspacing="1" width="100%" runat="server" id="tableUploads">
                            <tr align="left" valign="top" style="width: 100%;">
                                <td>Uploaded Documents</td>
                            </tr>
                            <tr align="left" valign="top" style="width: 100%;">
                                <td><%--OnRowDataBound="GridView1_RowDataBound"
                                        OnRowCommand="gvDocumentDetail_RowCommand"--%>
                                    <asp:GridView ID="gvDocumentDetail" runat="server" Width="100%" DataKeyNames="fGPtbl_DU_UnitId" OnRowDataBound="GridView1_RowDataBound"
                                        BorderWidth="0" CellPadding="10" CellSpacing="1" BackColor="#FFFFFF" AutoGenerateColumns="false"
                                        AllowSorting="false"
                                        AllowPaging="false">
                                        <HeaderStyle CssClass="GrdviewHeader" Height="40px" />
                                        <EmptyDataTemplate>
                                            <center><strong>No document provided</strong></center>
                                        </EmptyDataTemplate>
                                        <RowStyle CssClass="GrdviewRowOdd" />
                                        <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No./ क्र. सं." HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="UnitId" DataField="fGPtbl_DU_UnitId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                                            <asp:BoundField HeaderText="FormTypeId" DataField="fGPtbl_DU_FormtypeId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                                            <asp:BoundField HeaderText="fGPtbl_DU_Id" DataField="fGPtbl_DU_Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                                            <asp:BoundField HeaderText="FileName" DataField="fGPtbl_DU_FileName" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                                            <asp:BoundField HeaderText="FilePath" DataField="fGPtbl_DU_FilePath" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                                            <asp:BoundField HeaderText="DU_Name" DataField="fGPtbl_DU_Name" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                                            <asp:TemplateField HeaderText="NAME">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLName" runat="server" Visible="true" Text='<%#Eval("fGPtbl_DU_Name") %>' Target="_blank"></asp:HyperLink>
                                                    <asp:HiddenField ID="HiddenFieldpath" runat="server" Value='<%#Eval("fGPtbl_DU_FilePath") %>' />
                                                    <asp:HiddenField ID="HiddenFieldname" runat="server" Value='<%#Eval("fGPtbl_DU_FileName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRow" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please select at least one record."
                                        ClientValidationFunction="Validate" ForeColor="Red" ValidationGroup="1"></asp:CustomValidator>
                                    <br />
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>

                <tr align="left" valign="top" style="width: 100%;">
                    <td align="left" valign="top" class="leftHeading">Reason of Rejection :
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" MaxLength="5000"
                            CssClass="txt_bx" ValidationGroup="11" Height="122px" Width="396px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFValidatorReason" runat="server" ValidationGroup="11"
                            ErrorMessage="Please provide reason to reject" ControlToValidate="txtReason" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="TopRight" TargetControlID="RFValidatorReason">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>
        </div>
        <div align="left" style="height: 20px;">
        </div>
        <div align="right" style="padding-right: 110px;">
            <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btnCls"
                ValidationGroup="11" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="btnCls"
            CausesValidation="false" OnClick="btnBack_Click" />
        </div>

    </div>
</asp:Content>

