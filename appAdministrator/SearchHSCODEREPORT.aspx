<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="SearchHSCODEREPORT.aspx.cs" Inherits="appAdministrator_SearchHSCODEREPORT" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

  <%--  <script src="../JScript/jquery-1.7.1.js" type="text/javascript"></script>--%>
    <script src="../JScript/jquery.freezeheader.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript" >


   function CheckSingleCheckbox(obj) {
        var id = obj.id;
        var res = id.split(",");

        var clickedradiooptions = $('#<%=hdn_holdtxtval.ClientID %>').val();
         if (clickedradiooptions != "") {
            document.getElementById(clickedradiooptions).value = res[1];
        }
        //  alert(res[1]);
           document.getElementById("<%=txt_hs_code.ClientID %>").value = res[1];
             HidePopup();
            }

 function Bind_Searchdata() {

       

        var clickedradiooptions = $('#<%=hdn_holdtxtval.ClientID %>').val();
 
        
        
        var txtsearch = $('#<%=txt_search.ClientID %>').val();
        if (txtsearch == "") {
            alert('Please enter text to serach HS/NPC Code');
            return false;
        }
        
         $("#gv_result").append(" <tr><td align ='center' valign ='middle'><img src ='../Images/ajax-loader.gif' /></td></tr>");
             var  chkenterprise = $('#<%=rdl_hs_npc.ClientID %> input:checked ').val();
             var hcode;
           if (chkenterprise == "1") {
            hcode = "HS Code";
        }
        else {
            hcode = "NPC Code";
        }
        
          $.ajax({
            type: "POST",
            url: "SearchHSCODEREPORT.aspx/Bind_Searchgrid",
            data: '{searchitem:"' + txtsearch + '",codetype:"' + chkenterprise + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(response) {
                $("#heading").empty();
                $("#gv_result").empty();

              
                $("#heading").append("<tr class ='dynRowHeader'><td  >Options</td><td  >" + hcode + "</td><td >Product Details</td></tr>");
               
                var sb = "<tr><td colsapn='3' ><div style='overflow: scroll; '><table style ='height: 100%; width: 100%;'>";

                for (var i = 0; i < response.d.length; i++) {

                    if (i % 2 == 0) {
                   
                        $("#gv_result").append("<tr class ='dynRowEven'><td style ='width:80px;' >  <input type ='checkbox' id ='" + response.d[i].HSID + "," + response.d[i].HSCode + " ' onclick = 'javascript:CheckSingleCheckbox(this)' /> </td><td stlye ='width:80px;' >" + response.d[i].HSCode + "</td><td >" + response.d[i].HSCodedescription + "</td></tr>");
                    }
                    else {
                     
                        $("#gv_result").append("<tr class ='dynRowOdd' ><td stlye ='width:80px;' ><input type ='checkbox' id ='" + response.d[i].HSID + "," + response.d[i].HSCode + " ' onclick = 'javascript:CheckSingleCheckbox(this)' /> </td><td stlye ='width:80px;' >" + response.d[i].HSCode + "</td><td>" + response.d[i].HSCodedescription + "</td></tr>");
                    }


                }
                sb = sb.concat("</table></div></td></tr>");
             $("#gv_result").freezeHeader({ 'height': '350px' });
              

                $('#mask').show();
                $('#<%=pnlpopup.ClientID %>').show();
                return false;
            },
            error: function(response) {

                alert(response.status + ' ' + response.statusText);
            }
        });
        
        }
    

     function ShowPopupUpper(obj) {
    

        var chkonblur = $('#<%=hdn_holdtxtval.ClientID %>').val();
    
      var  chkenterprise = $('#<%=rdl_hs_npc.ClientID %> input:checked ').val();

        if (chkenterprise == "1") 
        {
            $('#<%=lbl_head.ClientID%>').html("Seach HS CODE FOR PARTICULAR COMPANY");
        }
        else if (chkenterprise == "2") {
            $('#<%=lbl_head.ClientID%>').html("Seach NPC CODE FOR PARTICULAR COMPANY");
        }
        else 
        {
           

           


        }

        $('#mask').show();
        $("#heading").empty();
        $("#gv_result").empty();
        $('#<%=pnlpopup.ClientID %>').show();
    }

    function HidePopup() {

        $('#mask').hide();
        $('#<%=pnlpopup.ClientID %>').hide();
        $('#<%=hdn_holdtxtval.ClientID %>').val("");
        $('#<%=txt_search.ClientID %>').val("");

    }
    $(".btnClose").live('click', function() {
        HidePopup();


    });

   
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
        
           table.fixed {table-layout:fixed; }/*Setting the table width is important!*/
         table.fixed td {overflow:hidden;}/*Hide text outside the cell.*/
         table.fixed td:nth-of-type(1) {width:80px;}/*Setting the width of column 1.*/
         table.fixed td:nth-of-type(2) {width:100px;}/*Setting the width of column 2.*/
         
         
           table.hfixed {table-layout:fixed; }/*Setting the table width is important!*/
         table.hfixed td {overflow:hidden;}/*Hide text outside the cell.*/
         table.hfixed td:nth-of-type(1) {width:100px;}/*Setting the width of column 1.*/
         table.hfixed td:nth-of-type(2) {width:100px;}/*Setting the width of column 2.*/
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:HiddenField runat ="server" ID ="hdn_enterprise" />
<asp:HiddenField ID ="hdn_holdtxtval" runat ="server" />
<table cellpadding="2"  cellspacing="2" border="0" width="100%">
   <tr>
       <td colspan="2" style="width:100%;" align="left" class="header">
           HS / NPC Code
          
       
       </td>
   </tr>
   <tr>
     <td colspan="2" style="width:100%;" align="left" >
          <!--  Table  For  select Criteria----> 
         <table cellpadding="2" cellspacing="2"  border="0" style="outline:1px solid Black;">
           <tr>
              <td  colspan="2" style="width:100%;" align="left" >
                <b> Select Criteria:</b>
              </td>
           </tr>
           <tr>
             <td  colspan="2" style="width:100%;" align="left" >
             <hr />
             </td>
           </tr>
           
           <tr>
               <td align="left" valign="top" >
                 <b>Search HS/ NPC Code</b>
                 <br />
                   <asp:RadioButtonList ID="rdl_hs_npc" runat="server" >
                     <asp:ListItem  Text="HS CODE" Value="1" Selected="True"></asp:ListItem>
                     <asp:ListItem Text="NPC CODE" Value="2"></asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               <td align="left" valign="top" >
                 <asp:TextBox ID="txt_hs_code" runat="server" Text="" Width="220" CssClass="txt_bx"  onclick ="javascript:ShowPopupUpper(this);"></asp:TextBox>
               </td>
           </tr>
           <tr>
             <td align="left" valign="top">
               <b> StatusWise:</b>
             </td>
             <td align="left" valign="top" >
               <asp:DropDownList ID="ddl_status" runat="server" Width="220" CssClass="clsDDL">
                      <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="LIVE" Value="1" ></asp:ListItem>
                        <asp:ListItem Text="EXPIRED" Value="2" ></asp:ListItem>
               </asp:DropDownList>
             </td>
           </tr>
           
           <tr>
                <td align="left" valign="top">
                  <b>Registration Date wise:</b>
                </td>
                <td align="left" valign="top">
                       <table cellpadding="2" cellspacing="2" border="0">
                          <tr>
                             <td align="left" valign="top" >From Date:</td>
                             <td align="left" valign="top"><asp:TextBox ID="txt_from_date" runat="server" Text="" Width="80" CssClass="txt_bx"></asp:TextBox></td>
                             <td align="left" valign="top">To Date:</td>
                             <td align="left" valign="top"><asp:TextBox ID="txt_to_date" runat="server" Text="" Width="80" CssClass="txt_bx"></asp:TextBox></td>
                          </tr>
                       </table>   
                </td>
           </tr>
           
           
           <tr>
              <td align="left" valign="top">
                  <b> ZoneWise:</b>
              </td>
              <td align="left" valign="top" >
                <asp:DropDownList ID="ddl_zone" runat="server"  CssClass="clsDDL" Width="210"></asp:DropDownList>
              </td>
           </tr>
           <tr>
              <td align="left" valign="top">
                 <b>BranchWise:</b>
              </td>
              <td align="left" valign="top">
                <asp:DropDownList ID="ddl_branch_wise" runat="server" CssClass="clsDDL" Width="210"></asp:DropDownList>
              </td>
           </tr>
           
           <tr>
              <td align="left" valign="top">
                 <b>StateWise:</b>
              </td>
              <td align="left" valign="top">
              <asp:DropDownList ID="ddl_state_wise" runat="server" CssClass="clsDDL" Width="210"></asp:DropDownList>
              </td>
           </tr>
           
           <tr>
              <td align="left" valign="top">
                 <b> CityWise:</b>
              </td>
              <td align="left" valign="top">
                <asp:DropDownList ID="ddl_city" runat="server" Width="210" CssClass="clsDDL"></asp:DropDownList>
              </td>
           </tr>
           <tr>
              <td align="left" valign="top">
                <b>Category Area:</b>
              </td>
              <td align="left" valign="top">
                 <asp:DropDownList ID="ddl_category" runat="server" Width="210" CssClass="clsDDL"></asp:DropDownList>
              </td>
           </tr>
           <tr>
             <td align="left" valign="top">
                <b>Category Cast:</b>
             </td>
             
             <td align="left" valign="top">
               <asp:DropDownList ID="ddl_cate_cast" runat="server" Width="210" CssClass="clsDDL"></asp:DropDownList>
             </td>
           </tr>
           <tr>
             <td align="left" valign="top">
               <b> Order By :</b>
             </td>
             
             <td align="left" valign="top">
                <asp:DropDownList ID="ddl_order_by" runat="server" Width="210" CssClass="clsDDL"></asp:DropDownList>
             </td>
           </tr>
         </table>
         
         <!---End Table for select Criteria----> 
     </td>
   </tr>
   
   <tr>
      <td colspan="2" align="left" valign="top">
      <asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="btnCls" />
      </td>
   
   </tr>
   
   <tr>
     <td colspan="2"  >
         <div id="mask">
    </div>
      <asp:Panel ID="pnlpopup"  runat="server"  BackColor="White" Height="70%"
            Width="50%" Style="z-index:111;background-color: White; position: fixed; left: 25%;  top: 20%; border: outset 2px gray;padding:5px;display:none">
            <table width="100%" cellpadding="0" style="border:1px solid black;" cellspacing="0">
         
          
                <tr  style=" background-color:#336596;">
                    <td colspan="2" style="color:white; font-weight: bold; font-size: 1.2em; padding:3px"
                        align="center">
                        <asp:Label  runat ="server" ID ="lbl_head"></asp:Label>
                       <a id="closebtn" style="color: white; float: right;text-decoration:none" class="btnClose"  href="#">X</a>
                    </td>
                </tr>
                   <tr>
              <td colspan ="2">
                 &nbsp;&nbsp;&nbsp;
              </td>
              </tr>
                
                <tr>
                    <td align="right" >
                       <asp:TextBox runat ="server" ID ="txt_search" Width ="350px" 
                   > </asp:TextBox>
                    </td>
                    <td align ="center" >
                      <asp:Button ID="Button1" CssClass ="btnCls"  OnClientClick="if(!Bind_Searchdata()) { return false;}"   runat="server" Text="Search"  />
                    </td>
                </tr>
                
             
                <tr>
                <td colspan ="2"> 
                &nbsp;&nbsp;&nbsp;
                
                </td>
                </tr>
                <tr>
                  <td colspan ="2">
                  
                  <table id ="heading" class="fixed" cellPadding="3" CellSpacing="1"  Width="100%" >
                   
                  </table>
                  <table id ="gv_result"  class="hfixed" cellPadding="3" CellSpacing="1"  Width="100%"  >
                   </table>
                  </td>
                </tr>
            </table>
        </asp:Panel>
          
          
     
     </td>
   </tr>
   
  </table>
</asp:Content>

