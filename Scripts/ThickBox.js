//弹出图片层（feature page）
//function InitImg(obj, apptype, imgsrc) {
//    if (apptype == 1)
//        imgsrc = "../images/livechat/feature/" + imgsrc;
//    if (apptype == 4)
//        imgsrc = "../images/newsletter/feature/" + imgsrc;
//        
//  //  alert(document.getElementById("divThickInner"));
//    $(obj).bind('click', function() {
//        $('#ImgScreenshots').attr("src", imgsrc);
//        showWindow('divThickInner', 'divThickOuter')
//    });
//}
function SetObjectOnMouseOver(object, color)
{
    object.style.background = color;
}

function SetObjectOnMouseOut(object, color)
{
    object.style.background = color;
}

function insertSpan(object, product)
{
    document.getElementById(object).innerHTML = "<span>(&nbsp;Why "+product +" is free?&nbsp;)</a>";
}

function showWindow(_in, _out) {

    var ThickInner = document.getElementById(_in);
    var ThickOuter = document.getElementById(_out);

    var x, y, innerY;
    var canViewHeight = document.body.clientHeight;
    var broswerHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : window.innerWidth;

    var de = document.documentElement;

    x = GetWindowX(de);

    //document.documentElement.clientWidth ? document.documentElement.clientWidth : window.innerWidth;
    y = GetWindowY(de);
    //canViewHeight >= broswerHeight ? canViewHeight : broswerHeight;

    innerY = GetInnerY();

    ThickInner.style.position = "absolute";
    ThickInner.style.zIndex = 99999;

    var _height, _width;

    _height = ThickInner.style.height.replace("px", "");
    _width = ThickInner.style.width.replace("px", "");

    setInterval(function() { SetThickInner(de, ThickInner, _height, _width); }, 100);
    ThickInner.style.display = "block";

    ThickOuter.style.position = "absolute";
    setInterval(function() { SetThickOuter(de, ThickOuter, 0, 0); }, 100);

    ThickOuter.style.zIndex = 99998;
    ThickOuter.style.display = "block";
    try { document.getElementById("flashcontent").style.display = "none"; }
    catch (e) {

    }
}


function SetThickOuter(_document, _o, _left, _top) {
    var x = GetWindowX(_document);
    _o.style.left = _left + 'px';
    _o.style.top = _top + 'px';
    _o.style.width = x < 800 ? 800 : x + 'px';
    _o.style.height = OverlaySize() + 'px';
}

function SetThickInner(_document, _o, _height, _width) {
    var broswerHeight = GetBroswerHeight();
    var innerY = GetInnerY();
    _o.style.width = _width + 'px';
    _o.style.height = _height + 'px';
    _o.style.left = (x - _width) / 2 + 'px';
    _o.style.top = innerY + broswerHeight / 2 - _height / 2 + 'px';
}

function GetWindowX(_document) {
    x = window.innerWidth ||
        self.innerWidth ||
        (_document && _document.clientWidth) ||
        document.body.clientWidth;
    return x;
}

function GetWindowY(_document) {
    y = window.innerHeight ||
        self.innerHeight ||
        (_document && _document.clientHeight) ||
        document.body.clientHeight;
    return y;
}

function GetBroswerHeight() {
    var broswerHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : window.innerWidth;
    return broswerHeight;
}

function GetInnerY() {
    if (typeof window.pageYOffset != 'undefined') {
        innerY = window.pageYOffset;
    }
    else if (typeof document.compatMode != 'undefined' && document.compatMode != 'BackCompat') {
        innerY = document.documentElement.scrollTop;
    }
    else if (typeof document.body != 'undefined') {
        innerY = document.body.scrollTop;
    }
    return innerY;
}

function WindowClose(_in, _out) {
    var i = document.getElementById(_in);
    var o = document.getElementById(_out);

    if (i != null) i.style.display = "none";
    if (o != null) o.style.display = 'none';
    try { document.getElementById("flashcontent").style.display = ""; }
    catch (e) {

    }
}
function WindowClose1(_in, _out) {
    var i = document.getElementById(_in);
    var o = document.getElementById(_out);
    location.href = location.href;

    if (i != null) i.style.display = "none";
    if (o != null) o.style.display = 'none';

}
function OverlaySize() {
    if (window.innerHeight && window.scrollMayY) {
        return window.innerHeight + window.scrollMaxY;
    }
    else if (document.body.scrollHeight > document.body.offsetHeight) {
        return document.body.scrollHeight;
    }
    else {
        return document.body.offsetHeight;
    }
}



  