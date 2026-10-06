<%@ Page Title="Inspection Complete of Factories for Registration" Language="C#" MasterPageFile="~/appMaster/Admin.master" EnableEventValidation="false"
    ValidateRequest="false" AutoEventWireup="true" CodeFile="ICRegistration_View.aspx.cs" Inherits="appAdministrator_ICRegistration_View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"><%--nochange--%>

    <script src="../JScript/jquery.freezeheader.js" type="text/javascript"></script>
    <%--<script src="https://code.jquery.com/jquery-2.1.1.min.js"></script>  --%>


    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        var storearray = new Array();
        function setsearchpostion(obj) {


            $('#<%=hdn_holdtxtval.ClientID %>').val(obj.id)

    }

    $(document).ready(function () {
        $("#Header_Admin").hide();
        $("#" + "ctl00_TreeMenu_Admin1_lblmenu").hide();
        // showhidecontent();
    });
    function showhidecontent() {

        var text = $("#ctl00_ContentPlaceHolder1_lbl_show_message_text").text();
        // alert(text);
        if (text != "") {
            $("#listofdocument").css("display", "None");
            $("#permonthshift").css("display", "None");
            $("#ctl00_ContentPlaceHolder1_ChkListDoc").css("display", "None");
        }
        else {
            $("#listofdocument").css("display", "block");
            $("#permonthshift").css("display", "block");
            $("#ctl00_ContentPlaceHolder1_ChkListDoc").css("display", "block");
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
        var chkenterprise = "1";
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
              else if (chkenterprise == "2") {
                  hcode = "NPC Code";
              }
              else {
                  hcode = "HS Code";
              }



              $.ajax({
                  type: "POST",
                  url: "ICRegistration_View.aspx/Bind_Searchgrid",
                  data: '{searchitem:"' + txtsearch + '",codetype:"' + chkenterprise + '"}',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function (response) {
                      $("#heading").empty();
                      $("#gv_result").empty();


                      $("#heading").append("<tr class ='dynRowHeader'><td >Options</td><td  >" + hcode + "</td><td>Product Details</td></tr>");

                      var sb = "<tr><td colsapn='3' ><div style='overflow: scroll; '><table style ='height: 100%; width: 100%;'>";

                      for (var i = 0; i < response.d.length; i++) {

                          if (i % 2 == 0) {

                              $("#gv_result").append("<tr class ='dynRowEven'><td stlye ='width:80px;' >  <input type ='checkbox' id ='" + response.d[i].HSID + "," + response.d[i].HSCode + " ' onclick = 'javascript:CheckSingleCheckbox(this)' /> </td><td  stlye ='width:80px;' >" + response.d[i].HSCode + "</td><td>" + response.d[i].HSCodedescription + "</td></tr>");
                          }
                          else {

                              $("#gv_result").append("<tr class ='dynRowOdd' ><td stlye ='width:80px;' ><input type ='checkbox' id ='" + response.d[i].HSID + "," + response.d[i].HSCode + " ' onclick = 'javascript:CheckSingleCheckbox(this)' /> </td><td stlye ='width:80px;' >" + response.d[i].HSCode + "</td><td>" + response.d[i].HSCodedescription + "</td></tr>");
                          }


                      }
                      sb = sb.concat("</table></div></td></tr>");

                      $("#gv_result").freezeHeader({ 'height': '300px' });

                      $('#mask').show();
                      $('#<%=pnlpopup.ClientID %>').show();
                    return false;
                },
                error: function (response) {

                    alert(response.status + ' ' + response.statusText);
                }
            });
        }





        function ShowPopupUpper(obj) {

            var chkonblur = $('#<%=hdn_holdtxtval.ClientID %>').val();
               // alert(chkonblur);
               if (chkonblur == "") {
                   alert('please put cursor on TextBox ');
                   return false;
               }
               //             alert("hello");
               //             alert(obj.id);
               var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
               //alert(chkenterprise);
                if (chkenterprise == "1") {
                    $('#<%=lbl_head.ClientID%>').html("Seach HS CODE FOR PARTICULAR STORE");
            }
            else if (chkenterprise == "2") {
                $('#<%=lbl_head.ClientID%>').html("Seach NPC CODE FOR PARTICULAR STORE");
            }
            else {


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

            if (chkenterprise == "1") {
                $('#<%=lbl_head.ClientID%>').html("Seach HS CODE FOR PARTICULAR STORE");
            }
            else if (chkenterprise == "2") {
                $('#<%=lbl_head.ClientID%>').html("Seach NPC CODE FOR PARTICULAR STORE");
            }

        $("#<%=txt_search.ClientID %>").val("");

            $('#mask').show();
            $("#heading").empty();
            $("#gv_result").empty();

            var GVMaintainReceiptMaster = document.getElementById('thetable');
            var rows = document.getElementById('thetable').getElementsByTagName('tbody')[0].getElementsByTagName('tr');

            for (i = 1; i < rows.length; i++) {

                rows[i].onclick = function () {

                    $('#<%=hdn_rowindex.ClientID %>').val(this.rowIndex);
                    if (chkenterprise == "3") {

                        var radioid = $(GVMaintainReceiptMaster.rows[this.rowIndex].cells[9].children[3]).context.id

                        var selectedval = getradioval(radioid);
                        chkenterprise = selectedval;
                    }

                    if (chkenterprise == "1") {
                        $('#<%=lbl_head.ClientID%>').html("Seach HS CODE FOR PARTICULAR STORE");
                    }
                    else if (chkenterprise == "2") {
                        $('#<%=lbl_head.ClientID%>').html("Seach NPC CODE FOR PARTICULAR STORE");
                    }


                }
        }


        $('#<%=pnlpopup.ClientID %>').show();
        }

        function getradioval(obj) {
            var RB1 = document.getElementById(obj);
            var radio = RB1.getElementsByTagName("input");
            var label = RB1.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    return radio[i].value;
                }
            }
        }

        function HidePopup() {
            var GVMaintainReceiptMaster = document.getElementById('thetable');
            var rows = document.getElementById('thetable').getElementsByTagName('tbody')[0].getElementsByTagName('tr');
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
            $("#heading").empty();
            $("#gv_result").empty();
            $("#<%=txt_search.ClientID %>").val("");
            $('#<%=hdn_holdtxtval.ClientID %>').val("");
            $('#<%=hdn_rowindex.ClientID %>').val("");


            $('#<%=hdn_storeradio.ClientID %>').val("1");





        }



        function radiochange(val, val1) {
            //  alert('hi');
            var id;
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

                    if (val1 == "1") {
                        $('#<%=hdn_storeradio.ClientID %>').val(radio[i].value);
                    }
                    else {
                        radio[i].checked = false;
                    }

                }
            }


        }


        function CheckSingleCheckbox(obj) {


            var id = obj.id;
            var res = id.split(",");
            //alert(res[1]);
            var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
            var GVMaintainReceiptMaster = document.getElementById('thetable');
            var rowindex = $('#<%=hdn_rowindex.ClientID %>').val();

            if (rowindex != "") {
                $(GVMaintainReceiptMaster.rows[rowindex].cells[9].children[0]).val(res[1]);
                $(GVMaintainReceiptMaster.rows[rowindex].cells[9].children[2]).val(res[0]);

                if (chkenterprise == "3") {

                    var radioval = $('#<%=hdn_storeradio.ClientID %>').val();
                            chkenterprise = radioval;
                            var uu = $(GVMaintainReceiptMaster.rows[rowindex].cells[9].children[4]).val("3");
                            // alert(uu);
                            $(GVMaintainReceiptMaster.rows[rowindex].cells[9].children[4]).val(chkenterprise);
                        }


                    }
                    else {
                          //$("#thetable").children().children()[rowindex].children[9].val(""); 
                        var id = $('#<%=hdn_holdtxtval.ClientID %>').val();
                          document.getElementById(id).value = res[1];
                          var radioid1 = $('#<%=rdo_option1.ClientID %>').selector;
                            }



              //  var inputs = $('#gv_result').find('input[type="checkbox"]') 

              //            for (var i = 0; i < inputs.length; i++) 
              //                {
              //                if (inputs[i].type == "checkbox") 
              //                {
              //                    if (obj.checked && inputs[i] != obj && inputs[i].checked) 
              //                    {
              //                        inputs[i].checked = false;
              //                    }
              //                    else 
              //                    {
              //                        
              //                    }
              //                }
              //            }

                            HidePopup();

                        }

                        function validate() {
                            var valid = true;
                            var uppervalid = true;

                            var GVMaintainReceiptMaster = document.getElementById('thetable');
                            var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
            uppervalid = validateupper();
            var radioidval;
            var rows = GVMaintainReceiptMaster.rows;
            for (i = 1, n = rows.length; i < n; ++i) {
                chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
                if (chkenterprise == "3") {
                    var radioid = rows[i].cells[9].children[3].id;
                    radioidval = getradioval(radioid);
                    chkenterprise = radioidval;
                }
                var id = rows[i].cells[9].children[0].value.trim();
                if (id != "") {
                    if (chkenterprise == "1") {
                        if (parseInt(id.length) > 3 && parseInt(id.length) < 9) {
                            if (parseInt(id.length) == 4 || parseInt(id.length) == 5|| parseInt(id.length) == 6 || parseInt(id.length) == 8) {
                                rows[i].cells[9].children[0].className = "txt_bx150";
                                valid = true;
                            }
                            else {
                                rows[i].cells[9].children[0].className = "txt_invalidhscode";
                                valid = false;
                            }

                        }
                        else {
                            rows[i].cells[9].children[0].className = "txt_invalidhscode";
                            valid = false;
                        }
                    }

                    else {
                        if (parseInt(id.length) > 3 && parseInt(id.length) < 9) {
                            if (parseInt(id.length) == 4 || parseInt(id.length) == 5|| parseInt(id.length) == 6 || parseInt(id.length) == 7 || parseInt(id.length) == 8) {
                                rows[i].cells[9].children[0].className = "txt_bx150";
                                valid = true;
                            }
                            else {
                                rows[i].cells[9].children[0].className = "txt_invalidhscode";
                                valid = false;
                            }
                        }
                        else {
                            rows[i].cells[9].children[0].className = "txt_invalidhscode";
                            valid = false;
                        }
                    }



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
              if (chkenterprise == "1") {
                  if (hscode1 != "") {
                      //                    if (parseInt(hscode1.length) != 4) {
                      //                        $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                      //                    //    alert('Invalid Hscode 1');
                      //                        chkvalid = false;
                      //                    }

                      if (parseInt(hscode1.length) > 3 && parseInt(hscode1.length) < 9) {
                          if (parseInt(hscode1.length) == 4 || parseInt(hscode1.length) == 6 || parseInt(hscode1.length) == 8) {
                              $('#<%=txt_hscode1.ClientID %>').addClass("txt_bx150");
                              valid = true;
                          }
                          else {
                              $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                              valid = false;
                          }
                      }
                      else {
                          $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                          valid = false;
                      }
                      //  }

                  }
                  if (hscode2 != "") {
                      //                    if (parseInt(hscode2.length) != 4) {
                      //                        $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                      //                      //  alert('Invalid Hscode 2');
                      //                        chkvalid = false;
                      //                    }

                      if (parseInt(hscode2.length) > 3 && parseInt(hscode2.length) < 9) {
                          if (parseInt(hscode2.length) == 4 || parseInt(hscode2.length) == 6 || parseInt(hscode2.length) == 8) {

                              $('#<%=txt_hscode2.ClientID %>').addClass("txt_bx150");
                              valid = true;
                          }
                          else {
                              $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                              valid = false;
                          }

                      }

                      else {
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

                      else {
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
                      if (parseInt(hscode4.length) > 3 && parseInt(hscode4.length) < 9) {
                          if (parseInt(hscode4.length) == 4 || parseInt(hscode4.length) == 6 || parseInt(hscode4.length) == 8) {

                              $('#<%=txt_hscode4.ClientID %>').addClass("txt_bx150");
                              valid = true;
                          }
                          else {
                              $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                              valid = false;
                          }

                      }

                      else {
                          $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                          valid = false;
                      }
                      //  }
                  }
              }
              else if (chkenterprise == "2") {

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


              else {
                  var selectedoption1, selectedoption2, selectedoption3, selectedoption4;
                  selectedoption1 = $('#<%=rdo_option1.ClientID %> input:checked ').val();
                  selectedoption2 = $('#<%=rdo_option2.ClientID %> input:checked ').val();
                  selectedoption3 = $('#<%=rdo_option3.ClientID %> input:checked ').val();
                  selectedoption4 = $('#<%=rdo_option4.ClientID %> input:checked ').val();

                  if (hscode1 != "") {
                      var code1len = parseInt(hscode1.length);
                      if (selectedoption1 == "2") {

                          if (code1len < 3 || code1len >= 9) {
                              $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                              //   alert('Invalid NPC Code 1');
                              chkvalid = false;
                          }
                      }
                      else {
                          //                    if (code1len != 4) {
                          //                        $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                          //                        // alert('Invalid HS Code 1');
                          //                        chkvalid = false;
                          //                    }
                          if (code1len == 4 || code1len == 6 || code1len == 9) {
                              $('#<%=txt_hscode1.ClientID %>').addClass("txt_bx150");
                              valid = true;
                          }
                          else {
                              $('#<%=txt_hscode1.ClientID %>').addClass("txt_invalidhscode");
                              valid = false;
                          }
                      }
                  }


                  if (hscode2 != "") {
                      //  alert(selectedoption2);
                      var code2len = parseInt(hscode2.length);
                      if (selectedoption2 == "2") {

                          if (code2len < 3 || code2len >= 9) {
                              $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                              //   alert('Invalid NPC Code 2');
                              chkvalid = false;
                          }
                      }
                      else {
                          //                    if (code2len != 4) {
                          //                        $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                          //                        //  alert('Invalid HS Code 2');
                          //                        chkvalid = false;
                          //                    }
                          if (code2len == 4 || code2len == 6 || code2len == 8) {
                              $('#<%=txt_hscode2.ClientID %>').addClass("txt_bx150");
                              valid = true;
                          }
                          else {
                              $('#<%=txt_hscode2.ClientID %>').addClass("txt_invalidhscode");
                              valid = false;
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
                          //                    if (code3len != 4) {
                          //                        $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
                          //                        //  alert('Invalid HS Code 3');
                          //                        chkvalid = false;
                          //                    }
                          //alert(code3len);
                          if (code3len == 4 || code3len == 6 || code3len == 8) {
                              $('#<%=txt_hscode3.ClientID %>').addClass("txt_bx150");
                              valid = true;
                          }
                          else {
                              $('#<%=txt_hscode3.ClientID %>').addClass("txt_invalidhscode");
                              valid = false;
                          }

                      }
                  }


                  if (hscode4 != "") {
                      var code4len = parseInt(hscode4.length);
                      if (selectedoption4 == "2") {
                          if (code4len < 4 || code4len >= 9) {
                              $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                              //  alert('Invalid NPC Code 4');
                              chkvalid = false;
                          }
                      }
                      else {
                          //                    if (code4len != 4) {
                          //                        $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                          //                        // alert('Invalid HS Code 4');
                          //                        chkvalid = false;
                          //                    }
                          if (code4len == 4 || code4len == 6 || code4len == 8) {
                              $('#<%=txt_hscode4.ClientID %>').addClass("txt_bx150");
                              valid = true;
                          }
                          else {
                              $('#<%=txt_hscode4.ClientID %>').addClass("txt_invalidhscode");
                              valid = false;
                          }

                      }
                  }

              }



          return chkvalid;
      }

      function CheckNumericKeyInfo(char1, mozChar) {
          if (mozChar != null) { // Look for a Mozilla-compatible browser
              if ((mozChar >= 48 && mozChar <= 57) || char1 == 8)
                  RetVal = true;
              else {
                  RetVal = false;
              }
          }
          else { // Must be an IE-compatible Browser
              if ((char1 >= 48 && char1 <= 57) || char1 == 8) RetVal = true;
              else {
                  RetVal = false;
              }
          }
          return RetVal;
      }
    </script>
    <style type="text/css">
        .label_mes {
            font-family: Arial;
            font-size: 20px;
            color: Red;
            font-weight: bold;
        }

        #mask {
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

        table.fixed {
            table-layout: fixed;
        }
            /*Setting the table width is important!*/
            table.fixed td {
                overflow: hidden;
            }
                /*Hide text outside the cell.*/
                table.fixed td:nth-of-type(1) {
                    width: 100px;
                }
                /*Setting the width of column 1.*/
                table.fixed td:nth-of-type(2) {
                    width: 100px;
                }
        /*Setting the width of column 2.*/


        table.hfixed {
            table-layout: fixed;
        }
            /*Setting the table width is important!*/
            table.hfixed td {
                overflow: hidden;
            }
                /*Hide text outside the cell.*/
                table.hfixed td:nth-of-type(1) {
                    width: 100px;
                }
                /*Setting the width of column 1.*/
                table.hfixed td:nth-of-type(2) {
                    width: 100px;
                }
        /*Setting the width of column 2.*/
        .water {
            font-family: Tahoma, Arial, sans-serif;
            color: gray;
        }

        .imgdisplay {
            display: none;
            width: 121px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div align="left">
        <asp:HiddenField ID="hdn_rowindex" runat="server" />
        <asp:HiddenField ID="hdn_storeradio" runat="server" Value="1" />
        <asp:HiddenField ID="hdn_holdtxtval" runat="server" />
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" Text="Inspection Complete Registration" CssClass="headerAll"></asp:Label>
        </div>
        <div align="left" style="height: 10px;">
        </div>
        <asp:MultiView ID="mvICReg" runat="server" ActiveViewIndex="0">
            <asp:View ID="viewGrid" runat="server">
                <div align="left" style="padding-left: 10px; padding-right: 10px;">
                    <asp:HiddenField ID="HiddenFieldCompany" runat="server" />
                    <asp:HiddenField ID="hiddenfieldCompanyId" runat="server" />
                    <asp:HiddenField ID="branchid" runat="server" />


                    <asp:GridView ID="gvICRegistration" runat="server" AutoGenerateColumns="false"
                        Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1"
                        OnRowDataBound="gvICRegistration_RowDataBound">
                        <HeaderStyle CssClass="dynRowHeader" />
                        <RowStyle CssClass="dynRowEven" />
                        <AlternatingRowStyle CssClass="dynRowOdd" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="fGPtbl_Company_GPReferenceNo" HeaderText="GP Referenceno." />--%>
                            <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                            <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No." />

                            <asp:BoundField DataField="fGPtbl_IS_DateStatus6" HeaderText="Inspection Report Date" />
                            <asp:BoundField DataField="fGPtbl_MIA_Name" HeaderText="Inspection agency name" />
                            <asp:TemplateField HeaderText="Inspection Report">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HLName" runat="server" Text="Report" ToolTip="View Inspection Report" Target="_blank"></asp:HyperLink>
                                    <asp:HiddenField ID="HiddenFieldpath" runat="server" Value='<%#Eval("fGPtbl_IS_ReportPath") %>' />
                                    <asp:HiddenField ID="HiddenFieldname" runat="server" Value='<%#Eval("fGPtbl_IS_ReportName") %>' />
                                    <asp:HiddenField ID="HiddenfieldCunitId" runat="server" Value='<%#Eval("fGPtbl_IS_UnitId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>

                </div>

                <div align="center" style="padding-top: 10px;">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
                <div align="center">
                    <asp:Button ID="btnAccept" runat="server" CssClass="btnCls" Text="Accept"
                        OnClick="btnAccept_Click" />&nbsp;&nbsp;
         <asp:Button ID="BackButton" runat="server" Text="Back" CssClass="btnCls"
             OnClick="BackButton_Click" />
                </div>
            </asp:View>
            <asp:View ID="viewRepeater" runat="server">

                <div align="left" style="padding-left: 10px;" class="header" id="permonthshift" runat="server">
                    Per month per shift
                </div>
                <div align="left" style="padding-left: 10px;" class="header">
                    <asp:Label ID="lbl_show_message_text" runat="server" Text=""></asp:Label>
                </div>

                <div align="left" style="padding-left: 30px;">
                    <asp:HiddenField runat="server" ID="hdn_enterprise" />
                    <asp:Repeater ID="repeaterPmps" runat="server"
                        OnItemDataBound="repeaterPmps_ItemDataBound">
                        <HeaderTemplate>
                            <table border="1" cellpadding="0" cellspacing="0">
                                <tr>
                                    <th>Factory location
                                    </th>
                                    <th>Pmps
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

                                    <asp:RadioButtonList ID="radlist" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Pmps" Value="Pmps" Selected="True"></asp:ListItem>
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







                <div align="left" style="height: 10px;">
                </div>
                <br />
                <div align="left" style="padding-left: 30px;">
                    <table border="1" cellpadding="5" cellspacing="0" width="50%">
                        <tr>
                            <td style="width: 7%;" class="leftHeading">
                                <asp:Label runat="server" ID="lbl_heading" Text="Profile HS/NPC Code"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                <asp:TextBox ID="txt_hscode1" ReadOnly="true" TabIndex="1" MaxLength="10" Width="100%" onblur="javascript:setsearchpostion(this);" runat="server"></asp:TextBox>
                                <br />
                                <asp:RadioButtonList ID="rdo_option1" name="rdo_option1" runat="server" onClick="javascript:radiochange(this,1);" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">HS Code </asp:ListItem>
                                    <asp:ListItem Value="2">NPC Code </asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 10%;">
                                <asp:TextBox ID="txt_hscode2" ReadOnly="true" onblur="javascript:setsearchpostion(this);" MaxLength="10" Width="100%" runat="server"></asp:TextBox>
                                <br />
                                <asp:RadioButtonList ID="rdo_option2" runat="server" onClick="javascript:radiochange(this,1);" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">HS Code </asp:ListItem>
                                    <asp:ListItem Value="2">NPC Code </asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 10%;">
                                <asp:TextBox ID="txt_hscode3" ReadOnly="true" onblur="javascript:setsearchpostion(this);" MaxLength="10" Width="100%" runat="server"></asp:TextBox>
                                <br />
                                <asp:RadioButtonList ID="rdo_option3" onClick="javascript:radiochange(this,1);" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">HS Code </asp:ListItem>
                                    <asp:ListItem Value="2">NPC Code </asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 10%;">
                                <asp:TextBox ID="txt_hscode4" ReadOnly="true" onblur="javascript:setsearchpostion(this);" MaxLength="10" Width="100%" runat="server"></asp:TextBox><br />
                                <asp:RadioButtonList ID="rdo_option4" runat="server" onClick="javascript:radiochange(this,1);" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">HS Code </asp:ListItem>
                                    <asp:ListItem Value="2">NPC code </asp:ListItem>
                                </asp:RadioButtonList>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="5" align="center">
                                <input type="button" id="btn_searchhscode" onclick="javascript: ShowPopupUpper(this);" value="Search" class="btnCls" />

                            </td>
                        </tr>
                    </table>
                </div>

                <br />
                <div align="left" style="padding-left: 10px; padding-right: 10px;">
                    <asp:Repeater ID="StoreRepeaterBranch" runat="server"
                        OnItemDataBound="StoreRepeaterBranch_ItemDataBound">

                        <HeaderTemplate>
                            <table border="1" cellpadding="0" cellspacing="0" id="thetable">
                                <tr style="width: 700px;">
                                    <th style="width: 50px;" align="center">S.NO/Store Code                                                    </th>

                                    <th style="width: 50px;" align="center">Description/ Stores/ Products/Services<br />
                                        भंडारों/ उत्पादों /सेवा का विवरण
                                    </th>
                                    <th>Ref. of BIS Specification or other Specification<br />
                                        बी.आई.एस. विशिष्ट का सन्दर्भ या अन्य विशिष्टता
                                    </th>
                                    <th style="width: 100px;">Model/ Brand<br />
                                        माडल/ ब्रांड
                                    </th>
                                    <th style="width: 100px;">Limiting Size Capacity/ Rating<br />
                                        सीमित आकार/ क्षमता/ रेटिंग
                                    </th>
                                    <th style="width: 100px;">Monthly Production Capacity per shift<br />
                                        महीने के उत्पादन क्षमता प्रति पारि</div>
                                    </th>
                                    <th style="width: 70px;">No. of shifts factory normally works<br />
                                        पारियों की सं. जो प्राय: कारखाने में चलती है
                                    </th>

                                    <th>Qualitative Capacity
                                    </th>
                                    <th>Quantitative Capacity
                                    </th>                                   
                                      <th>NIC Code
                                    </th>
                                    <th>HS/NPC Code
                                    </th>                                  
                                    <th runat="server" id="idthremarks">
                                         <asp:Label ID="lblRemarks" runat="server" Text="Remarks / टिप्पणियां "></asp:Label>
                                         
                                    </th>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <tr>

                                <td style="width: 100px;" valign="top">

                                    <asp:TextBox ID="txt_serial_no" runat="server" CssClass="txt_bx150" Text='<%# Eval("fGPtbl_CUnit_Store_SNo") %>' Width="20px" OnKeyPress="javascript:return CheckNumericKeyInfo(event.keyCode, event.which)"></asp:TextBox></td>
                                <td style="width: 100px;">
                                    <asp:HiddenField ID="hfStoreid" runat="server" Value='<%# Eval("fGPtbl_CUnit_Store_Id") %>' />
                                    <asp:HiddenField ID="hdnEnterprise" runat="server" Value='<%# Eval("fGPtbl_CUnit_Enterprise") %>' />
                                    <asp:Label ID="lbl_hsstoreid" runat="server" Text='<%# Eval("fGPtbl_CUnit_Store_Id") %>' Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="txt_bx150" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false" Text='<%# Eval("fGPtbl_CUnit_Store_DescriptionStoresProducts") %>'></asp:TextBox></td>
                                <td style="width: 100px;">
                                    <asp:TextBox ID="txtBis" runat="server" CssClass="txt_bx150" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false" Text='<%# Eval("fGPtbl_CUnit_Store_RefOfBISSpecification") %>'></asp:TextBox></td>
                                <td style="width: 100px;">
                                    <asp:TextBox ID="txtModel" runat="server" CssClass="txt_bx150" Width="100px" Height="400px" TextMode="MultiLine" Wrap="false" Text='<%# Eval("fGPtbl_CUnit_Store_ModelBrand") %>'></asp:TextBox></td>
                                <td style="width: 100px;">
                                    <asp:TextBox ID="txtLimiting" runat="server" CssClass="txt_bx150" Width="100px" Height="400px" TextMode="MultiLine" Wrap="false" Text='<%# Eval("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") %>'></asp:TextBox></td>
                                <td style="width: 100px;">
                                    <asp:TextBox ID="txtAnnual" runat="server" CssClass="txt_bx150" Width="100px" Height="400px" TextMode="MultiLine" Wrap="false" Text='<%# Eval("fGPtbl_CUnit_Store_AnnualProductionCapacity") %>'></asp:TextBox></td>
                                <td style="width: 100px;" valign="top">
                                    <asp:TextBox ID="txtShift" runat="server" CssClass="txt_bx150" Width="70px" Text='<%# Eval("fGPtbl_CUnit_Store_NoOfShifts") %>'></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtShift" FilterType="Numbers">
                                    </asp:FilteredTextBoxExtender>
                                </td>

                                <td style="width: 370px;">
                                    <asp:TextBox ID="txtQuality" runat="server" CssClass="txt_bx150" Text='<%# Eval("fGPtbl_CUnit_Store_QualitativeCapacity") %>' Width="370px" TextMode="MultiLine" Wrap="false" Height="400px"></asp:TextBox>
                                    <%-- TextMode="MultiLine" Wrap="false"--%>

                                  <%--  <asp:RequiredFieldValidator ID="regquality" runat="server" ControlToValidate="txtquality"
                                        Display="none" ValidationGroup="1" ErrorMessage=" please fill qualitative capacity !" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="validcallout" runat="server" PopupPosition="bottomleft"
                                        TargetControlID="regquality">
                                    </asp:ValidatorCalloutExtender>--%>

                                </td>
                                <td style="width: 370px;">
                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("fGPtbl_CUnit_Store_QuantitativeCapacity") %>' CssClass="txt_bx150" Width="370px" TextMode="MultiLine" Wrap="false" Height="400px"></asp:TextBox>
                                    <%-- Width="80px" TextMode="MultiLine" Wrap="false"--%>
                                    <asp:RequiredFieldValidator ID="regquantity" runat="server" ControlToValidate="txtQuantity"
                                        Display="none" ErrorMessage="Please Fill Quantitative Capacity !" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="validcalloutquant" runat="server" PopupPosition="BottomLeft"
                                        TargetControlID="regquantity">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                                  <td style="width: 370px;">
                                    <asp:TextBox ID="txtNICcode" runat="server" Text='<%# Eval("fGPtbl_CUnit_Store_NIC") %>' CssClass="txt_bx150" Width="370px" TextMode="MultiLine" Wrap="false" Height="400px"></asp:TextBox>
                               
                                </td>

                                <td valign="top">


                                    <asp:TextBox ID="txt_hscode" ReadOnly="false" runat="server" Width="100px" CssClass="txt_bx150">   <%--CssClass="txt_bx150" --%>
                                    </asp:TextBox>
                                    <br />

                                    <asp:HiddenField ID="hdn_hstextbox" runat="server" />


                                    <asp:RadioButtonList ID="rdooption_repeater" RepeatDirection="Horizontal" runat="server" onClick="javascript:radiochange(this,1);">
                                        <%--onClick ="javascript:radiochange(this,1);"--%>
                                        <asp:ListItem Value="1" Selected="True"> HS  </asp:ListItem>
                                        <asp:ListItem Value="2"> NPC  </asp:ListItem>
                                    </asp:RadioButtonList>

                                    <asp:HiddenField ID="hdn_hsornpc" runat="server" />

                                    <input type="button" id="btn_search_repeater" value="Search" class="btnCls" onclick="javascript: ShowPopup(this);" />
                                    <%----%>

                                    </td>

                                 
                                <td style="width: 370px;">
                                    <asp:TextBox ID="txtRemarks_Manufacturing" runat="server" Text='<%# Eval("fGPtbl_CUnit_Store_ManRemarks") %>' CssClass="txt_bx150" Width="370px" TextMode="MultiLine" Wrap="false" Height="400px"></asp:TextBox>
                                    <%-- Width="80px" TextMode="MultiLine" Wrap="false"--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator141" runat="server" ControlToValidate="txtRemarks_Manufacturing"
                                        Display="none" ErrorMessage="Please Fill Remarks !" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" PopupPosition="BottomLeft"
                                        TargetControlID="RequiredFieldValidator141">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                                
                               
                                
                            </tr>

                        </ItemTemplate>

                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div align="left" style="height: 10px;">
                </div>
                <div align="left" style="padding-left: 10px;" class="header" id="listofdocument" runat="server">
                    List of documents
                </div>
                <div align="left" style="padding-left: 30px;">
                    <asp:CheckBoxList ID="ChkListDoc" runat="server">
                    </asp:CheckBoxList>
                </div>


                <%--         <div align="left" style="height:10px;">
              <asp:Label ID="lbl_message" runat="server" Text=""  CssClass="label_mes"></asp:Label>
             </div>--%>
                <div align="left" style="height: 10px;">
                </div>



                <div align="left" style="padding-left: 20px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="4" ValidationGroup="1"
                        OnClientClick="if(!validate()) { return false;}" CssClass="btnCls" OnClick="btnSubmit_Click1" />
                    &nbsp;&nbsp;
                </div>

            </asp:View>
        </asp:MultiView>

    </div>
    <div>
        <div id="mask">
        </div>


        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="90%"
            Width="50%" Style="z-index: 111; background-color: White; position: fixed; left: 25%; top: 20%; border: outset 2px gray; padding: 5px; display: none">
            <table width="100%" cellpadding="0" style="border: 1px solid black;" cellspacing="0">


                <tr class="menutitle">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">
                        <asp:Label runat="server" ID="lbl_head"></asp:Label>
                        <a id="closebtn" style="color: white; float: right; text-decoration: none" onclick="javascript:HidePopup();" class="btnClose" href="#">X</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>

                <tr>
                    <td align="right">
                        <asp:TextBox runat="server" ID="txt_search" Width="350px"> </asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Button ID="btn_search" CssClass="btnCls" OnClientClick="if(!Bind_Searchdata()) { return false;}" runat="server" Text="Search" />
                    </td>
                </tr>


                <tr>
                    <td colspan="2">&nbsp;&nbsp;&nbsp;
                
                    </td>
                </tr>
                <tr>
                    <td colspan="2">

                        <table id="heading" class="fixed" cellpadding="3" cellspacing="1" width="100%">
                        </table>
                        <table id="gv_result" class="hfixed" cellpadding="3" cellspacing="1" width="100%">
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>


    </div>

</asp:Content>

