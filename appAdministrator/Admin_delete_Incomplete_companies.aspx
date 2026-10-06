<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Admin_delete_Incomplete_companies.aspx.cs" Inherits="appAdministrator_Admin_delete_Incomplete_companies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkboxAdmin_delete_Incomplete_companies
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                //        row.style.backgroundColor = "aqua";
            }
            else {
                //If not checked change back to original color
                //        if(row.rowIndex % 2 == 0)
                //        {
                //           //Alternating Row Color
                //           row.style.backgroundColor = "#C2D69B";
                //        }
                //        else
                //        {
                //           row.style.backgroundColor = "white";
                //        }
            }

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
    </script>
    <script type="text/javascript" language="javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        //                row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original 
                        //                if(row.rowIndex % 2 == 0)
                        //                {
                        //                   //Alternating Row Color
                        //                   row.style.backgroundColor = "#C2D69B";
                        //                }
                        //                else
                        //                {
                        //                   row.style.backgroundColor = "white";
                        //                }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll">Delete Companies</asp:Label>
        </div>

        <div align="left" style="height: 10px;">
        </div>

        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
        <div style="height: 10px;">
        </div>

        <div align="left" class="clsrowdiv">
            <div class="leftext">Search</div>
            <div class="coloncls">
                <asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL">
                    <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
                    <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Reference No." Value="3"></asp:ListItem>
                    <asp:ListItem Text="Registration No." Value="4"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="txtdiv">
                <asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox></div>
            <div class="btndiv">
                <asp:Button ID="btnSearch" runat="server" Text="Search"
                    CssClass="btnCls" OnClick="btnSearch_Click" />

            </div>


        </div>
        <br />
        <div class="txtlbl">
            <b><span style="color: Red;">
                <asp:Label ID="lbl_count_text" runat="server" Text=""></asp:Label></span></b>
        </div>

        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            <asp:GridView ID="gv_company" runat="server" AutoGenerateColumns="False"
                Width="100%" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                AllowSorting="True" AllowPaging="True" DataKeyNames="fGPtbl_Company_Id"
                OnPageIndexChanging="gv_company_PageIndexChanging"
                OnRowDeleting="gv_company_RowDeleting">
                <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                <Columns>
                    <%-- <asp:TemplateField>
        <HeaderTemplate>
          <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />
        </HeaderTemplate> 
       <ItemTemplate>
         <asp:CheckBox ID="chk_select" runat="server"  onclick = "Check_Click(this)" />
       </ItemTemplate> 
    </asp:TemplateField>  --%>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                    <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No." />
                    <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                    <asp:BoundField DataField="fGPtbl_Company_GPReferenceNo"
                        HeaderText="Reference No" />
                    <asp:BoundField DataField="regis" HeaderText="Registration No." />
                    <asp:BoundField DataField="status" HeaderText="Status" />
                    <asp:TemplateField HeaderText="Options">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_delete" runat="server" CommandArgument='<%# Bind("fGPtbl_Company_Id") %>'
                                OnClientClick="return confirm('Are you sure you want to delete ?');" CommandName="delete" CausesValidation="false">
                                                   Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
        <br />
        <center>
            <asp:Button runat="server" ID="btn_delete" Text="Delete" CssClass="btnCls"
                OnClick="btn_delete_Click" Visible="False" />
        </center>
        <br />

    </div>
</asp:Content>

