<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="BacklogUnitStep3_Store.aspx.cs"
Inherits="appAdministrator_BacklogUnitStep3_Store" Title="store page" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script src="../JScript/jquery.freezeheader.js" type="text/javascript"></script>
   <%--<script src="https://code.jquery.com/jquery-2.1.1.min.js"></script>  --%>
   
  
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>  
 <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"  
        rel="stylesheet" type="text/css" />  
        
        <style type="text/css">
    .water
    {
         font-family: Tahoma, Arial, sans-serif;
         color:gray;
    }
    .imgdisplay
    {
    	display:none;
    	width:121px;
    	}
    	
    
    	
    </style>
    
    	<style>
         table.fixed {table-layout:fixed; }/*Setting the table width is important!*/
         table.fixed td {overflow:hidden;}/*Hide text outside the cell.*/
         table.fixed td:nth-of-type(1) {width:100px;}/*Setting the width of column 1.*/
         table.fixed td:nth-of-type(2) {width:100px;}/*Setting the width of column 2.*/
         
         
           table.hfixed {table-layout:fixed; }/*Setting the table width is important!*/
         table.hfixed td {overflow:hidden;}/*Hide text outside the cell.*/
         table.hfixed td:nth-of-type(1) {width:100px;}/*Setting the width of column 1.*/
         table.hfixed td:nth-of-type(2) {width:100px;}/*Setting the width of column 2.*/
      </style>
    
