// JScript File

/*------------------------------------- Start -------------------------------------*/
/*---- Function to call on Body onload ----*/
function funBodyLoad_1()
{
    try
    {
        //alert("funBodyLoad_1()");
        var d=document.getElementById(frmHtmlViewId_1).contentWindow.document;
        if (d.selection)
        {
            //IE
            document.getElementById(frmHtmlViewId_1).contentWindow.document.designMode = "on";
        }
        else
        {
            //FF
            document.getElementById(frmHtmlViewId_1).contentDocument.designMode = "on";   //IE, Firefox, Chrome, Safari, Opera
        }
        
        funaddEventsFrame_1();                           // Attach events to the iframe
        funaddEventsText_1();                            // Attach events to the textbox
        
        //funFrameFill();
    }
    catch(e)
    {
        alert(e.message);
        alert("Server is busy. Please try after some time.");
        return false;
    }
    
}
/*---- Function to call on Body onload ----*/
/*------------------------------------- End -------------------------------------*/




/*------------------------------------- Start -------------------------------------*/
/*---- Function to add events to frame ----*/
function funaddEventsFrame_1()
{
    var d=document.getElementById(frmHtmlViewId_1).contentWindow.document;
    if (d.selection)
    {
        //IE
        document.getElementById(frmHtmlViewId_1).contentWindow.document.attachEvent("onclick", function(){funTxtFill_1();});
        document.getElementById(frmHtmlViewId_1).contentWindow.document.attachEvent("onkeyup", function(){funTxtFill_1();});
        document.getElementById(frmHtmlViewId_1).contentWindow.document.attachEvent("onkeydown", function(){funTxtFill_1();});
        document.getElementById(frmHtmlViewId_1).contentWindow.document.attachEvent("onkeypress", function(){funTxtFill_1();});
        document.getElementById(frmHtmlViewId_1).contentWindow.document.attachEvent("onmouseup", function(){funTxtFill_1();});
        document.getElementById(frmHtmlViewId_1).contentWindow.document.attachEvent("onmousedown", function(){funTxtFill_1();});
    }
    else
    {
        //FF
        document.getElementById(frmHtmlViewId_1).contentDocument.addEventListener("click", function(){funTxtFill_1();}, false);
        document.getElementById(frmHtmlViewId_1).contentDocument.addEventListener("keyup", function(){funTxtFill_1();}, false);
        document.getElementById(frmHtmlViewId_1).contentDocument.addEventListener("keydown", function(){funTxtFill_1();}, false);
        document.getElementById(frmHtmlViewId_1).contentDocument.addEventListener("keypress", function(){funTxtFill_1();}, false);
        document.getElementById(frmHtmlViewId_1).contentDocument.addEventListener("mouseup", function(){funTxtFill_1();}, false);
        document.getElementById(frmHtmlViewId_1).contentDocument.addEventListener("mousedown", function(){funTxtFill_1();}, false);
    }    
}
/*---- Function to add events to frame ----*/
/*------------------------------------- End -------------------------------------*/




/*------------------------------------- Start -------------------------------------*/
/*---- Function to add events to textbox ----*/
function funaddEventsText_1()
{
    var d=document.getElementById(frmHtmlViewId_1).contentWindow.document;
    if (d.selection)
    {
        //IE        
        document.getElementById(txtRichId_1).attachEvent("onclick", function(){funFrameFill_1();});
        document.getElementById(txtRichId_1).attachEvent("onkeyup", function(){funFrameFill_1();});
        document.getElementById(txtRichId_1).attachEvent("onkeydown", function(){funFrameFill_1();});
        document.getElementById(txtRichId_1).attachEvent("onkeypress", function(){funFrameFill_1();});
        document.getElementById(txtRichId_1).attachEvent("onmouseup", function(){funFrameFill_1();});
        document.getElementById(txtRichId_1).attachEvent("onmousedown", function(){funFrameFill_1();});
    }
    else
    {
        //FF
        document.getElementById(txtRichId_1).addEventListener("click", function(){funFrameFill_1();}, false);
        document.getElementById(txtRichId_1).addEventListener("keyup", function(){funFrameFill_1();}, false);
        document.getElementById(txtRichId_1).addEventListener("keydown", function(){funFrameFill_1();}, false);
        document.getElementById(txtRichId_1).addEventListener("keypress", function(){funFrameFill_1();}, false);
        document.getElementById(txtRichId_1).addEventListener("mouseup", function(){funFrameFill_1();}, false);
        document.getElementById(txtRichId_1).addEventListener("mousedown", function(){funFrameFill_1();}, false);
    }
}
/*---- Function to add events to textbox ----*/
/*------------------------------------- End -------------------------------------*/




