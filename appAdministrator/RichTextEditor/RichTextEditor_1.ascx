<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RichTextEditor_1.ascx.vb" Inherits="RichTextEditor_RichTextEditor_1" %>

<script language="javascript" type="text/javascript" src="RichTextEditor/EditorScripts/GlobalVar_1.js"></script>

<script language="javascript" type="text/javascript" src="RichTextEditor/EditorScripts/TagEnableDisable_1.js"></script>
<script language="javascript" type="text/javascript" src="RichTextEditor/EditorScripts/ImageScript_1.js"></script>
<script language="javascript" type="text/javascript" src="RichTextEditor/EditorScripts/HtmlInsert_1.js"></script>
<link rel="Stylesheet" type="text/css" href="RichTextEditor/EditorCSS/StyleSheet_1.css" />

<%------ Function to call on Body onload ------%>
<script language="javascript" type="text/javascript">

window.onload = function()
{
    //alert("window.onload = function()");
    funBodyLoad_1();
}
/**/
</script>

<table class="ClsMain_1" border="1" cellpadding="0" cellspacing="0" style="background-color:#EEEEEE;">
    <tr>
        <td>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr class="ClsTR_1" id="trCotrolRowFirst_1" >
                    <td class="ClsTR_1">
                        <table id="tblFirstRow_1" border="0" cellpadding="0" cellspacing="0" style="width:100%; background-image:url(RichTextEditor/EditorImages/background_silver.jpg); background-repeat:repeat-x;">
                            <tr class="ClsTR_1" style="height:2px;"><td></td></tr>
                            <tr class="ClsTR_1" style="height:22px; background-image:url(RichTextEditor/EditorImages/background_silver.jpg); background-repeat:repeat-x;">
                                <td id="td1_1" align="left" valign="top">
                                    <img src="RichTextEditor/EditorImages/seperator2.gif" alt="" />
                                </td>
                                <td id="td2_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td2_1')" onmouseout="funTDBackgroundMouseOut_1('td2_1')">
                                    <img id="ImgLeft_1" src="RichTextEditor/EditorImages/justifyleft.gif" alt="Left Align"
                                     onclick="funFontStyle_1('JustifyLeft')"
                                     onmouseover="funChangeImageMouseOver_1('ImgLeft_1','justify_left_on.gif','Left Align')"
                                     onmouseout="funChangeImageMouseOut_1('ImgLeft_1','justifyleft.gif','Left Align')" class="ClsImage_1"
                                    />
                                </td>
                                <td id="td3_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td3_1')" onmouseout="funTDBackgroundMouseOut_1('td3_1')">
                                    <img id="ImgCenter_1" src="RichTextEditor/EditorImages/justifycenter.gif" alt="Center Align"
                                     onclick="funFontStyle_1('JustifyCenter')"
                                     onmouseover="funChangeImageMouseOver_1('ImgCenter_1','justify_center_on.gif','Center Align')"
                                     onmouseout="funChangeImageMouseOut_1('ImgCenter_1','justifycenter.gif','Center Align')" class="ClsImage_1"
                                    />
                                </td>
                                <td id="td4_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td4_1')" onmouseout="funTDBackgroundMouseOut_1('td4_1')">
                                    <img id="ImgRight_1" src="RichTextEditor/EditorImages/justifyright.gif" alt="Right Align"
                                     onclick="funFontStyle_1('JustifyRight')"
                                     onmouseover="funChangeImageMouseOver_1('ImgRight_1','justify_right_on.gif','Right Align')"
                                     onmouseout="funChangeImageMouseOut_1('ImgRight_1','justifyright.gif','Right Align')" class="ClsImage_1"
                                    />
                                </td>
                                <td id="td5_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td5_1')" onmouseout="funTDBackgroundMouseOut_1('td5_1')">
                                    <img id="ImgJustify_1" src="RichTextEditor/EditorImages/justifyjustify.gif" alt="Justify"
                                     onclick="funFontStyle_1('JustifyFull')"
                                     onmouseover="funChangeImageMouseOver_1('ImgJustify_1','justify_justify_on.gif','Justify')"
                                     onmouseout="funChangeImageMouseOut_1('ImgJustify_1','justifyjustify.gif','Justify')" class="ClsImage_1"
                                    />
                                </td>
                                <%--Start:::Saperotor--%>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img id="Img16_1" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <%--End:::Saperotor--%>
                                <td id="td6_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td6_1')" onmouseout="funTDBackgroundMouseOut_1('td6_1')">
                                    <img id="ImgListOrdered_1" src="RichTextEditor/EditorImages/list_ordered.gif" alt="Ordered List" class="ClsImage_1"
                                     onclick="funList_1('InsertOrderedList')"
                                     onmouseover="funChangeImageMouseOver_1('ImgListOrdered_1','list_ordered_on.gif','Ordered List')"
                                     onmouseout="funChangeImageMouseOut_1('ImgListOrdered_1','list_ordered.gif','Ordered List')"
                                    />
                                </td>
                                <td id="td7_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td7_1')" onmouseout="funTDBackgroundMouseOut_1('td7_1')">
                                    <img id="ImgListUnordered_1" src="RichTextEditor/EditorImages/list_unordered.gif" alt="Unordered List"
                                     onclick="funList_1('InsertUnOrderedList')"
                                     onmouseover="funChangeImageMouseOver_1('ImgListUnordered_1','list_unordered_on.gif','Unordered List')"
                                     onmouseout="funChangeImageMouseOut_1('ImgListUnordered_1','list_unordered.gif','Unordered List')"
                                    />
                                </td>
                                <td id="td8_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td8_1')" onmouseout="funTDBackgroundMouseOut_1('td8_1')">
                                    <img id="ImgIndentLeft_1" src="RichTextEditor/EditorImages/indent_left.gif" alt="Indent Left" class="ClsImage_1"
                                     onclick="funFontStyle_1('Outdent')"
                                     onmouseover="funChangeImageMouseOver_1('ImgIndentLeft_1','indent_left_on.gif','Indent Left')"
                                     onmouseout="funChangeImageMouseOut_1('ImgIndentLeft_1','indent_left.gif','Indent Left')"
                                    />
                                </td>
                                <td id="td9_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td9_1')" onmouseout="funTDBackgroundMouseOut_1('td9_1')">
                                    <img id="ImgIndentRight_1" src="RichTextEditor/EditorImages/indent_right.gif" alt="Indent Right" class="ClsImage_1"
                                     onclick="funFontStyle_1('Indent')"
                                     onmouseover="funChangeImageMouseOver_1('ImgIndentRight_1','indent_right_on.gif','Indent Right')"
                                     onmouseout="funChangeImageMouseOut_1('ImgIndentRight_1','indent_right.gif','Indent Right')"
                                    />
                                </td>
                                <%--Start:::Saperotor--%>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img id="Img17_1" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <%--End:::Saperotor--%>
                                <td id="td10_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td10_1')" onmouseout="funTDBackgroundMouseOut_1('td10_1')">
                                    <img id="ImgColorFore_1" src="RichTextEditor/EditorImages/forecolor.gif" alt="Fore Color"
                                     onclick="funColor_1('ForeColor')"
                                     onmouseover="funChangeImageMouseOver_1('ImgColorFore_1','forecolor_on.gif','Fore Color')"
                                     onmouseout="funChangeImageMouseOut_1('ImgColorFore_1','forecolor.gif','Fore Color')" class="ClsImage_1"
                                    />
                                </td>
                                <td id="td11_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td11_1')" onmouseout="funTDBackgroundMouseOut_1('td11_1')">
                                    <img id="ImgColorBack_1" src="RichTextEditor/EditorImages/backcolor.gif" alt="Background Color"
                                     onclick="funColor_1('BackColor')"
                                     onmouseover="funChangeImageMouseOver_1('ImgColorBack_1','backcolor_on.gif','Background Color')"
                                     onmouseout="funChangeImageMouseOut_1('ImgColorBack_1','backcolor.gif','Background Color')" class="ClsImage_1"
                                    />
                                </td>
                                <%--Start:::Saperotor--%>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img id="Img3_1" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <%--End:::Saperotor--%>
                                <td id="td12_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td12_1')" onmouseout="funTDBackgroundMouseOut_1('td12_1')">
                                    <img id="ImgBold_1" src="RichTextEditor/EditorImages/bold.gif" alt="Font Bold"
                                     onclick="funFontStyle_1('bold')"
                                     onmouseover="funChangeImageMouseOver_1('ImgBold_1','bold_on.gif','Font Bold')"
                                     onmouseout="funChangeImageMouseOut_1('ImgBold_1','bold.gif','Font Bold')" class="ClsImage_1"
                                    />
                                </td>
                                <td id="td13_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td13_1')" onmouseout="funTDBackgroundMouseOut_1('td13_1')">
                                    <img id="ImgItalic_1" src="RichTextEditor/EditorImages/italic.gif" alt="Font Italic"
                                     onclick="funFontStyle_1('Italic')"
                                     onmouseover="funChangeImageMouseOver_1('ImgItalic_1','italics_on.gif','Font Italic')"
                                     onmouseout="funChangeImageMouseOut_1('ImgItalic_1','italic.gif','Font Italic')" class="ClsImage_1"
                                    />
                                </td>
                                <td id="td14_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td14_1')" onmouseout="funTDBackgroundMouseOut_1('td14_1')">
                                    <img id="ImgUnderline_1" src="RichTextEditor/EditorImages/Underline.gif" alt="Font Underline"
                                     onclick="funFontStyle_1('Underline')"
                                     onmouseover="funChangeImageMouseOver_1('ImgUnderline_1','underline_on.gif','Font Underline')"
                                     onmouseout="funChangeImageMouseOut_1('ImgUnderline_1','Underline.gif','Font Underline')" class="ClsImage_1"
                                    />
                                </td>
                                <td id="td15_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td15_1')" onmouseout="funTDBackgroundMouseOut_1('td15_1')">
                                    <img id="ImgStrike_1" src="RichTextEditor/EditorImages/strikethrough.gif" alt="Font Strike" class="ClsImage_1"
                                     onclick="funFontStyle_1('strikethrough')"
                                     onmouseover="funChangeImageMouseOver_1('ImgStrike_1','strikethrough_on.gif','Font Strike')"
                                     onmouseout="funChangeImageMouseOut_1('ImgStrike_1','strikethrough.gif','Font Strike')"
                                    />
                                </td>
                                <%--Start:::Saperotor--%>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img id="Img22_1" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <%--End:::Saperotor--%>
                                <td id="td16_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td16_1')" onmouseout="funTDBackgroundMouseOut_1('td16_1')">
                                    <img id="ImgSuperScript_1" src="RichTextEditor/EditorImages/superscript.gif" alt="Super Script" class="ClsImage_1"
                                     onclick="funFontStyle_1('superscript')"
                                     onmouseover="funChangeImageMouseOver_1('ImgSuperScript_1','superscript_on.gif','Super Script')"
                                     onmouseout="funChangeImageMouseOut_1('ImgSuperScript_1','superscript.gif','Super Script')"
                                    />
                                </td>
                                <td id="td17_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td17_1')" onmouseout="funTDBackgroundMouseOut_1('td17_1')">
                                    <img id="ImgSubScript_1" src="RichTextEditor/EditorImages/subscript.gif" alt="Sub Script" class="ClsImage_1"
                                     onclick="funFontStyle_1('subscript')"
                                     onmouseover="funChangeImageMouseOver_1('ImgSubScript_1','subscript_on.gif','Sub Script')"
                                     onmouseout="funChangeImageMouseOut_1('ImgSubScript_1','subscript.gif','Sub Script')"
                                    />
                                </td>
                                <%--Start:::Saperotor--%>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img id="Img7_1" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <%--End:::Saperotor--%>                            
                                <td id="td18_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td18_1')" onmouseout="funTDBackgroundMouseOut_1('td18_1')">
                                    <select id="ddlFontName_1" class="bodytext_1" onchange="funFontName_1('FontName')" style="width:80px;">
                                        <option selected="selected" value="0">Select Font</option>
                                        <option value="Arial">Arial</option>
                                        <option value="Verdana">Verdana</option>
                                        <option value="Courier">Courier</option>
                                        <option value="Times new roman">Times</option>
                                        <option value="Tahoma">Tahoma</option>
                                        <option value="Comic Sans MS">Comic Sans</option>
                                    </select>
                                </td>         
                                <%--Start:::Saperotor--%>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img id="Img10" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <%--End:::Saperotor--%>
                                <td id="td19_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td19_1')" onmouseout="funTDBackgroundMouseOut_1('td19_1')">
                                    <select id="ddlFontSize_1" class="bodytext_1" onchange="funFontSize_1('FontSize')" style="width:45px;">
                                        <option selected="selected" value="0">Size</option>
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                        <option value="6">6</option>
                                        <option value="7">7</option>
                                        <option value="8">8</option>
                                        <option value="9">9</option>
                                    </select>
                                </td>
                                <%--Start:::Saperotor--%>
                                <%--<td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img id="Img8_1" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;">
                                </td>--%>
                                <%--End:::Saperotor--%>
                                <%--<td id="td20_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td20_1')" onmouseout="funTDBackgroundMouseOut_1('td20_1')">
                                    <select id="ddlHeader_1" class="bodytext_1" onchange="funFontHeader('headings')">
                                        <option selected="selected" value="0">Header</option>
                                        <option value="H1">H1</option>
                                        <option value="H2">H2</option>
                                        <option value="H3">H3</option>
                                        <option value="H4">H4</option>
                                        <option value="H5">H5</option>
                                        <option value="H6">H6</option>
                                    </select>
                                </td>--%>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img src="RichTextEditor/EditorImages/seperator2.gif" alt="" />
                                </td>
                                <td style="width:60px;"></td>
                            </tr>
                            <tr class="ClsTR_1" style="height:2px;"><td></td></tr>
                        </table>
                    </td>
                </tr>
                <tr class="ClsTR_1" id="trCotrolRowSecond_1">
                    <td class="ClsTR_1">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%; background-image:url(RichTextEditor/EditorImages/background_silver.jpg); background-repeat:repeat-x;">
                            <tr class="ClsTR_1" style="height:2px;"><td></td></tr>
                            <tr class="ClsTR_1" style="height:22px; background-image:url(RichTextEditor/EditorImages/background_silver.jpg); background-repeat:repeat-x;">
                                <td align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/seperator2.gif" alt="" />
                                </td>
                                <td id="td21_1" align="left" valign="top" class="clsEmotion_1">
                                    <img id="ImgEmotion1" src="RichTextEditor/EditorImages/Emotion1.gif" alt="" class="ClsImage_1" />          
                                </td>
                                <td id="td22_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion2.gif" alt="" id="ImgEmotion2" class="ClsImage_1" />          
                                </td>
                                <td id="td23_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion3.gif" alt="" id="ImgEmotion3" class="ClsImage_1" />          
                                </td>
                                <td id="td24_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion4.gif" alt="" id="ImgEmotion4" class="ClsImage_1" />          
                                </td>                            
                                <td id="td25_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion5.gif" alt="" id="ImgEmotion5" class="ClsImage_1" />          
                                </td>                            
                                <td id="td26_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion6.gif" alt="" id="ImgEmotion6" class="ClsImage_1" />          
                                </td>                            
                                <td id="td27_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion7.gif" alt="" id="ImgEmotion7" class="ClsImage_1" />          
                                </td>                            
                                <td id="td28_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion8.gif" alt="" id="ImgEmotion8" class="ClsImage_1" />          
                                </td>                            
                                <td id="td29_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion9.gif" alt="" id="ImgEmotion9" class="ClsImage_1" />          
                                </td>                            
                                <td id="td30_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion10.gif" alt="" id="ImgEmotion10" class="ClsImage_1" />          
                                </td>                            
                                <td id="td31_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion11.gif" alt="" id="ImgEmotion11" class="ClsImage_1" />          
                                </td>                            
                                <td id="td32_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion12.gif" alt="" id="ImgEmotion12" class="ClsImage_1" />          
                                </td>
                                <td id="td33_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion13.gif" alt="" id="ImgEmotion13" class="ClsImage_1" />          
                                </td>
                                <td id="td34_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion14.gif" alt="" id="ImgEmotion14" class="ClsImage_1" />          
                                </td>
                                <td id="td35_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/Emotion15.gif" alt="" id="ImgEmotion15" class="ClsImage_1" />          
                                </td>
                                <%--Start:::Saperotor--%>
                                <td align="left" valign="top" style="width:1px;" class="clsEmotion_1">
                                </td>
                                <td align="left" valign="top" class="clsEmotion_1">
                                    <img id="Img28_1" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;" class="clsEmotion_1">
                                </td>
                                <%--End:::Saperotor--%>                            
                                <td id="td36_1" align="left" valign="top" class="clsEmotion_1">
                                    <img src="RichTextEditor/EditorImages/wordcount.gif" alt="Word Count" id="ImgWordCount_1" class="ClsImage_1" />          
                                </td>
                                <td id="td37_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td37_1')" onmouseout="funTDBackgroundMouseOut_1('td37_1')">
                                    <img src="RichTextEditor/EditorImages/remove_format.gif" alt="Remove Formatting" id="ImgRemoveFormatting_1" class="ClsImage_1"
                                     onclick="funFontStyle_1('RemoveFormat')"
                                     onmouseover="funChangeImageMouseOver_1('ImgRemoveFormatting_1','remove_format_on.gif','Remove Formatting')"
                                     onmouseout="funChangeImageMouseOut_1('ImgRemoveFormatting_1','remove_format.gif','Remove Formatting')"
                                    />          
                                </td>
                                <td id="td38_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td38_1')" onmouseout="funTDBackgroundMouseOut_1('td38_1')">
                                    <img src="RichTextEditor/EditorImages/insertrule.gif" alt="Insert Ruler" id="ImgRule_1" class="ClsImage_1"
                                     onclick="funFontStyle_1('InsertHorizontalRule')"
                                     onmouseover="funChangeImageMouseOver_1('ImgRule_1','insertrule_on.gif','Insert Ruler')"
                                     onmouseout="funChangeImageMouseOut_1('ImgRule_1','insertrule.gif','Insert Ruler')"
                                    />          
                                </td>
                                <%--Start:::Saperotor--%>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img id="Img20_1" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <%--End:::Saperotor--%>
                                <td id="td39_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td39_1')" onmouseout="funTDBackgroundMouseOut_1('td39_1')">
                                    <img src="RichTextEditor/EditorImages/undo.gif" alt="Undo" id="ImgUndo_1" class="ClsImage_1"
                                     onclick="funFontStyle_1('undo')"
                                     onmouseover="funChangeImageMouseOver_1('ImgUndo_1','undo_on.gif','Undo')"
                                     onmouseout="funChangeImageMouseOut_1('ImgUndo_1','undo.gif','Undo')"
                                    />          
                                </td>
                                <td id="td40_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td40_1')" onmouseout="funTDBackgroundMouseOut_1('td40_1')">
                                    <img src="RichTextEditor/EditorImages/redo.gif" alt="Redo" id="ImgRedo_1" class="ClsImage_1"
                                     onclick="funFontStyle_1('redo')"
                                     onmouseover="funChangeImageMouseOver_1('ImgRedo_1','redo_on.gif','Redo')"
                                     onmouseout="funChangeImageMouseOut_1('ImgRedo_1','redo.gif','Redo')"
                                    />          
                                </td>
                                <%--Start:::Saperotor--%>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img id="Img25_1" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <%--End:::Saperotor--%>
                                <td id="td41_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td41_1')" onmouseout="funTDBackgroundMouseOut_1('td41_1')">
                                    <img src="RichTextEditor/EditorImages/insert_table.gif" alt="Insert Table" id="ImgTable_1" class="ClsImage_1"
                                     onclick="funInsertTable_1()"
                                     onmouseover="funChangeImageMouseOver_1('ImgTable_1','insert_table_on.gif','Insert Table')"
                                     onmouseout="funChangeImageMouseOut_1('ImgTable_1','insert_table.gif','Insert Table')"
                                    />          
                                </td>
                                <%  If Request("e") = "e" Then%>
                                    <td id="td42_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td42_1')" onmouseout="funTDBackgroundMouseOut_1('td42_1')">
                                        <img src="RichTextEditor/EditorImages/insert_picture.gif" alt="Insert Picture" id="ImgPicture_1" class="ClsImage_1"
                                         onclick="funInsertPicture_1()"
                                         onmouseover="funChangeImageMouseOver_1('ImgPicture_1','insert_picture_on.gif','Insert Picture')"
                                         onmouseout="funChangeImageMouseOut_1('ImgPicture_1','insert_picture.gif','Insert Picture')"
                                        />          
                                    </td>
                                <% Else%>
                                <% End If%>
                                
                                <td id="td43_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td43_1')" onmouseout="funTDBackgroundMouseOut_1('td43_1')">
                                    <img src="RichTextEditor/EditorImages/insert_hyperlink.gif" alt="Insert Hyperlink" id="ImgHyperlink_1" class="ClsImage_1"
                                     onclick="funHyperLink_1()"
                                     onmouseover="funChangeImageMouseOver_1('ImgHyperlink_1','insert_hyperlink_on.gif','Insert Hyperlink')"
                                     onmouseout="funChangeImageMouseOut_1('ImgHyperlink_1','insert_hyperlink.gif','Insert Hyperlink')"
                                    />          
                                </td>
                                <%--Start:::Saperotor--%>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img id="Img21_1" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <%--End:::Saperotor--%>
                                <td id="td44_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td44_1')" onmouseout="funTDBackgroundMouseOut_1('td44_1')">
                                    <img src="RichTextEditor/EditorImages/preview.gif" alt="View in Browser" id="ImgPreview_1" class="ClsImage_1"
                                     onclick="funPreViewHtmlText_1()"
                                     onmouseover="funChangeImageMouseOver_1('ImgPreview_1','preview_on.gif','View in Browser')"
                                     onmouseout="funChangeImageMouseOut_1('ImgPreview_1','preview.gif','View in Browser')"
                                    />          
                                </td>
                                <%--<td id="td45_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td45_1')" onmouseout="funTDBackgroundMouseOut_1('td45_1')">
                                    <img src="RichTextEditor/EditorImages/print.gif" alt="Print" id="ImgPrint_1" class="ClsImage_1"
                                     onclick="funWebPrint_1()"
                                     onmouseover="funChangeImageMouseOver_1('ImgPrint_1','print.over.gif','Print')"
                                     onmouseout="funChangeImageMouseOut_1('ImgPrint_1','print.gif','Print')"
                                    />          
                                </td>--%>
                                <td id="td46_1" class="ClsTD_1" onmouseover="funTDBackgroundMouseOver_1('td46_1')" onmouseout="funTDBackgroundMouseOut_1('td46_1')">
                                    <img src="RichTextEditor/EditorImages/view_source.gif" alt="View HTML" id="ImgHtml_1" class="ClsImage_1"
                                     onclick="funViewHtmlText_1()"
                                     onmouseover="funViewHtmlTextMouseOver_1()"
                                     onmouseout="funViewHtmlTextMouseOut_1()"
                                    />          
                                </td>
                                <%--Start:::Saperotor--%>
                                <%--<td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img id="Img26_1" src="RichTextEditor/EditorImages/Seperator.gif" alt="" />
                                </td>
                                <td align="left" valign="top" style="width:1px;">
                                </td>--%>
                                <%--End:::Saperotor--%>                    
                                <%--<td id="td47_1" align="left" valign="top">
                                    <img src="RichTextEditor/EditorImages/help.gif" alt="Help" id="ImgHelp" class="ClsImage_1" />          
                                </td>--%>
                                <td align="left" valign="top" style="width:1px;">
                                </td>
                                <td align="left" valign="top">
                                    <img src="RichTextEditor/EditorImages/seperator2.gif" alt="" />
                                </td>
                                <td style="width:290px;"></td>
                            </tr>
                            <tr class="ClsTR_1" style="height:2px;"><td></td></tr>
                        </table>
                    </td>
                </tr>
                <tr id="trTextRich_1" class="ClsTR_1" style="display:none;">
                <%--<tr id="trTextRich_1" class="ClsTR_1">--%>
                    <td class="ClsTR_1">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                            <tr class="ClsTR_1">
                                <td class="ClsTextBoxTD_1">
                                    <asp:TextBox ID="txtRich_1"
                                        runat="server"
                                        TextMode="MultiLine"
                                        ValidationGroup="1"
                                        CssClass="ClsTextBox_1"
                                    ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                            runat="server" ControlToValidate="txtRich_1" Display="None" ErrorMessage="Please Fill the Content"
                                            Font-Size="X-Small" ValidationGroup="1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="1" />
                                    <%--<asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ClientValidate2"
                                    ControlToValidate="txtRich_1" Display="None" ErrorMessage="Short Description length should be less than 1500 character" ValidationGroup="1"></asp:CustomValidator>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trFrame_1" class="ClsTR_1">
                    <td class="ClsTR_1">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                            <tr class="ClsTR_1">
                                <td class="ClsFrameTD_1" onkeyup="funTxtFill_1()" onkeydown="funTxtFill_1()" onclick="funTxtFill_1()" onkeypress="funTxtFill_1()">
                                    <iframe id="frmHtmlView_1"
                                        frameborder="1"
                                        marginheight="0"
                                        marginwidth="0"
                                        scrolling="auto"
                                        src="HtmlView_1.htm" onload="funFrameFill_1()"
                                        class="ClsFrame_1"
                                    >
                                    </iframe>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <%--<tr class="ClsTR_1">
                    <td class="ClsTR_1">
                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                            <tr class="ClsTR_1">
                                <td id="td46" class="ClsTD_1" style="width:50px;" onmouseover="funTDBackgroundMouseOver_1('td46')" onmouseout="funTDBackgroundMouseOut_1('td46')">
                                    <img src="RichTextEditor/EditorImages/view_source.gif" alt="View HTML" id="ImgHtml" class="ClsImage_1"
                                     onclick="funViewHtmlText_1()"
                                     onmouseover="funViewHtmlTextMouseOver_1()"
                                     onmouseout="funViewHtmlTextMouseOut_1()"
                                    />          
                                </td>
                                <td id="td44" align="left" valign="top" style="width:50px;">
                                    <img src="RichTextEditor/EditorImages/preview.gif" alt="View in Browser" id="ImgPreview" class="ClsImage_1" />          
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
            </table>
        </td>
    </tr>
</table>