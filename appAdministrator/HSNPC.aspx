<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="HSNPC.aspx.cs" Inherits="appAdministrator_HSNPC" Title=" Update HS / NPC Code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="../JScript/jquery-1.7.1.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">


function Validation()
  {
     var Text_search=$("#<%=txt_product.ClientID %>");
        if(Text_search.val()=="")
        {
           alert("Please enter product name");
             return false;
        }
        else
        {
          var K=  $("#<%=rdl_button.ClientID %> input[type=radio]:checked").val();
            if(K=="1")
            {
               $("#<%=hs_code_npc.ClientID %>").text("HS CODE");
            }
            else
            {
             $("#<%=hs_code_npc.ClientID %>").text("NPC CODE");
            }
        }
  }
  function ShowPopup() {
            $('#mask').show();
            $('#<%=pnlpopup.ClientID %>').show();
        }
        function HidePopup() {
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
            
        }
        $(".btnClose").live('click',function () {
            HidePopup();
        });
        
        
     
        
        function showid(id)
        {
         
           alert(id);
      
       
       var id_value=$("#"+id.id).attr("myCustomID");;
       alert(id_value);
        
       
       
          
           return false;
          
        }
        
        
        function SubmitValidation()
        {
          var Text_search=$("#<%=txt_product.ClientID %>");
        if(Text_search.val()=="")
        {
           alert("Please enter product name");
             return false;
        }
        
             if($("#ctl00_ContentPlaceHolder1_lbl_search_record_text").val()=="")
             {
                        var values = '';
                        
                        var list=document.getElementById("<%=lst_box_HSCode.ClientID %>");
                          var R=list.options.length;
                           var valuetext="";
                          var K=  $("#<%=rdl_button.ClientID %> input[type=radio]:checked").val();
			for(var i = 0; i < document.getElementById("<%=lst_box_HSCode.ClientID %>").options.length;i++) 
			{
				if(document.getElementById("<%=lst_box_HSCode.ClientID %>")[i].selected == true) {
				values +=  document.getElementById("<%=lst_box_HSCode.ClientID %>")[i].value+",";
					valuetext=document.getElementById("<%=lst_box_HSCode.ClientID %>")[i].text;
				}
				
		
			}
		
			   
			if(values != '') 
			{
			      
			      	if(K=="1")
			{
			  var str=valuetext.split(" ");
			    
			     if(str[0].length >4)
			     {
			        alert("Please select only Four Digit HS Code ");
			         return false;
			     }
			   
			}
			      
			       var gridView = document.getElementById("<%=grd_view.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) 
            {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) 
                {
                    return true;
                }
               
            }
            
            alert("Please Choose at least company name");
             return false;
          
			
			}
			else 
			{
			alert("Please  select  Hs/Npc code");
			return false;
			}
             }
        }
        
        
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grd_view.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
              
            }

        }
        function deCheckAllEmp() {
            var GridVwHeaderChckbox = document.getElementById("<%=grd_view.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked =false;
                
            }

        }
    

</script>

