// JScript File
function Call1()
{
	//evt = (evt) ? evt : ((window.event) ? window.event : "")  
	//if(evt.keyCode == 9)
	//{
		CheckUserName();
//		CheckEmployerName()
	//}
}

var xmlHttp
var id
var MyPic
var is_ie = (navigator.userAgent.indexOf('MSIE') >= 0) ? 1 : 0; 
var is_ie5 = (navigator.appVersion.indexOf("MSIE 5.5")!=-1) ? 1 : 0; 
var is_opera = ((navigator.userAgent.indexOf("Opera6")!=-1)||(navigator.userAgent.indexOf("Opera/6")!=-1)) ? 1 : 0; 
//netscape, safari, mozilla behave the same??? 
var is_netscape = (navigator.userAgent.indexOf('Netscape') >= 0) ? 1 : 0; 


//CHECK USER NAME
function CheckUserName()
    { 
    var email = document.getElementById("ctl00_ContentPlaceHolder1_txtUserId").value;
    if(escape(email) != "")
     {
	    var url = '../checkusername.aspx?uname=' + escape(email);
	    xmlHttp=GetXmlHttpObject(stateChanged)
	    xmlHttp.open("GET", url , true)
	    xmlHttp.send(null)
        stateChanged()
     }
    else
    {
        document.getElementById("spanaval").className = 'msgRed';
	    document.getElementById("spanaval").innerHTML = "Please enter user id";
    }
} 

function stateChanged() 
{ 
	if (xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
	{ 
	    if(xmlHttp.responseText == "false")
	    {
	        document.getElementById("spanaval").className = 'msgRed'
	        document.getElementById("spanaval").innerHTML = "User Name not available";
	    }
	    else
	    {
		    document.getElementById("spanaval").className = 'msgGreen'
	        document.getElementById("spanaval").innerHTML = "User Name available";
		}
	} 
} 

function GetXmlHttpObject(handler) 
{ 
    var objXmlHttp = null;    //Holds the local xmlHTTP object instance

    //Depending on the browser, try to create the xmlHttp object 
    if (is_ie)
    { 
        //The object to create depends on version of IE 
        //If it isn't ie5, then default to the Msxml2.XMLHTTP object 
        var strObjName = (is_ie5) ? 'Microsoft.XMLHTTP' : 'Msxml2.XMLHTTP'; 
         
        //Attempt to create the object 
        try{ 
            objXmlHttp = new ActiveXObject(strObjName); 
            objXmlHttp.onreadystatechange = handler; 
        } 
        catch(e){ 
        //Object creation errored 
            alert('IE detected, but object could not be created. Verify that active scripting and activeX controls are enabled'); 
            return; 
        } 
    } 
    else if (is_opera){ 
        //Opera has some issues with xmlHttp object functionality 
        alert('Opera detected. The page may not behave as expected.'); 
        return; 
    } 
    else{ 
        // Mozilla | Netscape | Safari 
        objXmlHttp = new XMLHttpRequest(); 
        objXmlHttp.onload = handler; 
        objXmlHttp.onerror = handler; 
    } 
     
    //Return the instantiated object 
    return objXmlHttp; 
} 
