<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="CompanyshiftBranch.aspx.cs" Inherits="appAdministrator_CompanyshiftBranch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function validate_dropdown() {

            var parent = document.getElementById("<%=ddl_branch_parent.ClientID %>");
           var parent_val = parent.value;
           var Child = document.getElementById("<%=ddl_branch_child.ClientID %>");
           var Child_val = Child.value;
           if (parent_val == "" || Child_val == "") {
               alert("please select branch");
               return false;
           }/*ssss*/
           else {
               var gridView = document.getElementById("<%=grd_bind_company.ClientID %>");
                var checkBoxes = gridView.getElementsByTagName("input");
                for (var i = 0; i < checkBoxes.length; i++) {
                    if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                        return true;
                    }



                }

                alert('please select atleast one company');
                return false;

            }
            //   validategrid();

        }

        function validategrid() {
            var gridView = document.getElementById("<%=grd_bind_company.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                }



            }

            alert('please select atleast one company');
            return false;
        }
        function GridSelectAllColumn(spanChk) {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0]; xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++) {
                if (elm[i].type === 'checkbox' && elm[i].checked != xState)
                    elm[i].click();

            }


        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <table cellpadding="2" cellspacing="2" border="0" width="100%">
        <tr>
            <td align="left" valign="top" colspan="3">
                <b>Company Shift To branch </b>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 14px;"></td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <b>From </b>:    
                <asp:DropDownList ID="ddl_branch_parent" runat="server" CssClass="clsDDL" AutoPostBack="true"
                    OnSelectedIndexChanged="ddl_branch_parent_SelectedIndexChanged">
                </asp:DropDownList>

            </td>

            <td align="left" valign="top">
                <b>To</b>:
                <asp:DropDownList ID="ddl_branch_child" runat="server" CssClass="clsDDL"></asp:DropDownList>
            </td>
            <td align="left" valign="top">
                <asp:UpdatePanel ID="Update_bton" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btn_tranfer" runat="server" Text="Change" CssClass="btnCls"
                            OnClientClick="javascript:return validate_dropdown() ;" OnClick="btn_tranfer_Click" />
                    </ContentTemplate>

                </asp:UpdatePanel>


            </td>
        </tr>

        <tr>
            <td colspan="3" align="right">
                <asp:UpdateProgress ID="updProgress"
                    AssociatedUpdatePanelID="Update_bton"
                    runat="server">
                    <ProgressTemplate>
                        <img alt="progress" src="../Images/Admin/loading.gif" />
                        Processing...            
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="left" style="padding-left: 4px;">
                <asp:GridView ID="grd_bind_company" runat="server" AllowPaging="false" AllowSorting="false"
                    AutoGenerateColumns="false">

                    <HeaderStyle CssClass="dynRowHeader" />
                    <RowStyle CssClass="dynRowEven" />
                    <AlternatingRowStyle CssClass="dynRowOdd" />

                    <Columns>

                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>

                                <%-- <asp:CheckBox runat="server" ID="chk" Value='<%#Eval("ComapnyID") %>'  name="Cochk"/>--%>
                                <input type="checkbox" id="chk" value='<%#Eval("ComapnyID")+ "/"+ Eval("CompanyStatus") %>' name="conchk" />
                            </ItemTemplate>
                            <%-- <asp:Label ID="hiden_com_id" runat="server" Value='<%#Eval("ComapnyID") %>' />--%>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="GridSelectAllColumn(this);" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Name">
                            <ItemTemplate>
                                <asp:Label ID="lbl_company_name" runat="server" Text='<%#Eval("ComapnyName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PAN">
                            <ItemTemplate>
                                <asp:Label ID="lbl_PAN" runat="server" Text='<%#Eval("PAN") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                    <EmptyDataTemplate>
                        No data found!
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>



    </table>
</asp:Content>

