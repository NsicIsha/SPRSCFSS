// JScript File

function funChangeImageMouseOver_1(ControlId, ImageToChange, altText)
{
    try
    {
        var ImageUrl;
        ImageUrl = "RichTextEditor/EditorImages/" + ImageToChange;
        document.getElementById(ControlId).src = ImageUrl;
        document.getElementById(ControlId).border = "0";
        document.getElementById(ControlId).alt = altText;
        document.getElementById(ControlId).width = "20";
        document.getElementById(ControlId).height = "20";
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
function funChangeImageMouseOut_1(ControlId, ImageToChange, altText)
{
    try
    {
        var ImageUrl;
        ImageUrl = "RichTextEditor/EditorImages/" + ImageToChange;
        document.getElementById(ControlId).src = ImageUrl;
        document.getElementById(ControlId).border = "0";
        document.getElementById(ControlId).alt = altText;
        document.getElementById(ControlId).width = "20";
        document.getElementById(ControlId).height = "20";
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
    
}

function funTDBackgroundMouseOver_1(ControlId)
{
    //document.getElementById(ControlId).style.backgroundColor="#FFFFFF";//"#D4D0C8";
}

function funTDBackgroundMouseOut_1(ControlId)
{
    //document.getElementById(ControlId).style.backgroundColor="#FFFFFF";
}

/*-------------------------------Start-------------------------------*/
/*Function set to switch b/w html and text view.*/
/*Onclick --> switch b/w frame & textbox.*/
function funViewHtmlText_1()
{
    try
    {
        var ControlId;
        ControlId=ViewHtmlTextId_1;
        
        var ImageUrl;
        ImageUrl=document.getElementById(ControlId).src;
        //alert(ImageUrl);
        
        var vInStr;
        vInStr=ImageUrl.indexOf("RichTextEditor/EditorImages/view_source_on.gif");
        
        if (eval(vInStr)>0)
        {
            document.getElementById(ControlId).src = "RichTextEditor/EditorImages/view_text_on.gif"    //"RichTextEditor/EditorImages/view_text.gif";
            document.getElementById(ControlId).alt = "View Text";
            document.getElementById(ControlId).width = "20";
            document.getElementById(ControlId).height = "20";
            
            document.getElementById(trTextRichId_1).style.display="block";
            document.getElementById(trFrameId_1).style.display="none";
            //document.getElementById(trCotrolRowFirstId).style.display="none";
            //document.getElementById(trCotrolRowSecondId).style.display="none";
            
            //Diasbled all controls
            FunDisabled_1();

            document.getElementById(txtRichId_1).value=document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML;
        }
        else
        {
            vInStr=ImageUrl.indexOf("RichTextEditor/EditorImages/view_text_on.gif");
            if (eval(vInStr)>0)
            {
                document.getElementById(ControlId).src = "RichTextEditor/EditorImages/view_source_on.gif"  //"RichTextEditor/EditorImages/view_source.gif";
                document.getElementById(ControlId).alt = "View HTML";
                document.getElementById(ControlId).width = "20";
                document.getElementById(ControlId).height = "20";
                
                document.getElementById(trFrameId_1).style.display="block";
                document.getElementById(trTextRichId_1).style.display="none";
                
                //document.getElementById(trCotrolRowFirstId).style.display="block";
                //document.getElementById(trCotrolRowSecondId).style.display="block";
                
                //Enabled all controls
                FunEnabled_1();
                
                document.getElementById(frmHtmlViewId_1).contentWindow.document.body.innerHTML=document.getElementById(txtRichId_1).value;
            }
        }
    }
    catch(e)
    {
        alert("Server is busy. Please try after some time.");
        return false;
    }    
}
/*OnmouseOver -->change image*/
function funViewHtmlTextMouseOver_1()
{
    try
    {
        var ControlId;
        ControlId=ViewHtmlTextId_1;
        
        var ImageUrl;
        ImageUrl=document.getElementById(ControlId).src;
        //alert(ImageUrl);
        
        var vInStr;
        vInStr=ImageUrl.indexOf("RichTextEditor/EditorImages/view_source.gif");
        
        if (eval(vInStr)>0)
        {
            document.getElementById(ControlId).src = "RichTextEditor/EditorImages/view_source_on.gif";
            document.getElementById(ControlId).border = "0";
            document.getElementById(ControlId).alt = "View HTML";
            document.getElementById(ControlId).width = "20";
            document.getElementById(ControlId).height = "20";
        }
        else
        {
            vInStr=ImageUrl.indexOf("RichTextEditor/EditorImages/view_text.gif");
            if (eval(vInStr)>0)
            {
                document.getElementById(ControlId).src = "RichTextEditor/EditorImages/view_text_on.gif";
                document.getElementById(ControlId).border = "0";
                document.getElementById(ControlId).alt = "View Text";
                document.getElementById(ControlId).width = "20";
                document.getElementById(ControlId).height = "20";
            }        
        }
    }
    catch(e)
    {
        //return false;
    }
}
/*OnmouseOut -->change image*/
function funViewHtmlTextMouseOut_1()
{
    try
    {
        var ControlId;
        ControlId=ViewHtmlTextId_1;
        
        var ImageUrl;
        ImageUrl=document.getElementById(ControlId).src;
        //alert(ImageUrl);
        
        var vInStr;
        vInStr=ImageUrl.indexOf("RichTextEditor/EditorImages/view_source_on.gif");
        
        if (eval(vInStr)>0)
        {
            document.getElementById(ControlId).src = "RichTextEditor/EditorImages/view_source.gif";
            document.getElementById(ControlId).border = "0";
            document.getElementById(ControlId).alt = "View HTML";
            document.getElementById(ControlId).width = "20";
            document.getElementById(ControlId).height = "20";
        }
        else
        {
            vInStr=ImageUrl.indexOf("RichTextEditor/EditorImages/view_text_on.gif");
            if (eval(vInStr)>0)
            {
                document.getElementById(ControlId).src = "RichTextEditor/EditorImages/view_text.gif";
                document.getElementById(ControlId).border = "0";
                document.getElementById(ControlId).alt = "View Text";
                document.getElementById(ControlId).width = "20";
                document.getElementById(ControlId).height = "20";
            }
            
        }
    }
    catch(e)
    {
        //alert("Server is busy. Please try after some time.");
        //return false;
    }
}
/*Function set to switch b/w html and text view.*/
/*-------------------------------End-------------------------------*/