<%--     <script src="Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>--%>

 

    <script type="text/javascript" language="javascript">

        var storearray = new Array();
      
        function setsearchpostion(obj) {
            $('#<%=hdn_holdtxtval.ClientID %>').val(obj.id)
        
        }
        
       
        
        function validateupper() {
            var chkvalid = true;
            var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
            var hscode1, hscode2, hscode3, hscode4;
            hscode1 = $('#<%=txt_hscode1.ClientID %>').val().trim();
            hscode2 = $('#<%=txt_hscode2.ClientID %>').val().trim();
            hscode3 = $('#<%=txt_hscode3.ClientID %>').val().trim();
            hscode4 = $('#<%=txt_hscode4.ClientID %>').val().trim();
            //

            if (hscode1 == "" && hscode2 == "" && hscode3 == "" && hscode4 == "") {
                $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
                $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
               // alert('Please enter atleast one HS /NPC Code ');
               // chkvalid = false;
            }
            else {
                $('#<%=txt_hscode1.ClientID %>').removeClass("txt_invalidhscode");
                $('#<%=txt_hscode2.ClientID %>').removeClass("txt_invalidhscode");
                $('#<%=txt_hscode3.ClientID %>').removeClass("txt_invalidhscode");
                $('#<%=txt_hscode4.ClientID %>').removeClass("txt_invalidhscode");
               // alert('Please enter atleast one HS /NPC Code ');
             //   chkvalid = true;
                if (hscode1 != "" && hscode2 != "") {
                    if (hscode1 == hscode2) {
                        $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                        $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                        chkvalid = false;
                        //alert('duplicate hscode1 & hscode2');
                    }
                }
                if (hscode1 != "" && hscode3 != "") {
                    if (hscode1 == hscode3) {
                        $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                        $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
                        chkvalid = false;
                        //alert('duplicate hscode1 & hscode3');
                    }
                }
                if (hscode1 != "" && hscode4 != "") {
                    if (hscode1 == hscode4) {
                        $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                        $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                        chkvalid = false;
                        //alert('duplicate hscode1 & hscode4');
                    }
                }

                if (hscode2 != "" && hscode3 != "") {
                    if (hscode2 == hscode3) {
                        $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                        $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
                        chkvalid = false;
                        //  alert('duplicate hscode2 & hscode3');
                    }
                }

                if (hscode2 != "" && hscode4 != "") {
                    if (hscode2 == hscode4) {
                        $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                        $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                        chkvalid = false;
                        //   alert('duplicate hscode2 & hscode4');
                    }
                }

                if (hscode3 != "" && hscode4 != "") {
                    if (hscode3 == hscode4) {
                        $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
                        $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                        chkvalid = false;
                        //  alert('duplicate hscode3 & hscode4');
                    }
                }
             
             
            }
            if (chkenterprise == "1") 
            {
                if (hscode1 != "") 
                {
//                    if (parseInt(hscode1.length) != 4) {
//                        $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
//                    //    alert('Invalid Hscode 1');
//                        chkvalid = false;
                    //                    }

                 if (parseInt(hscode1.length) > 3 && parseInt(hscode1.length) < 9) 
                 {
                   if (parseInt(hscode1.length) == 4 || parseInt(hscode1.length) == 6 || parseInt(hscode1.length) == 8) 
                   {
                       $('#<%=txt_hscode1.ClientID %>').addClass("txt_bx150");
                        valid = true;
                  }
                    else 
                    {
                         $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                        valid = false;
                    }
                  }
                    else
                    {
                         $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                        valid = false;
                    }
              //  }

                }
                if (hscode2 != "") 
                {
//                    if (parseInt(hscode2.length) != 4) {
//                        $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
//                      //  alert('Invalid Hscode 2');
//                        chkvalid = false;
//                    }

                     if (parseInt(hscode2.length) > 3 && parseInt(hscode2.length) < 9) 
                     {
                        if (parseInt(hscode2.length) == 4 || parseInt(hscode2.length) == 6 || parseInt(hscode2.length) == 8) {
                       
                             $('#<%=txt_hscode2.ClientID %>').addClass("txt_bx150");
                            valid = true;
                        }
                        else {
                             $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                            valid = false;
                        }

                        }
               
                else
                {
                     $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                    valid = false;
                }
                // }
                }

                if (hscode3 != "") {
//                    if (parseInt(hscode3.length) != 4) {
//                        $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
//                       // alert('Invalid Hscode 3');
//                        chkvalid = false;
//                    }


                     if (parseInt(hscode3.length) > 3 && parseInt(hscode3.length) < 9) {
                if (parseInt(hscode3.length) == 4 || parseInt(hscode3.length) == 6 || parseInt(hscode3.length) == 8) {
               
                     $('#<%=txt_hscode3.ClientID %>').addClass("txt_bx150");
                    valid = true;
                }
                else {
                     $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
                    valid = false;
                }
                }
               
                else
                {
                     $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
                    valid = false;
                }
              //  }
                }

                if (hscode4 != "") {
                    //                    if (parseInt(hscode4.length) != 4) {
                    //                        $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                    //                    //    alert('Invalid Hscode 4');
                    //                        chkvalid = false;
                    //                    }
                    if (parseInt(hscode4.length) > 3 && parseInt(hscode4.length) < 9)
                     {
                        if (parseInt(hscode4.length) == 4 || parseInt(hscode4.length) == 6 || parseInt(hscode4.length) == 8) 
                        {

                            $('#<%=txt_hscode4.ClientID %>').addClass("txt_bx150");
                            valid = true;
                        }
                        else 
                        {
                            $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                            valid = false;
                        }

                    }
                    
                    else
                {
                     $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                    valid = false;
                }
              //  }
                }
            }
            else if (chkenterprise == "2") 
            {

                if (hscode1 != "") {
                    if (parseInt(hscode1.length) < 4 || parseInt(hscode1.length) >= 9) {
                        $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                 //      alert('Invalid NPC Code 1');
                        chkvalid = false;
                    }
                }
                if (hscode2 != "") {
                    if (parseInt(hscode2.length) < 4 || parseInt(hscode2.length) >= 9) {
                        $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                      //  alert('Invalid NPC Code 2');
                        chkvalid = false;
                    }
                }

                if (hscode3 != "") {
                    if (parseInt(hscode3.length) < 4 || parseInt(hscode3.length) >= 9) {
                        $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
                       // alert('Invalid NPC Code 3');
                        chkvalid = false;
                    }
                }

                if (hscode4 != "") {
                    if (parseInt(hscode4.length) < 4 || parseInt(hscode4.length) >= 9) {
                        $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                     //   alert('Invalid NPC Code 4');
                        chkvalid = false;
                    }
                }

            }


            else 
            {
                var selectedoption1, selectedoption2, selectedoption3, selectedoption4;
                selectedoption1 = $('#<%=rdo_option1.ClientID %> input:checked ').val();
                selectedoption2 = $('#<%=rdo_option2.ClientID %> input:checked ').val();
                selectedoption3 = $('#<%=rdo_option3.ClientID %> input:checked ').val();
                selectedoption4 = $('#<%=rdo_option4.ClientID %> input:checked ').val();

                if(hscode1 != "") {
                    var code1len = parseInt(hscode1.length);
                    if (selectedoption1 == "2") {

                        if (code1len < 4 || code1len >= 9) {
                            $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                         //   alert('Invalid NPC Code 1');
                            chkvalid = false;
                        }
                    }
                    else {
                        if (code1len != 4) {
                            $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                           // alert('Invalid HS Code 1');
                            chkvalid = false;
                        }
                    }
                }


                if (hscode2 != "") {
                    //  alert(selectedoption2);
                    var code2len = parseInt(hscode2.length);
                    if (selectedoption2 == "2") {
                       
                        if (code2len < 4 || code2len >= 9) {
                            $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                         //   alert('Invalid NPC Code 2');
                            chkvalid = false;
                        }
                    }
                    else {
                        if (code2len != 4) {
                            $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                          //  alert('Invalid HS Code 2');
                            chkvalid = false;
                        }
                    }
                }


                if (hscode3 != "") {
                    var code3len = parseInt(hscode3.length);
                    if (selectedoption3 == "2") {
                        if (code3len < 4 || code3len >= 9) {
                            $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
                         //   alert('Invalid NPC Code 3');
                            chkvalid = false;
                        }
                    }
                    else {
                        if (code3len != 4) {
                            $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
                          //  alert('Invalid HS Code 3');
                            chkvalid = false;
                        }
                    }
                }


                if (hscode4 != "") {
                    var code4len = parseInt(hscode4.length);
                    if (selectedoption4 == "2") {
                        if (code4len <4 || code4len >= 9) {
                            $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                            //  alert('Invalid NPC Code 4');
                            chkvalid = false;
                        }
                    }
                    else {
                        if (code4len != 4) {
                            $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                            // alert('Invalid HS Code 4');
                            chkvalid = false;
                        }
                    }
                }       

            }
            
            

            return chkvalid;
        }

        function validate() {
            var valid = true;
            var uppervalid = true;

            var GVMaintainReceiptMaster = document.getElementById('thetable');
           uppervalid = validateupper();
          
            var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
            //  var rowindex = $('#<%=hdn_rowindex.ClientID %>').val();
            var rows = GVMaintainReceiptMaster.rows;
            for (i = 1, n = rows.length; i < n; ++i) {

                var sno, factoryloc, Products, BIS_Specification, Model, Rating_Capacity, Production_Capacity, no_shifts, Qualitative_Capacity, Quantitative_Capacity, HS_NPC_Code, Code_type;
                sno = rows[i].cells[0].children[0].value;
                factoryloc = rows[i].cells[1].children[0].value;
                Products = rows[i].cells[2].children[0].value;
                BIS_Specification = rows[i].cells[3].children[0].value;
                Model = rows[i].cells[4].children[0].value;
                Rating_Capacity = rows[i].cells[5].children[0].value;
                Production_Capacity = rows[i].cells[6].children[0].value;
                no_shifts = rows[i].cells[7].children[0].value;
                Qualitative_Capacity = rows[i].cells[8].children[0].value;
                Quantitative_Capacity = rows[i].cells[9].children[0].value;

                storearray[i] = { "sno": sno, "unitid": factoryloc, "Store": Products, "BIS_Specification": BIS_Specification, "Model_Brand": Model, "Rating_Capacity": Rating_Capacity, "Production_Capacity": Production_Capacity, "no_shifts": no_shifts, "Qualitative_Capacity": Qualitative_Capacity, "Quantitative_Capacity": Quantitative_Capacity }

                if (Products == "0") {
                    if (BIS_Specification != "0" || Model != "0" || Rating_Capacity != "0" || Production_Capacity != "0" || no_shifts != "0" || Qualitative_Capacity != "0" || Quantitative_Capacity != "0")
                     {
                        rows[i].cells[2].children[0].className = "txt_invalidhscode";
                        valid = false;
                    }
                    else {
                        rows[i].cells[2].children[0].className = "txt_bx150";
                        rows[i].cells[3].children[0].className = "txt_bx150";
                        rows[i].cells[4].children[0].className = "txt_bx150";
                        rows[i].cells[5].children[0].className = "txt_bx150";
                        rows[i].cells[6].children[0].className = "txt_bx150";
                        rows[i].cells[7].children[0].className = "txt_bx150";
                        rows[i].cells[8].children[0].className = "txt_bx150";
                        rows[i].cells[9].children[0].className = "txt_bx150";
                        rows[i].cells[10].children[0].className = "txt_bx150";
                       
                    }
                }

                if (Products != "0") {
                    if (BIS_Specification == "0") {
                        rows[i].cells[3].children[0].className = "txt_invalidhscode";
                        valid = false;
                    }
                    else {
                        rows[i].cells[3].children[0].className = "txt_bx150";

                    }
                    if (Model == "0") {
                        rows[i].cells[4].children[0].className = "txt_invalidhscode";
                        valid = false;
                    }
                    else {
                        rows[i].cells[4].children[0].className = "txt_bx150";

                    }
                    if (Rating_Capacity == "0") {
                        rows[i].cells[5].children[0].className = "txt_invalidhscode";
                        valid = false;
                    }
                    else {
                        rows[i].cells[5].children[0].className = "txt_bx150";
                    }

                    if (Production_Capacity == "0") {
                        rows[i].cells[6].children[0].className = "txt_invalidhscode";
                        valid = false;
                    }
                    else {
                        rows[i].cells[6].children[0].className = "txt_bx150";
                    }
                    if (no_shifts == "0") {
                        rows[i].cells[7].children[0].className = "txt_invalidhscode";
                        valid = false;
                    }
                    else {
                        rows[i].cells[6].children[0].className = "txt_bx150";
                    }

                    if (Quantitative_Capacity == "0") {
                        rows[i].cells[8].children[0].className = "txt_invalidhscode";
                        valid = false;
                    }
                    else {
                        rows[i].cells[8].children[0].className = "txt_bx150";
                    }

                    if (Quantitative_Capacity == "0") {
                        rows[i].cells[9].children[0].className = "txt_invalidhscode";
                        valid = false;
                    }
                    else {
                        rows[i].cells[9].children[0].className = "txt_bx150";
                    }
                }
                
                
            
                var id = rows[i].cells[10].children[0].value.trim();
                if (id != "") 
                {
                    var radioid = rows[i].cells[10].children[3].id;
                     var radioidval;
                     if (chkenterprise == "3") {
                         radioidval = getradioval(radioid);
                     }
                     else {
                         radioidval = chkenterprise;
                     }
                   
                        if(radioidval == "1")
                        {
                            if (parseInt(id.length) > 3 && parseInt(id.length) < 9) {
                                if (parseInt(id.length) == 4 || parseInt(id.length) == 6 || parseInt(id.length) == 8) {
                                    rows[i].cells[10].children[0].className = "txt_bx150";
                                    valid = true;
                                }
                                else {
                                    rows[i].cells[10].children[0].className = "txt_invalidhscode";
                                    valid = false;
                                }
                            }
                            else {
                                rows[i].cells[10].children[0].className = "txt_invalidhscode";
                                valid = false;
                            }
                          }
                          else
                          {
                              if (parseInt(id.length) > 3&& parseInt(id.length) < 9) {
                                  if (parseInt(id.length) == 4||parseInt(id.length) == 6 || parseInt(id.length) == 7 || parseInt(id.length) == 8) {
                                      rows[i].cells[10].children[0].className = "txt_bx150";
                                      valid = true;
                                  }
                                  else {
                                      rows[i].cells[10].children[0].className = "txt_invalidhscode";
                                      valid = false;
                                  }
                              }
                              else {
                                  rows[i].cells[10].children[0].className = "txt_invalidhscode";
                                  valid = false;
                              }
                          }
                   
                }

            }
            var numOfTrue = 0;
            for (var j = 1; j < storearray.length; j++) {
                if (storearray[j].Store == "0") {
                    numOfTrue++;
                }
               
            }
        //    alert(numOfTrue);
            if (numOfTrue >= 40  ) {
                rows[1].cells[2].children[0].className = "txt_invalidhscode";
                rows[1].cells[3].children[0].className = "txt_invalidhscode";
                rows[1].cells[4].children[0].className = "txt_invalidhscode";
                rows[1].cells[5].children[0].className = "txt_invalidhscode";
                rows[1].cells[6].children[0].className = "txt_invalidhscode";
                rows[1].cells[7].children[0].className = "txt_invalidhscode";
                rows[1].cells[8].children[0].className = "txt_invalidhscode";
                rows[1].cells[9].children[0].className = "txt_invalidhscode";
                alert('Please add atleast one store');
                valid = false;
            }
            else {
                if (valid != false) { 
                rows[1].cells[2].children[0].className = "txt_bx150";
                rows[1].cells[3].children[0].className = "txt_bx150";
                rows[1].cells[4].children[0].className = "txt_bx150";
                rows[1].cells[5].children[0].className = "txt_bx150";
                rows[1].cells[6].children[0].className = "txt_bx150";
                rows[1].cells[7].children[0].className = "txt_bx150";
                rows[1].cells[8].children[0].className = "txt_bx150";
                rows[1].cells[9].children[0].className = "txt_bx150";
                valid = true;
            }
            }
            if (valid == true && uppervalid == true) {
                valid = true;
            }
            else {
                valid = false;
                alert('There is some error on page marked as red');
            }
           
            return valid;
            
        //    var controls = $(GVMaintainReceiptMaster.rows[rowindex].cells[10].children);
//            $(GVMaintainReceiptMaster.rows[rowindex].cells[10].children[0]).val(res[1]);
//            $(GVMaintainReceiptMaster.rows[rowindex].cells[10].children[2]).val(res[0]);
        }

        function CheckSingleCheckbox(obj) {
          //  alert(obj.id);

            var id = obj.id;
            var res = id.split(",");
            var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
            var GVMaintainReceiptMaster = document.getElementById('thetable');
            var rowindex = $('#<%=hdn_rowindex.ClientID %>').val();
            if (rowindex != "") {
               // alert('rows section');
                $(GVMaintainReceiptMaster.rows[rowindex].cells[10].children[0]).val(res[1]);
                $(GVMaintainReceiptMaster.rows[rowindex].cells[10].children[2]).val(res[0]);
             
              //  alert($(GVMaintainReceiptMaster.rows[rowindex].cells[10].children[3].id));
                if (chkenterprise == "3") {
//                    var radioid = $(GVMaintainReceiptMaster.rows[rowindex].cells[10].children[3]).id;
//                    var radiolist = $(radioid).find('input:radio');
//                    radiolist.removeAttr('checked');
                  var radioval=  $('#<%=hdn_storeradio.ClientID %>').val();
                  chkenterprise = radioval;
                  $(GVMaintainReceiptMaster.rows[rowindex].cells[10].children[4]).val(chkenterprise);
              }

           
               // var id = 
            }
            else {
                var id = $('#<%=hdn_holdtxtval.ClientID %>').val();
                document.getElementById(id).value = res[1];
                var radioid1 = $('#<%=rdo_option1.ClientID %>').selector;
               
               
            }
           
            var inputs = $('#gv_result').find('input[type="checkbox"]') 
         
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                    else {
                        //inputs[i].checked = true;
                    }
                }
            }
       
        
         
        }

        function Bind_Searchdata() {

            var clickedradiooptions = $('#<%=hdn_holdtxtval.ClientID %>').val();
             var txtsearch = $('#<%=txt_search.ClientID %>').val();
             if (txtsearch == "") {
                 alert('Please enter text to serach HS/NPC Code');
                 return false;
             }
             $("#gv_result").append(" <tr><td align ='center' valign ='middle'><img src ='../Images/ajax-loader.gif' /></td></tr>");

             var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
             if (chkenterprise == "3") {

                 if (clickedradiooptions != "") {
                     if (chkenterprise == "3") {

                         if (clickedradiooptions.indexOf("txt_hscode1") > -1) {
                             chkenterprise = $("#<%=rdo_option1.ClientID%>").find(":checked").val();

                         }
                         if (clickedradiooptions.indexOf("txt_hscode2") > -1) {
                             chkenterprise = $("#<%=rdo_option2.ClientID%>").find(":checked").val();
                         }
                         if (clickedradiooptions.indexOf("txt_hscode3") > -1) {
                             chkenterprise = $("#<%=rdo_option3.ClientID%>").find(":checked").val();
                         }

                         if (clickedradiooptions.indexOf("txt_hscode4") > -1) {
                             chkenterprise = $("#<%=rdo_option4.ClientID%>").find(":checked").val();
                         }


                     }
                 }
                 else {
                     var chkstorechoice;
                     chkstorechoice = $('#<%=hdn_storeradio.ClientID %>').val();
                     chkenterprise = chkstorechoice;
                 }
             }
             var hcode = "HS Code";
             if (chkenterprise == "1") {
                 hcode = "HS Code";
             }
             else {
                 hcode = "NPC Code";
             }
            // alert(chkenterprise);
                        
            $.ajax({
                type: "POST",
              //  url :"mysite/myService.asmx?op=Bind_Searchgrid"
                url: "BacklogUnitStep3_Store.aspx/Bind_Searchgrid",
                data: '{searchitem:"' + txtsearch + '",codetype:"' + chkenterprise + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
                $("#heading").empty();
                $("#gv_result").empty();
              
            //    $("#heading").append("<tr ><td ><img src ='../Images/blank.png' class='imgdisplay' height '0px' width ='80px' /> </td><td  ><img src ='../Images/blank.png' class='imgdisplay' height ='0px' width ='80px' /></td><td></td></tr>");
                $("#heading").append("<tr class ='dynRowHeader'><td >Options</td><td  >"+hcode+"</td><td>Product Details</td></tr>");
                 //   $("#gv_result").append("<tr><td colsapn='3'><div style='overflow: auto;'><table>");
                //$("#gv_result").append("<tr class ='dynRowHeader' stlye ='display:none;'  ><td stlye ='width:80px;' >Options</td><td stlye ='width:80px;' >HS Code</td><td>Product Details</td></tr>");
           //     $("#gv_result").append("<tr ><td ><img src ='../Images/blank.png' class='imgdisplay' height '0px' width ='80px' /> </td><td  ><img src ='../Images/blank.png' class='imgdisplay' height ='0px' width ='80px' /></td><td></td></tr>");
                    var sb = "<tr><td colsapn='3' ><div style='overflow: scroll; '><table style ='height: 100%; width: 100%;'>";

                    for (var i = 0; i < response.d.length; i++) {

                        if (i % 2 == 0) {
                           // sb = sb.concat("<tr class ='dynRowEven'><td>" + response.d[i].HSID + "</td><td>" + response.d[i].HSCode + "</td><td>" + response.d[i].HSCodedescription + "</td></tr>");
                            $("#gv_result").append("<tr class ='dynRowEven'><td stlye ='width:80px;' >  <input type ='checkbox' id ='" + response.d[i].HSID + "," + response.d[i].HSCode + " ' onclick = 'javascript:CheckSingleCheckbox(this)' /> </td><td  stlye ='width:80px;' >" + response.d[i].HSCode + "</td><td>" + response.d[i].HSCodedescription + "</td></tr>");
                        }
                        else {
                           // sb = sb.concat("<tr class ='dynRowOdd'><td>" + response.d[i].HSID + "</td><td>" + response.d[i].HSCode + "</td><td>" + response.d[i].HSCodedescription + "</td></tr>");
                            $("#gv_result").append("<tr class ='dynRowOdd' ><td stlye ='width:80px;' ><input type ='checkbox' id ='" + response.d[i].HSID + "," + response.d[i].HSCode + " ' onclick = 'javascript:CheckSingleCheckbox(this)' /> </td><td stlye ='width:80px;' >" + response.d[i].HSCode + "</td><td>" + response.d[i].HSCodedescription + "</td></tr>");
                        }


                    }
                    sb = sb.concat("</table></div></td></tr>");
                   // $("#gv_result").append(sb);
                      $("#gv_result").freezeHeader({ 'height': '300px' });
               
                    $('#mask').show();
                    $('#<%=pnlpopup.ClientID %>').show();
                    return false;
                },
                error: function(response) {

                    alert(response.status + ' ' + response.statusText);
                }
            });
        }
        
        
        function getradioval(obj)
        {
          var RB1 = document.getElementById(obj);
            var radio = RB1.getElementsByTagName("input");
            var label = RB1.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                return radio[i].value;
                }
            }
        }

        function radiochange(val,val1) {
            //  alert('hi');
            var id ;
            if (val1 != "1") {
                id = val;
            }
            else {
                id = val.id;
            }
            

            var RB1 = document.getElementById(id);
            var radio = RB1.getElementsByTagName("input");
            var label = RB1.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
//                    alert("SelectedText = " + label[i].innerHTML);
                    //                    alert("SelectedValue = " + radio[i].value);
                    if (val1 == "1") {
                        $('#<%=hdn_storeradio.ClientID %>').val(radio[i].value);
                    }
                    else {
                        radio[i].checked = false;
                    }
                    
                }
            }


        }


        function ShowPopupUpper(obj) {
            var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();

            if (chkenterprise == "1") {
                $('#<%=lbl_head.ClientID%>').html("Seach HS CODE FOR PARTICULAR STORE");
            }
            else if (chkenterprise == "2") {
                $('#<%=lbl_head.ClientID%>').html("Seach NPC CODE FOR PARTICULAR STORE");
            }
            else {
                var chkonblur = $('#<%=hdn_holdtxtval.ClientID %>').val();
                alert(chkonblur);
                if (chkonblur == "") {
                    alert('please put cursor on TextBox ');
                    return false;
                }

                if (chkonblur.indexOf("txt_hscode1") > -1) {
                    chkenterprise = $('#<%=rdo_option1.ClientID %> input:checked ').val();
                }
                else if (chkonblur.indexOf("txt_hscode2") > -1) {
                chkenterprise = $('#<%=rdo_option2.ClientID %> input:checked ').val();
            }
            else if (chkonblur.indexOf("txt_hscode3") > -1) {
                chkenterprise = $('#<%=rdo_option3.ClientID %> input:checked ').val();
            }
            else if (chkonblur.indexOf("txt_hscode4") > -1) {
                chkenterprise = $('#<%=rdo_option4.ClientID %> input:checked ').val();
            }
            if (chkenterprise == "1") {
                $('#<%=lbl_head.ClientID%>').html("Seach HS CODE FOR PARTICULAR STORE");
            }
            else {
                $('#<%=lbl_head.ClientID%>').html("Seach NPC CODE FOR PARTICULAR STORE");
            }
            
                
            }

            $('#mask').show();
            $("#heading").empty();
            $("#gv_result").empty();
            $('#<%=pnlpopup.ClientID %>').show();
        }
        
        

        function ShowPopup(obj) {

            var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
                    
            $('#mask').show();
            $("#heading").empty();
            $("#gv_result").empty();
    
            var GVMaintainReceiptMaster = document.getElementById('thetable');
            var rows = document.getElementById('thetable').getElementsByTagName('tbody')[0].getElementsByTagName('tr');

            for (i = 1; i < rows.length; i++) {

                rows[i].onclick = function() {
                    $('#<%=hdn_rowindex.ClientID %>').val(this.rowIndex);
                    var radioid = $(GVMaintainReceiptMaster.rows[this.rowIndex].cells[10].children[3]).context.id
                    //                var radiolist = $(radioid).find('input:radio');
                    var selectedval = getradioval(radioid);
                    //                    alert(selectedval);
                    //                    alert(radioid);
                    if (chkenterprise == "3") {
                        chkenterprise = selectedval;
                    }
                    
                    if (chkenterprise == "1") {
                        $('#<%=lbl_head.ClientID%>').html("Seach HS CODE FOR PARTICULAR STORE");
                    }
                    else if (chkenterprise == "2") {
                        $('#<%=lbl_head.ClientID%>').html("Seach NPC CODE FOR PARTICULAR STORE");
                    }
//                    else {
//                        var chkstorechoice;
//                        chkstorechoice = $('#<%=hdn_storeradio.ClientID %>').val();
//                        if (chkstorechoice == "1") {
//                            $('#<%=lbl_head.ClientID%>').html("Seach HS CODE FOR PARTICULAR STORE");
//                        }
//                        else {
//                            $('#<%=lbl_head.ClientID%>').html("Seach NPC CODE FOR PARTICULAR STORE");
//                        }
//                    }

                }
            }
        
            $('#<%=pnlpopup.ClientID %>').show();
        }
        function HidePopup() {
            var GVMaintainReceiptMaster = document.getElementById('thetable');
            var rows = document.getElementById('thetable').getElementsByTagName('tbody')[0].getElementsByTagName('tr');
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();           
            $("#heading").empty();
            $("#gv_result").empty();
            $('#<%=hdn_holdtxtval.ClientID %>').val("");
            $('#<%=hdn_rowindex.ClientID %>').val("");
          
            
            $('#<%=hdn_storeradio.ClientID %>').val("1");
//            var currentindex = $('#<%=hdn_rowindex.ClientID %>').val();
//            var radioid = rows[currentindex].cells[10].children[3].id;
            //            radiochange(radioid, 2);
           // $('#<%=rdo_option1.ClientID %>').removeAttr('checked');
          
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
    </style>
  
 
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <%--<script type="text/javascript">
     Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>--%>

<div>
<asp:HiddenField ID ="hdn_rowindex" runat ="server" />
<asp:HiddenField ID ="hdn_storeradio" runat ="server" Value ="1" />
<asp:HiddenField ID ="hdn_holdtxtval" runat ="server" />

<asp:MultiView ID="mvStore" runat="server" ActiveViewIndex="0" >
            <asp:View ID="viewRepeater" runat="server">
            
        

    <div align="left" style="padding-left:10px;" class="header">
                Per month per shift
             </div>
             <div align="left" style="padding-left:30px;">
             <asp:HiddenField runat ="server" ID ="hdn_enterprise" />
                <asp:Repeater ID="repeaterPmps" runat="server" 
                    
                     >
                    <HeaderTemplate>
                        <table border="1" cellpadding="0" cellspacing="0">
                         <tr>
                            <th>
                                Factory location
                            </th>
                            <th>
                                Pmps
                            </th>
                            <%--<th>
                                Ps
                            </th>--%>
                         </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                          
                                <asp:HiddenField ID="hfUnitidpmps" runat="server" Value='<%# Eval("fGPtbl_CUnit_Id") %>' />
                                <asp:Literal ID="ltlFactory" runat="server" Text='<%#Eval("fGPtbl_CUnit_1B_Factory_Address")%>'></asp:Literal>
                            </td>
                            <td>
                               
                                <asp:RadioButtonList ID="radlist" runat="server" RepeatDirection="Horizontal" >
                                    <asp:ListItem Text="Pmps" Selected ="True" Value="Pmps" ></asp:ListItem>
                                    <asp:ListItem Text="Pm" Value="Pm"></asp:ListItem>
                                    <asp:ListItem Text="PA" Value="PA"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RFValidator" runat="server" ErrorMessage="Please select PMPS/PS"
                                     Display="Dynamic" SetFocusOnError="true" ControlToValidate="radlist" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                           <%-- <td>
                                <asp:CheckBox ID="chkPs" runat="server" Text="ps" />
                            </td>--%>
                         
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                    
                </asp:Repeater>
             </div>
             <div align="left" style="height:10px;">
                 </div>
                 
                       <div align="left" style="height:10px;">
             
                 </div>
             <br />
               <div align="left" style="padding-left:30px;">
             <table border ="1" cellpadding ="5" cellspacing ="0" width ="50%">
                 <tr>
                   <td style ="width:7%;" class="leftHeading"><asp:Label runat ="server" ID ="lbl_heading"></asp:Label>   </td>
                   <td style ="width:10%;"> 
                   <asp:TextBox ID ="txt_hscode1" ReadOnly ="true" TabIndex ="1" MaxLength ="10"  Width ="100%"  onblur ="javascript:setsearchpostion(this);" runat ="server"></asp:TextBox> <br />
                    <asp:RadioButtonList ID="rdo_option1" name ="rdo_option1" runat="server" onClick ="javascript:radiochange(this,1);" RepeatDirection="Horizontal">
                                                <asp:ListItem  Value="1" Selected ="True" >HS Code </asp:ListItem>
                                                <asp:ListItem Value="2">NPC Code </asp:ListItem>
                                                 </asp:RadioButtonList> </td>
                   <td style ="width:10%;"> <asp:TextBox ID ="txt_hscode2" ReadOnly  ="true"  onblur ="javascript:setsearchpostion(this);" MaxLength ="10"  Width ="100%" runat ="server"></asp:TextBox> <br />
                    <asp:RadioButtonList ID="rdo_option2" runat="server" onClick ="javascript:radiochange(this,1);" RepeatDirection="Horizontal">
                                                <asp:ListItem  Value="1" Selected ="True">HS Code </asp:ListItem>
                                                <asp:ListItem Value="2">NPC Code </asp:ListItem>
                                                 </asp:RadioButtonList> </td>
                     <td style ="width:10%;"> <asp:TextBox ID ="txt_hscode3" ReadOnly  ="true"   onblur ="javascript:setsearchpostion(this);" MaxLength ="10" Width ="100%" runat ="server"></asp:TextBox> <br />
                      <asp:RadioButtonList ID="rdo_option3" onClick ="javascript:radiochange(this,1);" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem  Value="1" Selected ="True">HS Code </asp:ListItem>
                                                <asp:ListItem Value="2">NPC Code </asp:ListItem>
                                                 </asp:RadioButtonList>  </td>
                      <td style ="width:10%;"> <asp:TextBox ID ="txt_hscode4" ReadOnly  ="true"  onblur ="javascript:setsearchpostion(this);" MaxLength ="10" Width ="100%"   runat ="server"></asp:TextBox><br />
                       <asp:RadioButtonList ID="rdo_option4" runat="server" onClick ="javascript:radiochange(this,1);" RepeatDirection="Horizontal">
                                                <asp:ListItem  Value="1" Selected ="True">HS Code </asp:ListItem>
                                                <asp:ListItem Value="2">NPC code </asp:ListItem>
                                                 </asp:RadioButtonList>  </td>
                                                
                 </tr>
                 <tr>
                 <td colspan ="5" align ="center">
                 <input type ="button" id = "btn_searchhscode" onclick ="javascript:ShowPopupUpper(this);" value ="Search" class ="btnCls" />
                     
                 </td>
                 </tr>
             </table>
             </div>
             <br />
            <div align="left" style="padding-left:10px; padding-right:10px;">
                 <asp:Repeater ID="StoreRepeaterBranch" runat="server" 
                     onitemdatabound="StoreRepeaterBranch_ItemDataBound" >
                                    
                                        <HeaderTemplate>
                                            <table border="1" cellpadding="0" cellspacing="0" id ="thetable">        
                                                <tr style="width:700px;">
                                                    <th style="width:50px;" align="center">
                                                       S.NO/Store Code  
                                                       </th>
                                                       
                                                     <th style="width:100px;" align="center">
                                                        Factory Location
                                                     </th>
                                                    
                                                    <th style="width:50px;" align="center" >
                                                        Description of Stores/ Products<br />भंडारों/ उत्पादों का विवरण
                                                    </th>
                                                    <th>
                                                        Ref. of BIS Specification<br />बी.आई.एस. विशिष्ट का सन्दर्भ
                                                    </th>
                                                    <th style="width:100px;">
                                                        Model/ Brand<br />माडल/ ब्रांड
                                                    </th>
                                                    <th style="width:100px;">
                                                        Limiting Size Capacity/ Rating<br />सीमित आकार/ क्षमता/ रेटिंग
                                                    </th>
                                                    <th style="width:100px;">
                                                        Annual Production Capacity per shift<br />वार्षिक उत्पादन क्षमता प्रति पारि</div>
                                                    </th>    
                                                    <th style="width:70px;">
                                                        No. of shifts factory normally works<br />पारियों की सं. जो प्राय: कारखाने में चलती है
                                                    </th>
                                                    
                                                    <th>
                                                        Qualitative Capacity
                                                    </th>
                                                    <th>
                                                        Quantitative Capacity
                                                    </th>
                                                    <th>
                                                        HS/NPC Code
                                                    </th>
                                                
                                                    </tr>    
                                                    
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        
                                        <tr>
                                        
                                        <td style="width:100px;" valign="top">
                                        
                                          <asp:TextBox ID="txt_serial_no" runat="server" CssClass="txt_bx150" Width="20px">0</asp:TextBox></td>
                                          <td style="width:150px;" valign="top">
                                                <asp:DropDownList ID="ddlFactLoc" runat="server" Width="100px" ></asp:DropDownList>
                                          </td>
                                            <td style="width:100px;">
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="txt_bx150" Width="180px" Height="100px" TextMode="MultiLine" Wrap="false" Text="0" ></asp:TextBox></td>
                                        <td style="width:100px;"><asp:TextBox ID="txtBis" runat="server"  CssClass="txt_bx150" Width="180px" Height="100px" TextMode="MultiLine" Wrap="false" Text="0" ></asp:TextBox></td>
                                        <td style="width:100px;"><asp:TextBox ID="txtModel" runat="server"  CssClass="txt_bx150" Width="100px" Height="100px" TextMode="MultiLine" Wrap="false" Text="0" ></asp:TextBox></td>
                                        <td style="width:100px;"><asp:TextBox ID="txtLimiting" runat="server"  CssClass="txt_bx150" Width="100px" Height="100px" TextMode="MultiLine" Wrap="false" Text="0"></asp:TextBox></td>
                                        <td style="width:100px;"><asp:TextBox ID="txtAnnual" runat="server"  CssClass="txt_bx150" Width="100px" Height="100px" TextMode="MultiLine" Wrap="false" Text="0" ></asp:TextBox></td>
                                        <td style="width:100px;" valign="top">
                                            <asp:TextBox ID="txtShift" runat="server"  CssClass="txt_bx150" Width="70px" Text="0" ></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtShift" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                        </td>
                                        
                                        <td style="width:370px;">
                                            <asp:TextBox ID="txtQuality" runat="server" CssClass="txt_bx150" Width="370px"  TextMode="MultiLine" Wrap="false" Height="100px" Text="0"></asp:TextBox>   <%-- TextMode="MultiLine" Wrap="false"--%>
                                            <asp:RequiredFieldValidator id="regquality" runat="server" controltovalidate="txtquality"
                                display="none" validationgroup="1"  errormessage=" please fill qualitative capacity !" setfocusonerror="true"
             ></asp:RequiredFieldValidator>
                            <asp:validatorcalloutextender id="validcallout" runat="server" popupposition="bottomleft"
                                targetcontrolid="regquality"></asp:validatorcalloutextender>
                                        
                                        </td>
                                        <td style="width:370px;">
                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="txt_bx150" Width="370px" TextMode="MultiLine" Wrap="false" Height="100px" Text="0"></asp:TextBox>  <%-- Width="80px" TextMode="MultiLine" Wrap="false"--%>
                                        <asp:RequiredFieldValidator ID ="regquantity" runat ="server" ControlToValidate = "txtQuantity"
                                    display="none"     ErrorMessage = "Please Fill Quantitative Capacity !" ValidationGroup = "1" ></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="validcalloutquant" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="regquantity"></asp:ValidatorCalloutExtender>
                                        </td>
                                        <td  valign="top">
                                       <%-- <asp:DropDownList ID="ddlHSCode" runat="server" Width="80px">
                                        </asp:DropDownList>--%>
                                        <asp:TextBox ID ="txt_hscode" ReadOnly ="true" CssClass="txt_bx150"  runat ="server" Width ="100px">
                                        </asp:TextBox> <br />
                                        <%--- hdn_hstextbox used for maintaing hscode id   %>   --%>
                                         <%--- hdn_repeater_options used for maintaing hscode or npc   %>   --%>
                                <asp:HiddenField ID ="hdn_hstextbox" runat ="server" />
                                    <%--    <asp:HiddenField ID ="hdn_repeater_options" Value ="1" runat ="server" />--%>
                                      <%--  <input type ="text" id ="hscode" />--%>
                                       
                                            <asp:RadioButtonList  ID="rdooption_repeater" onClick ="javascript:radiochange(this,1);" RepeatDirection ="Horizontal" runat="server">
                                             <asp:ListItem  Value ="1"> HS  </asp:ListItem>
                                              <asp:ListItem  Value ="2"> NPC  </asp:ListItem>
                                            </asp:RadioButtonList>
                                          
                                                 <asp:HiddenField ID ="hdn_hsornpc" runat ="server" />
                                          <%--  <asp:Button ID="btn_search_repeater" CausesValidation ="false" Text ="Seach" CssClass ="btnCls" runat ="server" />--%>
                                            <input type ="button" id ="btn_search_repeater" value ="Seach" class ="btnCls" onclick ="javascript:ShowPopup(this);" />
                                       
                                        </td>
                                        
                                        </tr>
                                         
                                        </ItemTemplate>
                                        
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
             </div>
             
               <div align="right" style="padding-top:10px; padding-right:10px;">
                <asp:UpdatePanel ID="UpdatePanelStoreSubmit" runat="server"><ContentTemplate><asp:Button ID="btnBack" runat="server" CssClass="btnCls" Text="Back" 
                                        onclick="btnBack_Click" />&nbsp;&nbsp;&nbsp; <asp:Button ID="btnStoreSubmit" runat="server" CssClass="btnCls" Text="Save&amp;Continue"  Width="120px"
                                   
 OnClientClick="if(!validate()) { return false;}"      onclick="btnStoreSubmit_Click" /><asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelStoreSubmit"><ProgressTemplate><img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" style="padding-right:30px;" border="0" /></ProgressTemplate></asp:UpdateProgress></ContentTemplate></asp:UpdatePanel>
            </div>
            </asp:View>
            
            
            <asp:View ID="viewGrid" runat="server">
            
                <asp:GridView ID="gvStoreDetails" runat="server" 
                        AutoGenerateColumns="False" BackColor="#FFFFFF" 
                        BorderWidth="0" CellPadding="3" CellSpacing="1" Width="100%" 
                        onrowdatabound="gvStoreDetails_RowDataBound">
                        <HeaderStyle CssClass="dynRowHeader" />
                        <RowStyle CssClass="dynRowEven" />
                        <AlternatingRowStyle CssClass="dynRowOdd" />
                        <EmptyDataTemplate>
                            <center>
                                <div style="color:Red; font-size:18px;">
                                    Click on Add Store button to add stores.<br /> (You have to add at least ONE store to move to next step.)
                                </div>
                            </center>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="S no. /Code"><ItemTemplate><asp:Literal ID="ltlsnocode" runat="server" Text='<%# Bind("fGPtbl_CUnit_Store_SNo") %>'></asp:Literal></ItemTemplate></asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Location"><ItemTemplate><asp:HiddenField ID="hdnUnitid" runat="server" Value='<%# Bind("fGPtbl_CUnit_Store_UnitId") %>' /><asp:HiddenField ID="hdnStoreid" runat="server" Value='<%# Bind("fGPtbl_CUnit_Store_Id") %>' /><asp:Literal ID="ltlLocation" runat="server"></asp:Literal></ItemTemplate></asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Description/ Stores/ Products (भंडारों/ उत्पादों का विवरण)" ><ItemTemplate><asp:Literal ID="ltlDescription" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_DescriptionStoresProducts") %>'></asp:Literal></ItemTemplate></asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Ref. of BIS Specification (बी.आई.एस. विशिष्ट का सन्दर्भ)"><ItemTemplate><asp:Literal ID="ltlBis" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_RefOfBISSpecification") %>'></asp:Literal></ItemTemplate></asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Model/ Brand (माडल/ ब्रांड)" ><ItemTemplate><asp:Literal ID="ltlModel" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_ModelBrand") %>' ></asp:Literal></ItemTemplate></asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Limiting Size Capacity/ Rating (सीमित आकार/ क्षमता/ रेटिंग)" ><ItemTemplate><asp:Literal ID="ltlSize" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") %>'></asp:Literal></ItemTemplate></asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Annual Production Capacity per shift (वार्षिक उत्पादन क्षमता प्रति पारि)" ><ItemTemplate><asp:Literal ID="ltlProduction" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_AnnualProductionCapacity") %>'></asp:Literal></ItemTemplate></asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="No. of shifts factory normally works  (पारियों की सं. जो प्राय: कारखाने में चलती है)" ItemStyle-Width="50px" ><ItemTemplate><asp:Literal ID="ltlShift" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_NoOfShifts") %>'></asp:Literal></ItemTemplate></asp:TemplateField>
                              <asp:TemplateField HeaderText="Qualitative Capacity"><ItemTemplate><asp:Label ID="lbl_qualitativecapcity" runat="server" CssClass="lblstyle"
                            Text='<%# Bind("fGPtbl_CUnit_Store_QualitativeCapacity") %>'></asp:Label></ItemTemplate><FooterTemplate><asp:TextBox runat ="server" ID = "txt_footqualitativecapcity" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox></FooterTemplate></asp:TemplateField>
                
                <asp:TemplateField HeaderText="Quantitative Capacity"><ItemTemplate><asp:Label ID="lbl_quantitativecapacity" runat="server" CssClass="lblstyle"
                            Text='<%# Bind("fGPtbl_CUnit_Store_QuantitativeCapacity") %>'></asp:Label></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText="HS/NPC Code"><ItemTemplate><asp:Label runat ="server" ID = "lbl_hscode" Text = '<%# Bind("fGPtbl_CUnit_Store_HsCode") %>'></asp:Label></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText="Code Type"><ItemTemplate><asp:Label runat ="server" ID = "lbl_hscodetype" Text = '<%# Bind("HSCodeType") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                             
                        </Columns>
                      <SelectedRowStyle ForeColor="#CCFF99" Font-Bold="True" 
                        BackColor="#009999"></SelectedRowStyle>
                        <RowStyle ForeColor="#003399" BackColor="White"></RowStyle>
                   </asp:GridView>
                   
                   
                   <div align="right">
                        <asp:Button ID="btnAddStore" runat="server" Text="AddStore" CssClass="btnCls" 
                            onclick="btnAddStore_Click" />&nbsp;&nbsp;
                            
                        <asp:Button ID="btnSave" runat="server" Text="Save&Continue" CssClass="btnCls" 
                           Width="120px" onclick="btnSave_Click" />
                   </div>
            </asp:View>
            </asp:MultiView>
            
     </div>
     
    <div id="mask">
    </div>
      <asp:Panel ID="pnlpopup"  runat="server"  BackColor="White" Height="70%"
            Width="50%" Style="z-index:111;background-color: White; position: fixed; left: 25%;  top: 20%; 
 
border: outset 2px gray;padding:5px;display:none">
            <table width="100%" cellpadding="0" style="border:1px solid black;" cellspacing="0">
         
            </tr>
                <tr class ="menutitle">
                    <td colspan="2" style="color:White; font-weight: bold; font-size: 1.2em; padding:3px"
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
                      <asp:Button ID="btn_search" CssClass ="btnCls"  OnClientClick="if(!Bind_Searchdata()) { return false;}"   runat="server" Text="Search"  />
                    </td>
                </tr>
                <tr>
                <td colspan ="2"> 
                &nbsp;&nbsp;&nbsp;
                
                </td>
                </tr>
                <tr>
                  <td colspan ="2">
                  
                  <table id ="heading" class="fixed" cellPadding="3" CellSpacing="1"  Width="100%">
                   
                  </table>
                  <table id ="gv_result"  class="hfixed" cellPadding="3" CellSpacing="1"  Width="100%" >
                   </table>
                  </td>
                </tr>
            </table>
        </asp:Panel>
</asp:Content>

