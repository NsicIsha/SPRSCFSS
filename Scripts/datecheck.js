/**
 
 */
// Declaring valid date character, minimum year and maximum year
var dtCh= "/";
var minYear=1900;
var maxYear=2100;

function isInteger(s){
	var i;
    for (i = 0; i < s.length; i++){   
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag){
	var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++){   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary (year){
	// February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}
function DaysArray(n) {
	for (var i = 1; i <= n; i++) {
		this[i] = 31
		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
		if (i==2) {this[i] = 29}
   } 
   return this
}

function isDate(dtStr)
{

    if(dtStr.indexOf("-")>0)
     {
        dtStr=dtStr.replace("-","/");
        dtStr=dtStr.replace("-","/");
     }
    if(dtStr.indexOf(".")>0)
     {
        dtStr=dtStr.replace(".","/");
        dtStr=dtStr.replace(".","/");
     }

	var daysInMonth = DaysArray(12)
	var pos1=dtStr.indexOf(dtCh)
	var pos2=dtStr.indexOf(dtCh,pos1+1)

	var mnth=dtStr.substring(pos1+1,pos2)
	var strDay=dtStr.substring(0,pos1)
	var strYear=dtStr.substring(pos2+1)
	strYr=strYear
    var strMonth;
    mnth=mnth.toLowerCase();
   // alert(mnth);
    switch(mnth)
			 {
			 case 'jan':strMonth='01';
						break;
			 case 'feb':strMonth='02';
						break;
     		case 'mar':strMonth='03';
						break;
			 case 'apr':strMonth='04';
						break;
			 case 'may':strMonth='05';
						break;
     		case 'jun':strMonth='06';
						break;
			 case 'jul':strMonth='07';
						break;
			 case 'aug':strMonth='08';
						break;
     		case 'sep':strMonth='09';
						break;
			 case 'oct':strMonth='10';
						break;
			 case 'nov':strMonth='11';
						break;
			 case 'dec':strMonth='12';
						break;		
						case 'january':strMonth='01';
					break;
		 case 'february':strMonth='02';
					break;
 		case 'march':strMonth='03';
					break;
		 case 'april':strMonth='04';
					break;
		 case 'may':strMonth='05';
					break;
 		case 'june':strMonth='06';
					break;
		 case 'july':strMonth='07';
					break;
		 case 'august':strMonth='08';
					break;
 		case 'september':strMonth='09';
					break;
		 case 'october':strMonth='10';
					break;
		 case 'november':strMonth='11';
					break;
		 case 'december':strMonth='12';
					break;			
			 default :strMonth='13';                      
			 }
dtStr=strDay+'/'+strMonth+'/'+strYear;

	if (strDay.charAt(0)=="0" && strDay.length>1) 
	{
	strDay=strDay.substring(1)
	}
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	for (var i = 1; i <= 3; i++) {
		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
	}
	month=parseInt(strMonth);
	day=parseInt(strDay);
	year=parseInt(strYr);
	
	if (pos1==-1 || pos2==-1){
		alert("The date format should be : dd/mmm/yyyy");
		return false;
	}
	if (strMonth.length<1 || month<1 || month>12){
		alert("Please enter a valid month");
		return false;
	}
	if(strYr.length < 4 || strYr.length > 4){
		alert("Please enter a valid year");
		return false;
    }
	
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
	alert("Please enter a valid day");
		return false;
	}
//	if (strYear.length != 4 || year==0 || year<parseInt(1965) || year>parseInt(2000)){
//		alert("Please enter a valid 4 digit year between "+1965+" and "+2000)
//		return false
//	}
	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){
		//alert(dtStr.indexOf(dtCh,pos2+1));
		alert("Please enter a valid date");
		return false;
	}
	
return true
	
}
//function ValidateForm(osrc,args)
//{
//	
//	var dt=args.Value	
//	if (isDate(dt)==false){
//		
//		args.IsValid=false;
//		
//	return false
//	}
//	else
//	{
//	 args.IsValid=true;
//	 return true
//	}
//    
// }
 
function convertDate(dtStr)
{
    if(dtStr.indexOf("-")>0)
    {
        dtStr=dtStr.replace("-","/");
        dtStr=dtStr.replace("-","/");
    }
    if(dtStr.indexOf(".")>0)
    {
        dtStr=dtStr.replace(".","/");
        dtStr=dtStr.replace(".","/");
    }
//alert(dtStr);
    var pos1=dtStr.indexOf('/')
	var pos2=dtStr.indexOf(dtCh,pos1+1)
    var mnth=dtStr.substring(pos1+1,pos2)
	var strDay=dtStr.substring(0,pos1)
	var strYear=dtStr.substring(pos2+1)
	strYr=strYear
    var strMonth;
    mnth=mnth.toLowerCase();

switch(mnth)
			 {
			 case 'jan':strMonth='01';
						break;
			 case 'feb':strMonth='02';
						break;
     		case 'mar':strMonth='03';
						break;
			 case 'apr':strMonth='04';
						break;
			 case 'may':strMonth='05';
						break;
     		case 'jun':strMonth='06';
						break;
			 case 'jul':strMonth='07';
						break;
			 case 'aug':strMonth='08';
						break;
     		case 'sep':strMonth='09';
						break;
			 case 'oct':strMonth='10';
						break;
			 case 'nov':strMonth='11';
						break;
			 case 'dec':strMonth='12';
						break;		
						case 'january':strMonth='01';
					break;
		 case 'february':strMonth='02';
					break;
 		case 'march':strMonth='03';
					break;
		 case 'april':strMonth='04';
					break;
		 case 'may':strMonth='05';
					break;
 		case 'june':strMonth='06';
					break;
		 case 'july':strMonth='07';
					break;
		 case 'august':strMonth='08';
					break;
 		case 'september':strMonth='09';
					break;
		 case 'october':strMonth='10';
					break;
		 case 'november':strMonth='11';
					break;
		 case 'december':strMonth='12';
					break;			
			 default :strMonth='13';                      
			 }
//dtStr=strDay+'/'+strMonth+'/'+strYear;
dtStr=strMonth+'/'+strDay+'/'+strYear;
return dtStr
}



function dateDiff(src,args)
 {
    var myDOB =document.getElementById('ctl00_ContentPlaceHolder1_txtDob').value.split('/');
    myDate = myDOB[0];
    mnth= myDOB[1];
    switch(mnth)
		 {
		 case 'jan':strMonth='01';
					break;
		 case 'feb':strMonth='02';
					break;
 		case 'mar':strMonth='03';
					break;
		 case 'apr':strMonth='04';
					break;
		 case 'may':strMonth='05';
					break;
 		case 'jun':strMonth='06';
					break;
		 case 'jul':strMonth='07';
					break;
		 case 'aug':strMonth='08';
					break;
 		case 'sep':strMonth='09';
					break;
		 case 'oct':strMonth='10';
					break;
		 case 'nov':strMonth='11';
					break;
		 case 'dec':strMonth='12';
					break;		
		case 'january':strMonth='01';
					break;
		 case 'february':strMonth='02';
					break;
 		case 'march':strMonth='03';
					break;
		 case 'april':strMonth='04';
					break;
		 case 'may':strMonth='05';
					break;
 		case 'june':strMonth='06';
					break;
		 case 'july':strMonth='07';
					break;
		 case 'august':strMonth='08';
					break;
 		case 'september':strMonth='09';
					break;
		 case 'october':strMonth='10';
					break;
		 case 'november':strMonth='11';
					break;
		 case 'december':strMonth='12';
					break;				
		 default :strMonth='13';                      
		 }
    myMonth=parseInt(strMonth);
    myYear = myDOB[2];
    var age;
    var now = new Date();
    var todayDate = now.getDate();
    var todayMonth = now.getMonth()+1;
    var todayYear = now.getFullYear();
    if(todayDate<myDate)
    {
    todayDate+=30;
    todayMonth-=1;        
    }
    if(todayMonth<myMonth)
    {
    todayMonth+=12;
    todayYear-=1;
    }
    var year=todayYear-myYear;
    if(year<18)
    {
        args.IsValid=false;
    }
    else
    {
        args.IsValid=true;
    }
}
