<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Admin_Edit_Store.aspx.cs"
    Inherits="appAdministrator_Admin_Edit_Store" Title="Edit store details" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../JScript/jquery.freezeheader.js" type="text/javascript"></script>
    <style type="text/css">
        .editclasstextboxhscode {
            font-family: Arial, Helvetica, sans-serif;
        }

        .content {
            padding-left: 10px;
            padding-right: 10px;
            vertical-align: top;
        }

        .gridPager {
            vertical-align: top;
        }

        .lblstyle {
            white-space: pre;
        }

        .dynRowEvenGrid {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            height: 30px;
            text-align: left;
            vertical-align: top;
            background-color: #DDF1F7;
            color: #000000;
            font-weight: normal;
        }

        .dynRowOddGrid {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            height: 30px;
            text-align: left;
            vertical-align: top;
            background-color: #EFF9FC;
            color: #000000;
            font-weight: normal;
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
    </style>

    <script type="text/javascript" language="javascript">

        //    function GetSelectedRow(lnk) {
        //        var row = lnk.parentNode.parentNode;
        //        var rowIndex = row.rowIndex - 1;
        //        alert("RowIndex: " + rowIndex);
        //        return false;
        //    }

        var chksearch = "";

        function validatefooter() {
            var valid = true;
            //  alert('hi');
            var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
        <%--
        if (chkenterprise == "3") {
            var grid = document.getElementById('<%= gv_storedetails.ClientID %>');
       var flabels = document.getElementById('<%=((RadioButtonList)gv_storedetails.FooterRow.FindControl("rdooption_footer")).ClientID %>');

            var selectedval = $(flabels).find(":checked").val();
            chkenterprise = selectedval;
        }
		--%>

        var grid = document.getElementById('<%= gv_storedetails.ClientID %>');
        var flabels = document.getElementById('<%=((TextBox)gv_storedetails.FooterRow.FindControl("txt_foothscode")).ClientID %>');
            var id = document.getElementById(flabels.id).value.trim();
            if (id != "") {
                if (chkenterprise == "1") {
                    if (parseInt(id.length) > 3 && parseInt(id.length) < 9) {
                        if (parseInt(id.length) == 4 || parseInt(id.length) == 6 || parseInt(id.length) == 8) {
                            document.getElementById(flabels.id).className = "txt_bx150";
                            valid = true;
                        }
                        else {
                            document.getElementById(flabels.id).className = "txt_invalidhscode";
                            valid = false;
                        }
                    }
                    else {
                        document.getElementById(flabels.id).className = "txt_invalidhscode";
                        valid = false;
                    }
                }
                else {
                    if (parseInt(id.length) > 3 && parseInt(id.length) < 9) {
                        if (parseInt(id.length) == 4 || parseInt(id.length) == 6 || parseInt(id.length) == 7 || parseInt(id.length) == 8) {
                            document.getElementById(flabels.id).className = "txt_bx150";
                            valid = true;
                        }
                        else {
                            document.getElementById(flabels.id).className = "txt_invalidhscode";
                            valid = false;
                        }
                    }
                    else {
                        document.getElementById(flabels.id).className = "txt_invalidhscode";
                        valid = false;
                    }
                }
            }

            return valid;

        }


        function validateedit() {
            var valid = true;
            var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
        var grid = document.getElementById('<%= gv_storedetails.ClientID %>');
        var rowindex = $('#<%=hdn_edittemplate.ClientID %>').val();

        var txtid = grid.rows[rowindex].cells[11].children[2].id;


        if (chkenterprise == "3") {
            var txtid = grid.rows[rowindex].cells[11].children[2].id;
           // alert(txtid);
           // var flabels = document.getElementById('<%=((RadioButtonList)gv_storedetails.FooterRow.FindControl("rdooption_footer")).ClientID %>');
                var selectedval = getradioval(txtid);
                chkenterprise = selectedval;
            }

            var flabels = grid.rows[rowindex].cells[11].children[0].id;

            var id = document.getElementById(flabels).value.trim();

            if (id != "") {
                if (chkenterprise == "1") {
                    if (parseInt(id.length) > 3 && parseInt(id.length) < 9) {

                        if (parseInt(id.length) == 4 || parseInt(id.length) == 6 || parseInt(id.length) == 8) {
                            document.getElementById(flabels).className = "txt_bx150";
                            valid = true;
                        }
                        else {

                            document.getElementById(flabels).className = "txt_invalidhscode";
                            valid = false;
                        }
                    }
                    else {
                        document.getElementById(flabels).className = "txt_invalidhscode";
                        valid = false;
                    }
                }
                else {
                    if (parseInt(id.length) > 3 && parseInt(id.length) < 9) {
                        if (parseInt(id.length) == 4 || parseInt(id.length) == 6 || parseInt(id.length) == 7 || parseInt(id.length) == 8) {
                            document.getElementById(flabels).className = "txt_bx150";
                            valid = true;
                        }
                        else {
                            document.getElementById(flabels).className = "txt_invalidhscode";
                            valid = false;
                        }
                    }
                    else {
                        document.getElementById(flabels).className = "txt_invalidhscode";
                        valid = false;
                    }
                }
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

                    if (code1len < 4 || code1len >= 9) {
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

                    if (code2len < 4 || code2len >= 9) {
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
                    alert(code3len);
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
                    if (code4len < 6 || code4len >= 9) {
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

        function ShowPopup(obj) {

            chksearch = obj.id;
            // alert(chksearch);
            var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
        var grid = document.getElementById('<%= gv_storedetails.ClientID %>');
        var flabels = document.getElementById('<%=((RadioButtonList)gv_storedetails.FooterRow.FindControl("rdooption_footer")).ClientID %>');
        if (chksearch.indexOf("btn_search_footer") > -1) {
            if (chkenterprise == "3") {
                var selectedval = $(flabels).find(":checked").val();
                chkenterprise = selectedval;
            }
        }


        else if (chksearch.indexOf("btn_search_edit") > -1) {
            if (chkenterprise == "3") {
                //  var grid = document.getElementById('<%= gv_storedetails.ClientID %>');
                //                var flabels = document.getElementById('<%=((RadioButtonList)gv_storedetails.FooterRow.FindControl("rdooption_footer")).ClientID %>');
                var grid = document.getElementById('<%= gv_storedetails.ClientID %>');
                var rowindex = $('#<%=hdn_edittemplate.ClientID %>').val();
                var txtid = grid.rows[rowindex].cells[11].children[2].id;
                //   alert(txtid);
                var selectedval = getradioval(txtid);
                chkenterprise = selectedval;
            }
        }

        if (chkenterprise == "1") {
            $('#<%=lbl_head.ClientID%>').html("Seach HS CODE FOR PARTICULAR STORE");
        }
        else if (chkenterprise == "2") {
            $('#<%=lbl_head.ClientID%>').html("Seach NPC CODE FOR PARTICULAR STORE");
        }



        $('#mask').show();
        $("#heading").empty();
        $("#gv_result").empty();
        $('#<%=pnlpopup.ClientID %>').show();
        }

        function CheckSingleCheckbox(obj) {
            var id = obj.id;
            var res = id.split(",");

            var clickedradiooptions = $('#<%=hdn_holdtxtval.ClientID %>').val();

        //   $(clickedradiooptions).val(res[1]);
        if (clickedradiooptions != "") {
            document.getElementById(clickedradiooptions).value = res[1];
        }
        else if (chksearch !== "") {

            if (chksearch.indexOf("btn_search_footer") > -1) {
                var grid = document.getElementById('<%= gv_storedetails.ClientID %>');
                    var flabels = document.getElementById('<%=((TextBox)gv_storedetails.FooterRow.FindControl("txt_foothscode")).ClientID %>');
                    document.getElementById(flabels.id).value = res[1];
                    var hiddenfiled = document.getElementById('<%=((HiddenField)gv_storedetails.FooterRow.FindControl("hid_foothscodeId")).ClientID %>');
                document.getElementById(hiddenfiled.id).value = res[0];
            }
            if (chksearch.indexOf("btn_search_edit") > -1) {
                var grid = document.getElementById('<%= gv_storedetails.ClientID %>');
                var rowindex = $('#<%=hdn_edittemplate.ClientID %>').val();
                var txtid = grid.rows[rowindex].cells[11].children[0].id;
                // alert(txtid);
                $("#" + txtid).val(res[1].trim());
                var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
                var hiddenhsid;
                if (chkenterprise != "3") {
                    hiddenhsid = grid.rows[rowindex].cells[11].children[3].id;
                }
                else {
                    hiddenhsid = grid.rows[rowindex].cells[11].children[4].id;
                }

                //   alert(hiddenhsid);
                $("#" + hiddenhsid).val(res[0].trim());
//                var flabels = document.getElementsByClassName('editclasstextboxhscode');
//                alert(flabels);
//                document.getElementById(flabels.id).value = res[1];
//                var hiddenfiled = document.getElementById('<%=((HiddenField)gv_storedetails.FooterRow.FindControl("hid_foothscodeId")).ClientID %>');
                    //                document.getElementById(hiddenfiled.id).value = res[0];
                }
            }

            //   var inputs = $('#gv_result').find('input[type="checkbox"]') 
            //    for (var i = 0; i < inputs.length; i++) {
            // if (inputs[i].type == "checkbox") {
            //                if (obj.checked && inputs[i] != obj && inputs[i].checked) {
            //                    inputs[i].checked = false;
            //                }
            //                else {
            //                    //inputs[i].checked = true;
            //                }
            //  }
            //   }
            HidePopup();

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



        var storearray = new Array();


        function Bind_Searchdata() {



            var clickedradiooptions = $('#<%=hdn_holdtxtval.ClientID %>').val();
        //   alert(clickedradiooptions);


        var txtsearch = $('#<%=txt_search.ClientID %>').val();
        if (txtsearch == "") {
            alert('Please enter text to serach HS/NPC Code');
            return false;
        }
        $("#gv_result").append(" <tr><td align ='center' valign ='middle'><img src ='../Images/ajax-loader.gif' /></td></tr>");




        var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();
        if (chkenterprise == "0") {
            chkenterprise = "1";
        }
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
        else if (chksearch !== "") {
            //  alert(chksearch);
            if (chksearch.indexOf("btn_search_footer") > -1) {
                if (chkenterprise == "3") {
                    var grid = document.getElementById('<%= gv_storedetails.ClientID %>');
                var flabels = document.getElementById('<%=((RadioButtonList)gv_storedetails.FooterRow.FindControl("rdooption_footer")).ClientID %>');
                var selectedval = $(flabels).find(":checked").val();
                chkenterprise = selectedval;
            }
        }
        else if (chksearch.indexOf("btn_search_edit") > -1) {
            if (chkenterprise == "3") {
              //  var grid = document.getElementById('<%= gv_storedetails.ClientID %>');
                //                var flabels = document.getElementById('<%=((RadioButtonList)gv_storedetails.FooterRow.FindControl("rdooption_footer")).ClientID %>');
                var grid = document.getElementById('<%= gv_storedetails.ClientID %>');
                var rowindex = $('#<%=hdn_edittemplate.ClientID %>').val();
                    var txtid = grid.rows[rowindex].cells[11].children[2].id;
                    var selectedval = getradioval(txtid);
                    chkenterprise = selectedval;
                }
            }
        }
        var hcode = "HS Code";
        if (chkenterprise == "1") {
            hcode = "HS Code";
        }
        else {
            hcode = "NPC Code";
        }
        //   alert(chkenterprise);

        $.ajax({
            type: "POST",
            url: "Admin_Edit_Store.aspx/Bind_Searchgrid",
            data: '{searchitem:"' + txtsearch + '",codetype:"' + chkenterprise + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#heading").empty();
                $("#gv_result").empty();

                //    $("#heading").append("<tr ><td ><img src ='../Images/blank.png' class='imgdisplay' height '0px' width ='80px' /> </td><td  ><img src ='../Images/blank.png' class='imgdisplay' height ='0px' width ='80px' /></td><td></td></tr>");
                $("#heading").append("<tr class ='dynRowHeader'><td >Options</td><td  >" + hcode + "</td><td>Product Details</td></tr>");
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
            error: function (response) {

                alert(response.status + ' ' + response.statusText);
            }
        });
        }


        function setsearchpostion(obj) {
            $('#<%=hdn_holdtxtval.ClientID %>').val(obj.id)

        }

        function ShowPopupUpper(obj) {
            var chkenterprise = $('#<%=hdn_enterprise.ClientID %>').val();

        var chkonblur = $('#<%=hdn_holdtxtval.ClientID %>').val();
        //   alert(chkonblur);
        if (chkonblur == "") {
            alert('please put cursor on TextBox ');
            return false;
        }

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

        function HidePopup() {
            //        var GVMaintainReceiptMaster = document.getElementById('thetable');
            //        var rows = document.getElementById('thetable').getElementsByTagName('tbody')[0].getElementsByTagName('tr');
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
        $('#<%=hdn_holdtxtval.ClientID %>').val("");

        }
        $(".btnClose").live('click', function () {
            HidePopup();


        });


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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField runat="server" ID="hdn_enterprise" />
    <asp:HiddenField ID="hdn_holdtxtval" runat="server" />
    <asp:HiddenField ID="hdn_storeradio" runat="server" Value="1" />
    <asp:HiddenField ID="hdn_edittemplate" runat="server" />
    <div align="left">
        <asp:Label ID="lblstore" runat="server" Text="Edit Store Details" CssClass="headerAll"></asp:Label>
    </div>
    <br />

    <div style="padding-left: 30px;">
        <asp:Repeater ID="repeaterPmps" runat="server"
            OnItemDataBound="repeaterPmps_ItemDataBound">
            <HeaderTemplate>
                <table border="1" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>Factory location
                        </th>
                        <th>Pmps
                        </th>

                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:HiddenField ID="hfUnitidpmps" runat="server" Value='<%# Eval("fGPtbl_CUnit_Id") %>' />
                        <asp:Literal ID="ltlFactory" runat="server" Text='<%#Eval("fGPtbl_CUnit_1B_Factory_Address")%>'></asp:Literal>
                    </td>
                    <td>
                        <asp:HiddenField ID="hfRdlPmps" runat="server" Value='<%#Eval("pmps") %>' />
                        <asp:RadioButtonList ID="radlist" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Pmps" Value="pmps"></asp:ListItem>
                            <asp:ListItem Text="Pm" Value="pm"></asp:ListItem>
                            <asp:ListItem Text="PA" Value="PA"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RFValidator" runat="server" ErrorMessage="Please select PMPS/PS"
                            Display="Dynamic" SetFocusOnError="true" ControlToValidate="radlist" ValidationGroup="1"></asp:RequiredFieldValidator>
                    </td>

                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>

        </asp:Repeater>
        <%--<div>
                    <asp:Button ID="btncreatexml" runat="server" Text="Create" 
                        onclick="btncreatexml_Click" />
                </div>--%>
    </div>

    <div align="left" style="padding-left: 10px;">
        <table border="1" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="leftHeading">
                    <asp:Label runat="server" ID="lbl_heading"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_hscode1" Width="150px" ReadOnly="true" TabIndex="1" MaxLength="10" onblur="javascript:setsearchpostion(this);" runat="server"></asp:TextBox>
                    <br />
                    <asp:RadioButtonList ID="rdo_option1" name="rdo_option1" runat="server" onClick="javascript:radiochange(this,1);" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">HS Code </asp:ListItem>
                        <asp:ListItem Value="2">NPC Code </asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:TextBox ID="txt_hscode2" ReadOnly="true" onblur="javascript:setsearchpostion(this);" MaxLength="10" Width="150px" runat="server"></asp:TextBox>
                    <br />
                    <asp:RadioButtonList ID="rdo_option2" runat="server" onClick="javascript:radiochange(this,1);" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">HS Code </asp:ListItem>
                        <asp:ListItem Value="2">NPC Code </asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:TextBox ID="txt_hscode3" ReadOnly="true" onblur="javascript:setsearchpostion(this);" MaxLength="10" Width="150px" runat="server"></asp:TextBox>
                    <br />
                    <asp:RadioButtonList ID="rdo_option3" onClick="javascript:radiochange(this,1);" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">HS Code </asp:ListItem>
                        <asp:ListItem Value="2">NPC Code </asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:TextBox ID="txt_hscode4" ReadOnly="true" onblur="javascript:setsearchpostion(this);" MaxLength="10" Width="150px" runat="server"></asp:TextBox><br />
                    <asp:RadioButtonList ID="rdo_option4" runat="server" onClick="javascript:radiochange(this,1);" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">HS Code </asp:ListItem>
                        <asp:ListItem Value="2">NPC code </asp:ListItem>
                    </asp:RadioButtonList>
                </td>

            </tr>
            <tr>
                <td colspan="5" align="center">
                    <input type="button" id="btn_searchhscode" onclick="javascript:ShowPopupUpper(this);" value="Search" class="btnCls" />

                </td>
            </tr>
        </table>
    </div>

    <div style="height: 10px;"></div>

    <div align="left" style="color: Red; padding-left: 30px;">
        <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
    </div>

    <div align="left" style="padding-left: 30px;">
        <asp:Button ID="btnpmpssubmit" runat="server" Text="Submit Header" CssClass="btnCls"
            OnClientClick="if(!validateupper()) { return false;}" OnClick="btnpmpssubmit_Click" ValidationGroup="1" Width="110px" />
    </div>
    <br />
    <br />







    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnl_store" Width="100%" class="content">


                <asp:HiddenField runat="server" ID="hndamendmentid" />
                <%--onrowupdating="gv_storedetails_RowUpdating" --%>

                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <div align="right" style="color: Red; width: 100%">
                                <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
                                        <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                        <span style="color: red; font-size: medium">Please wait while we are processing your request <%--Please wait, while your request is being processed--%></span>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>

                        </td>
                    </tr>
                    <tr>
                        <td>

                            <%--<asp:TemplateField HeaderText="Qualitative Capacity">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_qualitativecapcity" runat="server" 
                            Text='<%# HttpUtility.HtmlDecode(Eval("fGPtbl_CUnit_Store_QualitativeCapacity").ToString()) %>' Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_qualitativecapcity" runat="server" CssClass="lblstyle"
                         Text='<%# HttpUtility.HtmlDecode(Eval("fGPtbl_CUnit_Store_QualitativeCapacity").ToString()) %>' ></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                      <asp:TextBox runat ="server" ID = "txt_footqualitativecapcity" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantitative Capacity">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_quantitativecapacity" runat="server" 
                     Text='<%# HttpUtility.HtmlDecode(Eval("fGPtbl_CUnit_Store_QuantitativeCapacity").ToString()) %>'        Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_quantitativecapacity" runat="server" CssClass="lblstyle"
                             Text='<%# HttpUtility.HtmlDecode(Eval("fGPtbl_CUnit_Store_QuantitativeCapacity").ToString()) %>' ></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                      <asp:TextBox runat ="server" ID = "txt_footquantitativecapacity" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                            --%>

                            <asp:GridView ID="gv_storedetails" runat="server" AutoGenerateColumns="False"
                                Width="100%" BorderWidth="0px" CellPadding="5" CellSpacing="3" DataKeyNames="fGPtbl_CUnit_Store_Id"
                                AllowPaging="false"
                                AllowSorting="True" ShowFooter="True"
                                OnRowCommand="gv_storedetails_RowCommand"
                                OnRowDataBound="gv_storedetails_RowDataBound"
                                OnRowEditing="gv_storedetails_RowEditing"
                                OnRowDeleting="gv_storedetails_RowDeleting"
                                OnRowUpdating="gv_storedetails_RowUpdating"
                                OnRowCancelingEdit="gv_storedetails_RowCancelingEdit">
                                <HeaderStyle CssClass="dynRowHeader" />
                                <RowStyle CssClass="dynRowEvenGrid" />
                                <AlternatingRowStyle CssClass="dynRowOddGrid" />
                                <PagerStyle CssClass="gridPager" />                             
                                <Columns>
                                    <asp:TemplateField HeaderText="Sequence Code" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbl_Seqno" Width="50px" Text='<%# Bind("fGPtbl_CUnit_Store_Sequence") %>'></asp:Label>
                                            <asp:HiddenField ID="hdnEnterprise" runat="server" Value='<%# Eval("fGPtbl_CUnit_Enterprise") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_Seqno" runat="server" Width="50px" MaxLength="3" Text='<%# Bind("fGPtbl_CUnit_Store_Sequence") %>' OnKeyPress="javascript:return CheckNumericKeyInfo(event.keyCode, event.which)"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_footSseqno" runat="server" Width="50" MaxLength="3" OnKeyPress="javascript:return CheckNumericKeyInfo(event.keyCode, event.which)"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.no. /Store Code" ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbl_Sno" Width="50px" Text='<%# Bind("fGPtbl_CUnit_Store_SNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_Sno" runat="server" Width="50px" MaxLength="3" Text='<%# Bind("fGPtbl_CUnit_Store_SNo") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_footSno" runat="server" MaxLength="3" Width="50"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location" HeaderStyle-Width="150">
                                        <EditItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddl_editlocation" Width="100%"></asp:DropDownList>
                                            <asp:HiddenField ID="hdnUnitid" runat="server" Value='<%# Bind("fGPtbl_CUnit_Store_UnitId") %>' />
                                            <asp:RequiredFieldValidator runat="server" ID="req_location_edit" ControlToValidate="ddl_editlocation" InitialValue="0" Display="Dynamic" ErrorMessage="Please select Location" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbl_location" Width="150" Text='<%# Bind("fGPtbl_CUnit_1B_Factory_Address") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList runat="server" DataValueField="fGPtbl_CUnit_Id" DataTextField="fAddress" ID="ddl_footlocation" Width="100%" CssClass="clsDDL" OnSelectedIndexChanged="ddl_footlocation_change"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="req_location" ControlToValidate="ddl_footlocation" InitialValue="0" Display="Dynamic" ErrorMessage="Please select Location" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Description of Stores/ Products/ Services">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_product" runat="server"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_DescriptionStoresProducts") %>' Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="req_product_edit" ControlToValidate="txt_product" Display="Dynamic" ErrorMessage="Please enter product" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_product" runat="server" CssClass="lblstyle"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_DescriptionStoresProducts") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txt_footproduct" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="req_product" ControlToValidate="txt_footproduct" Display="Dynamic" ErrorMessage="Please enter product" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref. of BIS Specification or other specification">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_refbisspecification" runat="server"
                                                Text='<%# System.Web.HttpUtility.HtmlDecode((string)Eval("fGPtbl_CUnit_Store_RefOfBISSpecification")) %>' Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_refbisspecification" runat="server" CssClass="lblstyle"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_RefOfBISSpecification") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txt_footrefbisspecification" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Model/ Brand">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_model" runat="server"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_ModelBrand") %>' Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_model" runat="server" CssClass="lblstyle"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_ModelBrand") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txt_footmodel" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Limiting Size Capacity/ Rating" ItemStyle-VerticalAlign="Top">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_limitingsize" runat="server"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") %>' Width="180px" Height="400px" TextMode="MultiLine" Wrap="false" OnKeyPress="javascript:return CheckNumericKeyInfo(event.keyCode, event.which)" MaxLength="4"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_limitingsize" runat="server"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txt_footlimitingsize" OnKeyPress="javascript:return CheckNumericKeyInfo(event.keyCode, event.which)" MaxLength="4"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Monthly Production Capacity per shift">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_annualcapacity" runat="server"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_AnnualProductionCapacity") %>' OnKeyPress="javascript:return CheckNumericKeyInfo(event.keyCode, event.which)" MaxLength="4"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_annualcapacity" runat="server"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_AnnualProductionCapacity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txt_footannualcapacity" OnKeyPress="javascript:return CheckNumericKeyInfo(event.keyCode, event.which)" MaxLength="4"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" No. of shifts factory normally works">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_noofshifts" runat="server"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_NoOfShifts") %>' OnKeyPress="javascript:return CheckNumericKeyInfo(event.keyCode, event.which)" MaxLength="4"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_noofshifts" runat="server"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_NoOfShifts") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txt_footnoofshifts" OnKeyPress="javascript:return CheckNumericKeyInfo(event.keyCode, event.which)" MaxLength="4"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qualitative Capacity">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_qualitativecapcity" runat="server"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_QualitativeCapacity") %>' Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_qualitativecapcity" runat="server" CssClass="lblstyle"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_QualitativeCapacity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txt_footqualitativecapcity" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantitative Capacity">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_quantitativecapacity" runat="server" Text='<%# Bind("fGPtbl_CUnit_Store_QuantitativeCapacity") %>'
                                                Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_quantitativecapacity" runat="server" CssClass="lblstyle"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_QuantitativeCapacity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txt_footquantitativecapacity" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HS / NPC Code">
                                        <EditItemTemplate>
                                            <%--  <asp:DropDownList runat ="server" ID ="ddl_hscode" Width ="100px"></asp:DropDownList>--%>
                                            <asp:TextBox runat="server" ReadOnly="false" Text='<%# Bind("fGPtbl_CUnit_Store_HsCode") %>' ID="txt_edithscode" CssClass="editclasstextboxhscode"></asp:TextBox>
                                            <br />
                                            <asp:RadioButtonList ID="rdooption_edit" RepeatDirection="Horizontal" runat="server">
                                            </asp:RadioButtonList>
                                            <br />
                                            <asp:HiddenField ID="hid_editscodeId" runat="server" />
                                            <asp:HiddenField ID="hdnfHScode" runat="server" Value='<%# Bind("fGPtbl_IS_HS_or_NPC") %>' />
                                            <input type="button" id="btn_search_edit" value="Seach" onclick="javascript:ShowPopup(this);" class="btnCls" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbl_hscode" Text='<%# Bind("fGPtbl_CUnit_Store_HsCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_foothscode" runat="server" ReadOnly="false"></asp:TextBox>
                                            <br />
                                            <asp:HiddenField ID="hid_foothscodeId" runat="server" />
                                            <asp:RadioButtonList ID="rdooption_footer" RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem Value="1" Selected="True"> HS  </asp:ListItem>
                                                <asp:ListItem Value="2"> NPC  </asp:ListItem>
                                            </asp:RadioButtonList>
                                            <input type="button" id="btn_search_footer" value="Seach" onclick="javascript:ShowPopup(this);" class="btnCls" />
                                            <%-- <asp:DropDownList ID ="ddl_foothscode" runat ="server"  DataTextField = "fGPtbl_HS_HScode" DataValueField = "fGPtbl_HS_HScode" Width ="100px"></asp:DropDownList>--%>
                                            <%--<asp:LinkButton ID="lnkViewTimeSlots" runat="server" Text="Edit" ForeColor="Blue"  OnClientClick="return GetSelectedRow(this); javascript:shouldsubmit=true;" CausesValidation="false" Style="padding: 0px; margin: 0px;"></asp:LinkButton>--%>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NIC Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditNIC" runat="server" Text='<%# Bind("fGPtbl_CUnit_Store_NIC") %>'
                                                Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_NIC" runat="server" CssClass="lblstyle" Text='<%# Bind("fGPtbl_Cunit_Store_NIC") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txt_footNIC" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_RemarksEdit" runat="server" Text='<%# Bind("fGPtbl_CUnit_Store_ManRemarks") %>'
                                                Width="180px" Height="400px" TextMode="MultiLine" Wrap="false" CausesValidation="true"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator runat="server" ID="req_RemarksEdit" ControlToValidate="txt_RemarksEdit" Display="Dynamic" ErrorMessage="Please enter remarks" ValidationGroup="2"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegExpRemarksEdit" runat="server"
                                                ErrorMessage="Remarks must be between 10 to 100 characters"
                                                ControlToValidate="txt_RemarksEdit"
                                                ValidationExpression="^[a-zA-Z0-9'@&#.\s]{10,100}$" />--%>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Remarks" runat="server" CssClass="lblstyle"
                                                Text='<%# Bind("fGPtbl_CUnit_Store_ManRemarks") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txt_Remarks" Width="180px" Height="400px" TextMode="MultiLine" Wrap="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="req_txt_Remarks" ControlToValidate="txt_Remarks" Display="Dynamic" ErrorMessage="Please enter remarks" ValidationGroup="2"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegExpRemarksEdit" runat="server"
                                                ErrorMessage="Remarks must be between 10 to 100 characters"
                                                ControlToValidate="txt_Remarks"
                                                ValidationExpression="^[a-zA-Z0-9'@&#.\s]{10,100}$" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Options">
                                        <EditItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnk_update" CausesValidation="false" OnClientClick="if(!validateedit()) { return false;}" Text="Update" CommandName="Update" ValidationGroup="2"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnk_cancel" Text="Cancel" CommandName="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CausesValidation="false" ID="lnk_edit" Text="Edit" CommandName="Edit"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnk_delete" Text="Delete" OnClientClick="return confirm('Are you sureyou want to delete this record?');" CommandName="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton runat="server" ID="lnk_edit" Text="INSERT" OnClientClick="if(!validatefooter()) { return false;}" CommandArgument="INSERT" ValidationGroup="2"></asp:LinkButton>
                                            <%--<asp:Button runat ="server" ID ="btn_addnew" UseSubmitBehavior ="false" CommandName="AddNew" Text ="Add" />--%>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <div id="mask">
            </div>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="70%"
                Width="50%" Style="z-index: 111; background-color: White; position: fixed; left: 25%; top: 20%; border: outset 2px gray; padding: 5px; display: none">
                <table width="100%" cellpadding="0" style="border: 1px solid black;" cellspacing="0">                  
                <tr class="menutitle">
                    <td colspan="2" style="color: White; font-weight: bold; font-size: 1.2em; padding: 3px"
                        align="center">
                        <asp:Label runat="server" ID="lbl_head"></asp:Label>
                        <a id="closebtn" style="color: white; float: right; text-decoration: none" class="btnClose" href="#">X</a>
                    </td>
                </tr>
                    <tr>
                        <td colspan="2">
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
                        <td colspan="2">                
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

