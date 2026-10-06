<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="DeleteCompanyByBranch.aspx.cs" Inherits="appAdministrator_DeleteCompanyByBranch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
  
/* The Modal (background) */
.modal {
    display: none; /* Hidden by default */
    position: absolute; /* Stay in place */
    z-index: 1; /* Sit on top */
    padding-top: 20%; /* Location of the box */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgb(0,0,0); /* Fallback color */
   /* background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

/* Modal Content */
.modal-content {
    background-color: #fefefe;
    margin: auto;
    padding: 20px;
    border: 1px solid #888;
    width: 80%;
}

/* The Close Button */
.close {
    color: #aaaaaa;
    float: right;
    font-size: 28px;
    font-weight: bold;
}

.close:hover,
.close:focus {
    color: #000;
    text-decoration: none;
    cursor: pointer;
}

  
  </style>
  
  <script type="text/javascript" language="javascript">

      function GetSelectedRow(lnk) {
          var row = lnk.parentNode.parentNode;
          var rowIndex = row.rowIndex - 1;
          var customerId = row.cells[0].innerHTML;
          var city = row.cells[1].getElementsByTagName("input")[0].value;
          alert("RowIndex: " + rowIndex + " CustomerId: " + customerId + " City:" + city);
          return false;
      }
      function DeleteText(id) {
          Reset();
          var btn_id = id.id;
          var modal = document.getElementById('myModal');
          var btn = document.getElementById(btn_id);
          var span = document.getElementById('close_mod');
          //var span = document.getElementsByClassName("close")[0];
          //document.getElementById('text1').value = id.id;
          // When the user clicks the button, open the modal
          $("#tex1").val(id.id);
        //  btn.onclick = function() {
          
              modal.style.display = "block";
          //}

          // When the user clicks on <span> (x), close the modal
          span.onclick = function() {
              modal.style.display = "none";
          }

          return false;

      }

      function Reset() {
          $("#ctl00_ContentPlaceHolder1_ddl_delete_reson").val("0");
          $("#ctl00_ContentPlaceHolder1_txt_reson_delete").val("");
      }

      function validation_submit() {

          var dropdown = $("#ctl00_ContentPlaceHolder1_ddl_delete_reson").val();
          if ($("#ctl00_ContentPlaceHolder1_ddl_delete_reson").val() == "0") {
              alert("Please select Reson.");
              return false;
          }
      }

      function onchangedropdown() {
          var dropdown = $("#ctl00_ContentPlaceHolder1_ddl_delete_reson").val();
          if ($("#ctl00_ContentPlaceHolder1_ddl_delete_reson").val() == "Other") {
              $("#ctl00_ContentPlaceHolder1_txt_reson_delete").css("display","block");
          
          }
          else
          {
             $("#ctl00_ContentPlaceHolder1_txt_reson_delete").css("display","none");
          }
      }
  </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField runat ="server" ID ="hid_comapnyId" />
    <input type="text" id="tex1" name="text1" style="display:none;"/> 
   <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll">Delete Companies</asp:Label>
        </div>
        
        <div align="left" style="height:10px;">
            
        </div>
        
        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
           <div style="height:10px;">
        
        </div>
        
          <div align="left" class="clsrowdiv">
            <div class="leftext">Search</div>
            <div class="coloncls"><asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL">
                <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
                <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
                <asp:ListItem Text="Reference No." Value="3"></asp:ListItem>
                <asp:ListItem Text="Registration No." Value="4"></asp:ListItem>
            </asp:DropDownList></div>
            <div class="txtdiv"><asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox></div>
            <div class="btndiv">
                <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CssClass="btnCls" onclick="btnSearch_Click" />
                 
                    </div>
                    
                    
        </div> <br />
         <div class="txtlbl">
         <b><span style="color:Red;"> <asp:Label ID="lbl_count_text" runat="server" Text=""></asp:Label></span></b>
         </div>
         
          <div align="left" style="padding-left: 10px; padding-right: 10px;">
             <asp:GridView ID="gv_company" runat="server" AutoGenerateColumns="False"
            Width="100%"
               AllowPaging="True"
                AllowSorting="True"  DataKeyNames ="fGPtbl_Company_Id"
                onpageindexchanging="gv_company_PageIndexChanging" 
               >      <%--onrowcommand="gv_company_RowCommand"--%>
                    <EmptyDataTemplate>
                                                <div align="center"><strong style="font-size:18px; font-weight:bold; color:Red;"> Record not found!!!</strong></div>
                           </EmptyDataTemplate>
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" CssClass="Pagging" />
           <Columns>
           
            <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
                <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                <asp:BoundField DataField="fGPtbl_Company_GPReferenceNo" 
                    HeaderText="Reference No" />
                <asp:BoundField DataField="regis" HeaderText="Registration No." />
                <asp:BoundField DataField="status" HeaderText="Status" />
                
                 <asp:TemplateField HeaderText="Options" 
                    HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      
                           <a href="#" id="<%#Eval("fGPtbl_Company_Id") %>" onclick="javascript:return DeleteText(this)"> Delete</a>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                
</asp:TemplateField>
               
               </Columns>
                
                
                </asp:GridView>
             
          
          </div>
          
          
          
     <div id="myModal" class="modal">   
     <div class="modal-content">
    <span id="close_mod" class="close">×</span>  
   
    <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#DDF1F7;">
               <tr>
                  <td align="left">
                      <asp:HiddenField ID="lbl_company_id_onpnl" runat="server" />
                                   Reason for Deletion:
                    </td>
                    
                      <td>
                      <select ID="ddl_delete_reson" runat="server" CssClass="clsDDL"  onchange="onchangedropdown()">
                       <option value="0" selected="selected"> Select</option>
                       <option value="Inspection Failed"> Inspection Failed</option>
                       <option value="Closed"> Closed</option>
                       <option value="Pending Documents"> Pending Documents</option>
                       <option value="Unit Expired"> Unit Expired</option>
                      <option value="Typo Error"> Typo Error</option>
                      <option value="Double Entry"> Double Entry</option>
                      <option value="Inspection Pending"> Inspection Pending</option>
                      <option value="Case Rejected"> Case Rejected</option>
                      <option value="Other">Other</option>
                      </select>
                           <asp:TextBox ID="txt_reson_delete" runat="server" TextMode="MultiLine"  style="display:none;"></asp:TextBox>
                                         
                        </td>
               </tr>
               
                 <tr>
                   <td colspan="2"   align="center">
                      <asp:Button ID="btnSubmitpan" runat="server" Text="Submit" TabIndex="3" 
                                               OnClientClick="javascript:return validation_submit();" CssClass="btnCls" onclick="btnSubmitpan_Click"
                                             />
                    </td>
                 </tr>
    </table>
    
    

     </div>
 
    </div>
 
          
          </div>
</asp:Content>

