                                          
var persistmenu="yes" //"yes" or "no". Make sure each SPAN content contains an incrementing ID starting at 1 (id="sub1", id="sub2", etc)
var persisttype="sitewide" //enter "sitewide" for menu to persist across site, "local" for this page only

if (document.getElementById){ //DynamicDrive.com change
	document.write('<style type="text/css">\n')
	document.write('.submenu{display: none;}\n')

	document.write('.subsubmenu{display: none;}\n')

	document.write('</style>\n')
}

function SwitchMenu(obj,imgid)
{
	if(document.getElementById)
	{
		var el = document.getElementById(obj);
		var ar = document.getElementById("masterdiv").getElementsByTagName("span"); //DynamicDrive.com change
			if(el.style.display != "block")
				{ //DynamicDrive.com change
					for (var i=0; i<ar.length; i++)
						{
						    if (ar[i].className == "submenu") //DynamicDrive.com change
						    {
						        //$("#" + ar[i].id).hide(700);
						        ar[i].style.display = "none";
						    } 
						}
					/**/
						//$("#" + obj).show(700);
						el.style.display = "block";
					var L3_IMG;
					L3_IMG = eval(document.getElementById(imgid));
					L3_IMG.src="../TreeMenu/menuimg/minus.gif";

				}
				else
				{
				    //$("#" + obj).hide(700);
				    el.style.display = "none";
					var L3_IMG;
					L3_IMG = eval(document.getElementById(imgid));
					L3_IMG.src="../TreeMenu/menuimg/plus.gif";
				}
		 
			// image minus to plus
			for(var j=1;j<=ar.length;j++)
				{
					var L3_IMG;
					var imageid;
					var temp
					if(eval(document.getElementById('img'+j)))
						{
							
							//alert(imgid);
							temp = imgid.split('img');
							filePosition=temp.length-1;
							var imageid= temp[filePosition];
							//alert(imageid + '::' + j)
							if(eval(j)!=eval(imageid))
								{

									L3_IMG = eval(document.getElementById('img'+j));
									//alert(L3_IMG);
									L3_IMG.src="../TreeMenu/menuimg/plus.gif";
								}
							
							

						}
				}/**/
		}
}
function sub_SwitchMenu(obj){
	if(document.getElementById){
	var el = document.getElementById(obj);
	var ar = document.getElementById("masterdiv").getElementsByTagName("span"); //DynamicDrive.com change
		if(el.style.display != "block"){ //DynamicDrive.com change
			for (var i=0; i<ar.length; i++){
				if (ar[i].className=="subsubmenu") //DynamicDrive.com change
				ar[i].style.display = "none";
			}
			el.style.display = "block";
		}else{
			el.style.display = "none";
		}
	}
}

function get_cookie(Name) { 
var search = Name + "="
var returnvalue = "";
if (document.cookie.length > 0) {
offset = document.cookie.indexOf(search)
if (offset != -1) { 
offset += search.length
end = document.cookie.indexOf(";", offset);
if (end == -1) end = document.cookie.length;
returnvalue=unescape(document.cookie.substring(offset, end))
}
}
return returnvalue;
}

function onloadfunction(){
if (persistmenu=="yes"){
	var cookiename=(persisttype=="sitewide")? "switchmenu" : window.location.pathname
	var cookievalue=get_cookie(cookiename)
	if (cookievalue!="")
	document.getElementById(cookievalue).style.display="block"
	

//
	cookievalue==""
	var cookiename=(persisttype=="sitewide")? "sub_switchmenu" : window.location.pathname
	var cookievalue=get_cookie(cookiename)
	if (cookievalue!="")
	document.getElementById(cookievalue).style.display="block"

//
}
}

function savemenustate(){
var inc=1, blockid=""
while (document.getElementById("sub"+inc)){
if (document.getElementById("sub"+inc).style.display=="block"){
blockid="sub"+inc
break
}
inc++
}
var cookiename=(persisttype=="sitewide")? "switchmenu" : window.location.pathname
var cookievalue=(persisttype=="sitewide")? blockid+";path=/" : blockid
document.cookie=cookiename+"="+cookievalue

//
inc=1
blockid=""
while (document.getElementById("subsub"+inc)){
if (document.getElementById("subsub"+inc).style.display=="block"){
blockid="subsub"+inc
break
}
inc++
}
var cookiename=(persisttype=="sitewide")? "sub_switchmenu" : window.location.pathname
var cookievalue=(persisttype=="sitewide")? blockid+";path=/" : blockid
document.cookie=cookiename+"="+cookievalue
//

}

if (window.addEventListener)
window.addEventListener("load", onloadfunction, false)
else if (window.attachEvent)
window.attachEvent("onload", onloadfunction)
else if (document.getElementById)
window.onload=onloadfunction

if (persistmenu=="yes" && document.getElementById)
window.onunload=savemenustate