/*------------------------------------- Start -------------------------------------*/
/*---- Function to Fill text box ----*/
function funTxtFill_1()
{
    try
    {
        //alert("funTxtFill");
        document.getElementById(txtRichId_1).value=document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
    }
    catch(e)
    {
        alert("Server is busy. Please try after some time.");
        return false;
    }
}
/*---- Function to Fill text box ----*/
/*------------------------------------- End -------------------------------------*/




/*------------------------------------- Start -------------------------------------*/
/*---- Function to Fill frame window ----*/
function funFrameFill_1()
{
    try
    {
        //alert("funFrameFill");
        document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML=document.getElementById(txtRichId_1).value;
    }
    catch(e)
    {
        alert("Server is busy. Please try after some time.");
        return false;
    }
}
/*---- Function to Fill frame window ----*/
/*------------------------------------- End -------------------------------------*/




/*------------------------------------- Start -------------------------------------*/
/*---- Function to change Font Style ----*/
function funFontStyle_1(vrStyle_1)
{
    try
    {
        //alert(vrStyle_1);
        //alert(frmHtmlViewId_1);
        if (funTextOrHtml_1()==true)
        {
            //document.frames["frmHtmlView"].document.execCommand(vrStyle_1,false,null);      //IE
            document.getElementById(frmHtmlViewId_1).contentWindow.document.execCommand(vrStyle_1,false,null);      //IE, Firefox, Chrome, Safari, Opera

            funTxtFill_1();
            //document.getElementById(txtRichId_1).value=document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
        }
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
/*---- Function to change Font Style ----*/
/*------------------------------------- End -------------------------------------*/



/*------------------------------------- Start -------------------------------------*/
/*---- Function to change Font Name ----*/
function funFontName_1(vrStyle_1)
{
    try
    {
        var FontName;
        FontName=document.getElementById(ddlFontNameId_1).value;
        
        document.getElementById(frmHtmlViewId_1).contentWindow.document.execCommand(vrStyle_1,false,FontName);      //IE, Firefox, Chrome, Safari, Opera

        funTxtFill_1();
        //document.getElementById(txtRichId_1).value=document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
/*---- Function to change Font Name ----*/
/*------------------------------------- End -------------------------------------*/



/*------------------------------------- Start -------------------------------------*/
/*---- Function to change Font Size ----*/
function funFontSize_1(vrStyle_1)
{
    try
    {
        var FontSize;
        FontSize=document.getElementById(ddlFontSizeId_1).value;
        
        document.getElementById(frmHtmlViewId_1).contentWindow.document.execCommand(vrStyle_1,false,FontSize);      //IE, Firefox, Chrome, Safari, Opera

        funTxtFill_1();
        //document.getElementById(txtRichId_1).value=document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
/*---- Function to change Font Size ----*/
/*------------------------------------- End -------------------------------------*/













/*------------------------------------- Start -------------------------------------*/
/*---- Function to change Font Header ----*/
//function funFontHeader(vrStyle_1)
//{    
//    var FontHeader;    
//    FontHeader=document.getElementById(ddlHeaderId).value;
//    alert(FontHeader);
//    
//    document.getElementById(frmHtmlViewId_1).contentWindow.document.execCommand(vrStyle_1,false,FontHeader);      //IE, Firefox, Chrome, Safari, Opera

//    document.getElementById(txtRichId_1).value=document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
//}
/*---- Function to change Font Header ----*/
/*------------------------------------- End -------------------------------------*/




/*------------------------------------- Start -------------------------------------*/
/*---- Function to change Insert Picture ----*/
function funInsertPicture_1()
{
    try
    {
        if (funTextOrHtml_1()==true)
        {
            var url= window.location.href;
            var result = url.split('=');
            var s= result[1].split('&');
            var path=s[0];
                    
            window.open(vrWinOpenUploadImage_1+path,'','height=230, width=770, resizable=yes, scrollbars=no,titlebar=no,toolbar=no,statusbar=no,left=50, top=100');
        }
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}

function funImageNotSelect_1()
{
    //alert("funImageNotSelect");
    window.close();
}

function funImageSelect_1(ImageTag_1)
{
    try
    {
        //alert("funImageSelect(ImageTag)");
        //alert(ImageTag);
        
        //Insert into main editor
        //funInsertHtmlTag_1(ImageTag_1);
        funInsertHtmlTagWithLink_1(ImageTag_1);
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}

function funImageHoverInsert_1(Imgname_1)
{
    try
    {
        //alert("function funImageHoverInsert(Imgname)");
        //alert(Imgname);
        
        var ImageTag_1;
        ImageTag_1="<img src='" + Imgname_1 + "' alt='' border='0' />"
        
        funImageSelect_1(ImageTag_1);
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}

function funImageControlClose_1()
{
    window.close();
}

/*---- Function to change Insert Picture ----*/
/*------------------------------------- End -------------------------------------*/



/*------------------------------------- Start -------------------------------------*/
/*---- Function to Insert HyperLink ----*/
function funHyperLink_1()
{
    try
    {
        if (funTextOrHtml_1()==true)
        {
            window.open(vrWinOpenInsertHyperLink_1,'','height=130, width=370, resizable=yes, scrollbars=no,titlebar=no,toolbar=no,statusbar=no,left=50, top=100');
        }
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}

function funinsertHyperLink_1()
{
    try
    {
        //alert("function funinsertHyperLink()");
        var edLinkUrl;
        var edLinkTarget;
        var edLinkClass;
        var edLinkName;
        
        edLinkUrl=document.getElementById("txtTblLink_Url").value;
        edLinkTarget=document.getElementById("txtTblLink_Target").value;
        edLinkClass=document.getElementById("txtTblLink_Class").value;
        edLinkName=document.getElementById("txtTblLink_Name").value;
        
        //alert(edLinkUrl);
        
        if (trim_1(edLinkName)=="")
        {
            alert("Please provide link name!!");
            document.getElementById("txtTblLink_Name").focus();
            return false;
        }
        
        if (trim_1(edLinkUrl)=="")
        {
            alert("Please provide link url!!");
            document.getElementById("txtTblLink_Url").focus();
            return false;
        }
        
        var edLink1;
        
        var browser1=navigator.appName;
        browser1=browser1.toUpperCase(); 
        if (browser1=="MICROSOFT INTERNET EXPLORER")
        {
            edLink1='<a href="http://' + edLinkUrl + '"';
        }
        else
        {
            edLink1='<a href="http://' + edLinkUrl + '"';
        }
        
        if (trim_1(edLinkTarget)!="") edLink1 +=' target="' + edLinkTarget + '"';
        
        if (trim_1(edLinkClass)!="") edLink1 +=' class="' + edLinkClass + '"';
        
        edLink1 +='>';
        edLink1 +=edLinkName + '</a>';
        
        //alert(edLink1);
        
        //return false;
        //Insert into main editor
        funInsertHtmlTag_1(edLink1);
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
/*---- Function to Insert HyperLink ----*/
/*------------------------------------- End -------------------------------------*/




/*------------------------------------- Start -------------------------------------*/
/*---- Function to Insert Table ----*/
function funInsertTable_1()
{
    try
    {
        if (funTextOrHtml_1()==true)
        {
            window.open(vrWinOpenInsertTable_1,'','height=210, width=350, resizable=yes, scrollbars=no,titlebar=no,toolbar=no,statusbar=no,left=50, top=100');
        }
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}

function funHtmCreateTable_1()
{
    try
    {
        //alert("submit");
        var edRow;
        var edCell;
        var edPadding;
        var edBorderSize;
        var edBorderStyle;
        var edWidth;
        var edWidthType;
        var edAlignment;
        var edBackColor;
        var edBorderColor;
        
        edRow=document.getElementById("txtTblInsrt_Row").value;
        edCell=document.getElementById("txtTblInsrt_Cols").value;
        
        edPadding=document.getElementById("txtTblInsrt_Padding").value;
        edBorderSize=document.getElementById("txtTblInsrt_BorderSize").value;    
        edBorderStyle=document.getElementById("txtTblInsrt_BorderStyle").value;
        
        edWidth=document.getElementById("txtTblInsrt_Width").value;
        edWidthType=document.getElementById("txtTblInsrt_WidthType").value;
        
        edAlignment=document.getElementById("txtTblInsrt_Alignment").value;
        edBackColor=document.getElementById("backgroundcolor").value;
        edBorderColor=document.getElementById("bordercolor").value;
        
        //alert(edRow);
        //alert(edCell);
        var edTable;
        var edStyle1;
        var edStyle2;
        var edStyle3;
        
        edStyle1=" style='";
        edStyle3="' "
        
        edStyle2="";
        
        if (trim_1(edPadding)!="") edStyle2 +="padding:" + edPadding + "px; ";
        
        if (trim_1(edBorderSize)!="") edStyle2 +="border-width:" + edBorderSize + "px; ";
        
        if (trim_1(edBorderStyle)!="") edStyle2 +="border-style:" + edBorderStyle + "; ";
        
        if (trim_1(edWidth)!="") edStyle2 +="width:" + edWidth + edWidthType + "; ";
        
        if (trim_1(edAlignment)!="") edStyle2 +="text-align:" + edAlignment + "; ";
        
        if (trim_1(edBackColor)!="")
        {
            if (edBackColor!="none")
            {
                edStyle2 +="background-color:" + edBackColor + "; ";
            }        
        }
        
        if (trim_1(edBorderColor)!="")
        {
            if (edBorderColor!="none")
            {
                edStyle2 +="border-color:" + edBorderColor + "; ";
            }        
        }
        
        //alert(edStyle2);
        
        
        
        
        edTable="<table" + edStyle1 + edStyle2 + edStyle3 + ">"
        
        var i;
        var j;
        
        for (i=0; i<eval(edRow); i++)
        {
            j=0;
            
            edTable +="<tr>";        
            for (j=0; j<eval(edCell); j++)
            {
                edTable +="<td></td>";
            }
            edTable +="</tr>";
        }
        edTable +="</table>";
        //alert(edTable);


        //Insert into main editor
        funInsertHtmlTag_1(edTable);
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
/*---- Function to Insert Table ----*/
/*------------------------------------- End -------------------------------------*/




/*------------------------------------- Start -------------------------------------*/
/*---- Function to Print web page ----*/
function funWebPrint_1()
{
    try
    {
        //alert("funWebPrint");
        //window.print();
        
        document.getElementById(frmHtmlViewId_1).contentWindow.document.body.print();
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
/*---- FFunction to Print web page ----*/
/*------------------------------------- End -------------------------------------*/



/*------------------------------------- Start -------------------------------------*/
/*---- Function to Preview web page ----*/
function funPreViewHtmlText_1()
{
    try
    {
        if (funTextOrHtml_1()==true)
        {
            //alert("function funViewHtmlText()");
            window.open('RichTextEditor/htmlPreview_1.htm','','height=500, width=700, resizable=yes, scrollbars=yes,titlebar=no,toolbar=no,statusbar=no,left=50, top=100');
        }
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
function funHtmlPreviewData_1()
{
    try
    {
        //alert("function funHtmlPreviewData()");
        //alert(frmHtmlViewId_1);
        //alert(window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML);
        document.getElementById("spnHtmlPreviewId").innerHTML=window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
/*---- FFunction to Preview web page ----*/
/*------------------------------------- End -------------------------------------*/



/*------------------------------------- Start -------------------------------------*/
/*---- Function set to change Fore color & Back color ----*/
function funColor_1(ColorType)
{
    try
    {
        if (funTextOrHtml_1()==true)
        {
            window.open('RichTextEditor/EditorHtml/htmlForeColor_1.htm?ColorType='+ColorType,'','height=250, width=240, resizable=yes, scrollbars=yes,titlebar=no,toolbar=no,statusbar=no');
        }
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}

function funColorMouseOver_1(vrColor)
{
    try
    {
        document.getElementById("txtColorMouseOver").style.backgroundColor=vrColor;
        document.getElementById("txtColorMouseOverCode").value=vrColor;
    }
    catch(e)
    {
        //alert();
    }
}

function funColorSelect_1(vrColor)
{
    try
    {
        var ColorType;
        var fullURL;
        fullURL = parent.document.URL;
        ColorType = fullURL.substring(fullURL.indexOf('?')+11, fullURL.length)

        document.getElementById("txtColorSelect").style.backgroundColor=vrColor;
        
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.execCommand(ColorType,false,vrColor);      //IE, Firefox, Chrome, Safari, Opera
        window.opener.document.getElementById(txtRichId_1).value=window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
        
        window.close();
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
/*---- Function set to change Fore color & Back color ----*/
/*------------------------------------- End -------------------------------------*/


//Function for Ordered & Unordered list
function funList_1(ListType)
{
    try
    {
        if (funTextOrHtml_1()==true)
        {
            //<%=hdnAllAdminId.ClientID %>

            //<%=txtRich.ClientID %>
            //<%=frmHtmlView.ClientID %>
            //alert("funList");
            //document.getElementById(frmHtmlViewId_1).contentWindow.document.execCommand(ListType);
            document.getElementById(frmHtmlViewId_1).contentWindow.document.execCommand(ListType,false,null);
            
            funTxtFill_1();
            //document.getElementById(txtRichId_1).value=document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
        }
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
















function funInsertHtmlTag_1(Tag)
{
    //alert(browser_1);
    browser_1=browser_1.toUpperCase(); 
    //alert(browser);
    
    //alert("funInsertHtmlTag");
    //alert(Tag);
    //var d=window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document;
    //if (d.selection)
    if (browser_1=="MICROSOFT INTERNET EXPLORER")
    {
        //IE
        //alert("IE");
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.focus();
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.selection.createRange().pasteHTML(Tag);
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.focus();
    }
    else
    {
        //FF
        //alert("FF");
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.focus();
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.execCommand("InsertHTML",false,Tag); 
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.focus();
    }

    //window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.execCommand(ColorType,false,vrColor);      //IE, Firefox, Chrome, Safari, Opera
    
    
    window.opener.document.getElementById(txtRichId_1).value=window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
    window.close();
}

function funInsertHtmlTagWithLink_1(Tag)
{
    browser_1=browser_1.toUpperCase(); 
    if (browser_1=="MICROSOFT INTERNET EXPLORER")
    {
        //IE
        //alert("IE");
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.focus();
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.selection.createRange().pasteHTML(Tag);
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.focus();
    }
    else
    {
        //FF
        //alert("FF");
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.focus();
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.execCommand("InsertHTML",false,Tag); 
        window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.focus();
    }
    
    var vrFrmHtmlData_1;
    //alert(pthUploadImageStringToFind_1);
    //alert(pthUploadImageStringToReplace_1);
    
    vrFrmHtmlData_1=window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
    
    //alert(vrFrmHtmlData_1);
    vrFrmHtmlData_1=vrFrmHtmlData_1.replace(pthUploadImageStringToFind_1,pthUploadImageStringToReplace_1);
    //alert(vrFrmHtmlData_1);
    
    window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML=vrFrmHtmlData_1;
    
    window.opener.document.getElementById(txtRichId_1).value=window.opener.document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
    window.close();
}











// Removes leading whitespaces
function LTrim_1( value )
{
	var re = /\s*((\S+\s*)*)/;
	return value.replace(re, "$1");
}
// Removes ending whitespaces
function RTrim_1( value )
{
	var re = /((\s*\S+)*)\s*/;
	return value.replace(re, "$1");
}
// Removes leading and ending whitespaces
function trim_1( value )
{
	return LTrim_1(RTrim_1(value));
}

function funTextOrHtml_1()
{
    var ImageUrl;
    ImageUrl=document.getElementById(ViewHtmlTextId_1).src;
    var vInStr;
    vInStr=ImageUrl.indexOf("RichTextEditor/EditorImages/view_source.gif");

    if (eval(vInStr)>0)
    {
        return true;
    }
    else
    {
        alert(alertDisableText_1);
        return false;
    }
}