<style type="text/css">
  #mask
        {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 4;
            opacity: 0.4;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=40)"; /* first!*/
            filter: alpha(opacity=40); /* second!*/
            background-color: gray;
            display: none;
            width: 100%;
            height: 100%;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <table cellpadding="2"  cellspacing="2" border="0" width="100%">
   <tr>
       <td colspan="2" style="width:100%;" align="left" class="header">
          Update HS / NPC Code
          
       
       </td>
   </tr>
 <%--  <tr>
           <td colspan="2" align="left" valign="top" style="width:100%;">
           <asp:radiobuttonlist id="rdl_button" runat="server"  RepeatDirection="Horizontal">
           <asp:ListItem Text="HsCode" Value="1" Selected="True"></asp:ListItem>
           <asp:ListItem Text="NpcCode" Value="2" ></asp:ListItem>
           </asp:radiobuttonlist>
           
           </td>
   
   </tr>--%>
   
   <tr>
   <td align="left" style="width:100%;">
     <table cellpadding="2" cellspacing="2" border="0" width="80%">
     <tr>
     <td align="left" valign="top" > 
                    
     
           <asp:radiobuttonlist id="rdl_button" runat="server"  RepeatDirection="Vertical">
           <asp:ListItem Text="HS Code" Value="1" Selected="True"></asp:ListItem>
           <asp:ListItem Text="NPC Code" Value="2" ></asp:ListItem>
           </asp:radiobuttonlist>      
            </td>
           <td align="left" valign="middle" ><b>  Enter Product Name: </b></td>
     <td align="left" >
     <%--<asp:UpdatePanel ID="updte_text" runat="server">
     <ContentTemplate>--%>
    
     <asp:TextBox ID="txt_product" runat="server"  Width="220"></asp:TextBox>
     <%-- </ContentTemplate>
     </asp:UpdatePanel>--%>
     </td>
     <td align="left">
     <asp:UpdatePanel ID="Update_bton" runat="server">
                  <ContentTemplate>
     <asp:Button ID="btn_search" runat="server" Text="Search" 
             CssClass="btnCls" OnClientClick="javascript:return Validation();" 
             onclick="btn_search_Click" />
               </ContentTemplate>
                  </asp:UpdatePanel>
             </td>
     </tr>
     <tr>
       <td colspan="3" align="right" style="padding-right:20px;">
              <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="Update_bton"
        runat="server">
            <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
        </asp:UpdateProgress>
       </td>
     </tr>
     </table>
   </td>
   
   </tr>
   <tr>
     <td colspan="2" style="width:100%; padding-left:100px;" align="left">
    <%-- <div id="mask">
    </div>--%>
      <asp:Panel ID="pnlpopup" runat="server"  BackColor="White"
          Height="150px"   Width="200px" 
          Visible="false">
           
           
        <table  cellpadding="2" cellspacing="2" border="0" width="100%" style="outline:black 1px solid;">
         <tr style="background-color: #336596;">
                    <td colspan="2" style="color:White; font-weight: bold; font-size: 1.2em; padding:3px"
                        align="center">
             <b><asp:Label ID="hs_code_npc"  runat="server" Text=""></asp:Label> </b> <a id="closebtn" style="color: white; float: right;text-decoration:none" class="btnClose"  href="#"></a>
                     
                 
                    </td>
                </tr>
                
                
                   <tr>
           <td colspan="2" align="left" valign="top" style="width:100%;">
          
         
           
           </td>
   
                 </tr>
                 
                 <tr>
                 <td colspan="2" align="left" valign="top" style="width:100%">
                 <table cellpadding="2" cellspacing="2" border="0" width="100%">
                 <tr>
                 <td align="left" style="width:100%">
                 <asp:ListBox ID="lst_box_HSCode" runat="server" Height="100"   Width="500" SelectionMode="Single" AutoPostBack="true"></asp:ListBox>
                 <asp:Label ID="lbl_search_record_text" runat="server" Text=""></asp:Label>
                 </td>
                 </tr>
                 
               
                 </table>
                 </td>
                 </tr>
                
                </table>
                </asp:Panel>
   </td>
   
   </tr>
   
   <tr>
      <td colspan="2" style="width:100%">
      
     <%-- <asp:UpdatePanel ID="update_grd" runat="server" >
         <ContentTemplate>--%>
        
          <asp:gridview ID="grd_view" runat="server"  CellPadding="3" CellSpacing="3" 
              AutoGenerateColumns="false" onrowcommand="grd_view_RowCommand">
             <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
          <EmptyDataTemplate>
                    No record found
                </EmptyDataTemplate>
                
                <Columns>
                      <asp:TemplateField ItemStyle-Width="25px" HeaderStyle-Width="25px">
                        <HeaderTemplate>
                          &nbsp; &nbsp;   &nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                        <input type="checkbox" ID="chkEmp" value= '<%#Eval("CompanyId") %>' name="conchk" />
                        
                        </ItemTemplate>
                    </asp:TemplateField>
               
                
                <asp:TemplateField HeaderText="Company Name">
                   <ItemTemplate>
                      <asp:Label ID="lbl_Comapny_name" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                   </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Address">
               <ItemTemplate>
                  <asp:Label ID="lbl_address" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:Label>
               </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="HSCODE/NPC Code">
                   <ItemTemplate>
                      <asp:Label ID="lbl_hs_code" runat="server" Text='<%#Eval("HSCode") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Product">
                          <ItemTemplate>
                          <asp:Label ID="lbl_product" runat="server" Text='<%#Eval("Product_Description") %>'></asp:Label>
                          </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="" Visible="false">
               <ItemTemplate>
<%--                    <asp:Button ID="btnAccept" runat="server" CssClass="btnCls" CausesValidation="false"
                            Text="Fill code" Width="60px" CommandName="action" CommandArgument='<%#Eval("CompanyId") %>'   />--%>
                    
                    <asp:LinkButton ID="link_button" runat="server"   Text="Fill code" myCustomID='<%#Eval("CompanyId") %>'   OnClientClick ="javascript:return showid(this);"  >  </asp:LinkButton>  <%----%>
                    
                   
                  </ItemTemplate>
                
                </asp:TemplateField>
          
                
                </Columns>
          </asp:gridview>
      <%-- </ContentTemplate>
      </asp:UpdatePanel>--%>
      </td>
   
   </tr>
   
   <tr>
                 <td colspan="2" align="center" >
                  <asp:UpdatePanel ID="Update_bton_submit" runat="server">
                  <ContentTemplate>
                 <asp:Button ID="btn_submit" runat="server" Text="Submit"  Visible="false" 
                         CssClass="btnCls"  OnClientClick="javascript:return SubmitValidation();" 
                         onclick="btn_submit_Click"/>
                         <asp:Label ID="lbl_message" runat="server" Text=""></asp:Label>
                         </ContentTemplate>
                  </asp:UpdatePanel>
                         
                 </td>
   </tr>
   <tr>
   <td colspan="2" align="center">
         <asp:UpdateProgress ID="UpdateProgress1"
        AssociatedUpdatePanelID="Update_bton_submit"
        runat="server">
            <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
        </asp:UpdateProgress>
   
   </td>
   </tr>
   
   </table>
</asp:Content>

