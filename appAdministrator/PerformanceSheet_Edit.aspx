<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="PerformanceSheet_Edit.aspx.cs" Inherits="appAdministrator_PerformanceSheet_Edit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table cellpadding="0" cellspacing="0" width="700px"  style="margin-top:10px;">
  
<tr>
            <td align="left"  colspan="3" class="headerAll" style="width: 170px;">
                 Edit Performance Sheet Details
            </td>
        </tr>
        
        <tr>
          <td colspan="3" style="height:10px;"></td>
        </tr>

<tr style="width:800px;">
  <td align="left" style="padding-left:4px; width:300px;"><b>Enter  PAN Number </b> </td>
    <td align="left" style="width:300px;">
     
     <asp:TextBox ID="txt_PAN" runat="server" Text="" Width="200px"></asp:TextBox>
        <asp:RequiredFieldValidator ValidationGroup="GetPan" ID="rfdPan" ControlToValidate="txt_PAN" ErrorMessage="PAN Required" runat="server"></asp:RequiredFieldValidator>

    </td>
<td align="left"><asp:Button ID="btn_status_track" runat="server" 
        Text="Fetch Details" Width="120" CssClass="btnCls"   
        onclick="btn_status_track_Click"  ValidationGroup="GetPan" /></td>
   
</tr>
 <tr><td colspan="3" style="height:10px;"></td></tr>
 
   <tr><td colspan="3" style="width:750px; padding-left:10px;"><%--PrimaryId--%>
     <asp:GridView ID="gv_PerformanceSheet" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="Id" 
                                    OnRowEditing="gv_PerformanceSheet_RowEditing"
                                    OnRowUpdating="gv_PerformanceSheet_RowUpdating"
                                    OnRowDeleting="gv_PerformanceSheet_RowDeleting">
                                    <HeaderStyle CssClass="GrdviewHeader" Height="30" />
                                    <RowStyle CssClass="GrdviewRowOdd" />
                                    <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                    <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Order Number, Date & Authority placing the contracts">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_OrderNumber" runat="server" Text='<%# Bind("OrderNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_OrderNumber" TextMode="MultiLine" runat="server" Text='<%# Bind("OrderNumber") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                           <%-- <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_OrderNumber" runat="server" TextMode="MultiLine" Width="120px" ValidationGroup="gridd"></asp:TextBox>

                                                
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date of Receipt of Order ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Date_of_ReceiptOrder" runat="server" Text='<%# Bind("Date_of_ReceiptOrder") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Date_of_ReceiptOrder" runat="server" TextMode="MultiLine" Height="100px"
                                                    Text='<%# Bind("Date_of_ReceiptOrder") %>'></asp:TextBox>
                                                <%--<asp:RegularExpressionValidator SetFocusOnError="true" Display="Dynamic" CssClass="text-danger" ID="resssv" runat="server" ControlToValidate="txt_Date_of_ReceiptOrder" ValidationGroup="gridd" ErrorMessage="Date should be in MM/dd/YYYY format" ValidationExpression="^(0?[1-9]|1[0-2])/(0?[1-9]|1[0-9]|2[0-9]|3[01])/\d{4}$"></asp:RegularExpressionValidator>--%>

                                            </EditItemTemplate>
                                           <%-- <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_Date_of_ReceiptOrder" runat="server"
                                                    ValidationGroup="gridd"></asp:TextBox>
                                                <asp:RegularExpressionValidator SetFocusOnError="true" Display="Dynamic" CssClass="text-danger" ID="resssv" runat="server" ControlToValidate="txt_foot_Date_of_ReceiptOrder" ValidationGroup="gridd" ErrorMessage="Date should be in MM/dd/YYYY format" ValidationExpression="^(0?[1-9]|1[0-2])/(0?[1-9]|1[0-9]|2[0-9]|3[01])/\d{4}$"></asp:RegularExpressionValidator>

                                                
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Stores/Product/Services">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Store_Produt_Service_Desc" runat="server" Text='<%# Bind("Store_Produt_Service_Desc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Store_Produt_Service_Desc" runat="server" TextMode="MultiLine" Height="100px" Text='<%# Bind("Store_Produt_Service_Desc") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                           <%-- <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_Store_Produt_Service_Desc" ValidationGroup="gridd" runat="server"></asp:TextBox>
                                               
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Quantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Quantity" runat="server"
                                                    Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:TextBox ID="txt_foot_Quantity" ValidationGroup="gridd" TextMode="SingleLine" runat="server"></asp:TextBox>
                                                <asp:RegularExpressionValidator SetFocusOnError="true" Display="Dynamic" CssClass="text-danger" ID="rfvPinVxxal" ControlToValidate="txt_foot_Quantity" runat="server" ErrorMessage="Only Numbers allowed" ValidationGroup="gridd" ValidationExpression="\d+"></asp:RegularExpressionValidator>

                                              
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value (in Rs.)" HeaderStyle-ForeColor="Red">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Value" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Value" runat="server"
                                                    Text='<%# Bind("Value") %>'></asp:TextBox>

                                                <%--<asp:RegularExpressionValidator SetFocusOnError="true" Display="Dynamic" CssClass="text-danger" ID="rfvPinVaxl" ControlToValidate="txt_Value" runat="server" ErrorMessage="Only Numbers allowed"  ValidationExpression="\d+"></asp:RegularExpressionValidator>--%>

                                            </EditItemTemplate>
                                           <%-- <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_Value" MaxLength="9" ValidationGroup="gridd" TextMode="SingleLine" runat="server"></asp:TextBox>
                                                <asp:RegularExpressionValidator SetFocusOnError="true" Display="Dynamic" CssClass="text-danger" ID="rfvPinVaxl" ControlToValidate="txt_foot_Value" runat="server" ErrorMessage="Only Numbers allowed" ValidationGroup="gridd" ValidationExpression="\d+"></asp:RegularExpressionValidator>

                                                <asp:RequiredFieldValidator ID="reqpisncodValue" runat="server" Display="Dynamic" ErrorMessage="Enter Value"
                                                    ControlToValidate="txt_foot_Value" SetFocusOnError="true" ValidationGroup="gridd"></asp:RequiredFieldValidator>

                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name of Consignee">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Name_Consignee" runat="server" Text='<%# Bind("Name_Consignee") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Name_Consignee" runat="server" Width="80px" Text='<%# Bind("Name_Consignee") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                           <%-- <FooterTemplate>
                                                <asp:TextBox ID="txt_Name_Consignee" Width="80px" runat="server"></asp:TextBox>

                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Due date of Delivery(DD/MM/YYYY)">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_DeliveryDueDate" runat="server" Text='<%# Bind("DeliveryDueDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_DeliveryDueDate" runat="server" Text='<%# Bind("DeliveryDueDate") %>'></asp:TextBox>
                                                <%--<asp:RegularExpressionValidator SetFocusOnError="true" Display="Dynamic" CssClass="text-danger" ID="recsssv" runat="server" ControlToValidate="txt_DeliveryDueDate" ValidationGroup="gridd" ErrorMessage="Date should be in MM/dd/YYYY format" ValidationExpression="^(0?[1-9]|1[0-2])/(0?[1-9]|1[0-9]|2[0-9]|3[01])/\d{4}$"></asp:RegularExpressionValidator>--%>
                                            </EditItemTemplate>
                                          <%--  <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_DeliveryDueDate" runat="server" Width="120px" ValidationGroup="gridd"></asp:TextBox>
                                                <asp:RegularExpressionValidator SetFocusOnError="true" Display="Dynamic" CssClass="text-danger" ID="rescafdeefssv" runat="server" ControlToValidate="txt_foot_DeliveryDueDate" ValidationGroup="gridd" ErrorMessage="Date should be in MM/dd/YYYY format" ValidationExpression="^(0?[1-9]|1[0-2])/(0?[1-9]|1[0-9]|2[0-9]|3[01])/\d{4}$"></asp:RegularExpressionValidator>

                                              
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date of Material Delivery(DD/MM/YYYY)" >
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_MaterialDeliveryDate" runat="server" Text='<%# Bind("MaterialDeliveryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_MaterialDeliveryDate" runat="server" Text='<%# Bind("MaterialDeliveryDate") %>'></asp:TextBox>
                                                <%--<asp:RegularExpressionValidator SetFocusOnError="true" Display="Dynamic" CssClass="text-danger" ID="recsssv" runat="server" ControlToValidate="txt_MaterialDeliveryDate" ValidationGroup="gridd" ErrorMessage="Date should be in MM/dd/YYYY format" ValidationExpression="^(0?[1-9]|1[0-2])/(0?[1-9]|1[0-9]|2[0-9]|3[01])/\d{4}$"></asp:RegularExpressionValidator>--%>

                                            </EditItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:TextBox ID="txt_foot_MaterialDeliveryDate" runat="server" Width="120px" ValidationGroup="gridd"></asp:TextBox>
                                                <asp:RegularExpressionValidator SetFocusOnError="true" Display="Dynamic" CssClass="text-danger" ID="recsssv" runat="server" ControlToValidate="txt_foot_MaterialDeliveryDate" ValidationGroup="gridd" ErrorMessage="Date should be in MM/dd/YYYY format" ValidationExpression="^(0?[1-9]|1[0-2])/(0?[1-9]|1[0-9]|2[0-9]|3[01])/\d{4}$"></asp:RegularExpressionValidator>
                                                
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Despatching particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_DespatchingParticulars" runat="server" Text='<%# Bind("DespatchingParticulars") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_DespatchingParticulars" runat="server" Width="50px" Text='<%# Bind("DespatchingParticulars") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                          <%--  <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_DespatchingParticulars" runat="server" Width="50px"></asp:TextBox>

                                              
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Remarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Remarks" runat="server" Width="50px" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                           <%-- <FooterTemplate>
                                                <asp:TextBox ID="txt_foot_Remarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Options">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_edit" runat="server" CommandArgument='<%# Bind("ordernumber") %>'
                                                    CommandName="edit" ValidationGroup="gridd" CausesValidation="false">
                                                   Edit
                                                </asp:LinkButton>
                                                &nbsp;&nbsp;
                                                 <asp:LinkButton ID="lnk_delete" runat="server" CommandArgument='<%# Bind("ordernumber") %>'
                                                     Visible="false" OnClientClick="return confirm('Are you want to delete ?');" CommandName="delete" CausesValidation="false">
                                                   Delete
                                                 </asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ValidationGroup="gridd" ID="lnk_update" runat="server" CommandArgument='<%# Bind("ordernumber") %>' CommandName="update" CausesValidation="false" >
                                                   Update
                                                </asp:LinkButton>
                                            </EditItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:Button ID="btn_add" runat="server" Text="Add New" ValidationGroup="gridd" OnClick="AddnewPerformanceSheet" />
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
   
   </td></tr>
    <tr id="trSubmit" runat="server" visible="false"><td colspan="3" style="height:10px;">

         <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" CssClass="btnCls"
                                                ValidationGroup="1" Width="150px" TabIndex="54"
                                                OnClick="btnSubmit_Click" />
        </td></tr>
   
   <tr><td colspan="3">
     <asp:Label ID="lbl_branch_account_chek" runat="server" Text=""></asp:Label>
   
   </td> </tr>
   
   
   <tr>
     <td colspan="3">
       <asp:Label ID="lbl_report_status_track" runat="server" Text=""></asp:Label>
     </td>
   
   </tr>

</table>

</asp:Content>

