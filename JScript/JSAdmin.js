//DEFAULT BUTTON CLICK
function FunDefaultButtonClick(ButtonId)
{
    if((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13))
    {
        document.getElementById(ButtonId).click();
        return false;
    }
    else
    {
        return true;
    }
}
//-----------------------------------
//MODAL POPUP TEXTBOX FOCUS
function FunModalpopupFocus(ControlId)
{
    setTimeout("FunFocusTextbox('"+ ControlId +"')",1000);
}
//-----------------------------------

//
//TEXT BOX FOCUS
function FunFocusTextbox(TextboxId)
{
    document.getElementById(TextboxId).focus();
}
//-----------------------------------

//NUMERIC + NEGATIVE + FLOAT CHECK
function IsNumeric(strString)
{
    var strValidChars = "0123456789.-";
    var strChar;
    var cnt=0 ; 
    var blnResult = true;
    if (strString.length == 0) return false;
    //  test strString consists of valid characters listed above
    for (i = 0; i < strString.length && blnResult == true; i++)
    {
        strChar = strString.charAt(i);
        if(strChar=="-" && i>0)
        {
            return false;
        }
        else
        {
            if (strValidChars.indexOf(strChar) == -1)
            {
                blnResult = false;
            }
        }
    }
    for (i = 0; i < strString.length && blnResult == true; i++)
    {
        strChar = strString.charAt(i);
        if (strString.charAt(i)=='.')
        {
            cnt = cnt + 1 ;
        }
        if (cnt>1) return false;
    }
    return blnResult;
}
//-----------------------------------

//NUMERIC + NONNEGATIVE + FLOAT CHECK
function IsNumericNonNegative(source, arguments)
{
    var strString = arguments.Value ;
    var strValidChars = "0123456789.";
    var strChar;
    var cnt=0 ; 
    var blnResult = true;
    if (strString.length == 0) blnResult=false;
    //  test strString consists of valid characters listed above
    for (i = 0; i < strString.length && blnResult == true; i++)
    {
        strChar = strString.charAt(i);
        if(strChar=="-" && i>0)
        {
            blnResult=false;
        }
        else
        {
            if (strValidChars.indexOf(strChar) == -1)
            {
                blnResult = false;
            }
        }
    }
    for (i = 0; i < strString.length && blnResult == true; i++)
    {
        strChar = strString.charAt(i);
        if (strString.charAt(i)=='.')
        {
            cnt = cnt + 1 ;
        }
        if (cnt>1) blnResult= false;
    }
    return blnResult;
    if(blnResult==false)
    {
        arguments.IsValid = false;
    }
    else
    {
        arguments.IsValid = true;
    }
    
     
}
//-----------------------------------

//DATE FORMAT
function ChkDateFormat(ControlId)
{
    var DateFormat=/((^(10|12|0?[13578])([/])(3[01]|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(11|0?[469])([/])(30|[12][0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(2[0-8]|1[0-9]|0?[1-9])([/])((1[8-9]\d{2})|([2-9]\d{3}))$)|(^(0?2)([/])(29)([/])([2468][048]00)$)|(^(0?2)([/])(29)([/])([3579][26]00)$)|(^(0?2)([/])(29)([/])([1][89][0][48])$)|(^(0?2)([/])(29)([/])([2-9][0-9][0][48])$)|(^(0?2)([/])(29)([/])([1][89][2468][048])$)|(^(0?2)([/])(29)([/])([2-9][0-9][2468][048])$)|(^(0?2)([/])(29)([/])([1][89][13579][26])$)|(^(0?2)([/])(29)([/])([2-9][0-9][13579][26])$))/;
    var DateToCheck=document.getElementById(ControlId).value;
    var MatchArray=DateToCheck.match(DateFormat);
    if(MatchArray==null)
    {
        document.getElementById(ControlId).focus();
        return false;
    }
}
//-----------------------------------

//POPUP OPEN
function FunOpenPopup(x)
{
    window.open(x,'','height=400,width=400,resizable=yes,scrollbars=yes');
} 

function FunOpenPopup950x600(x)
{
    window.open(x,'','height=600,width=950,resizable=yes,scrollbars=yes');
} 
//-----------------------------------

//CUSTOM VALIDATION - Character Limit
function CharacterLimit200(source, arguments)
{
    if (arguments.Value.length  <=200)
    {
        arguments.IsValid = true;
    }
    else
    {
        arguments.IsValid = false;
    }
}
function CharacterLimit2000(source, arguments)
{
    if (arguments.Value.length  <=2000)
    {
        arguments.IsValid = true;
    }
    else
    {
        arguments.IsValid = false;
    }
}
function CharacterLimit8000(source, arguments)
{
    if (arguments.Value.length  <=8000)
    {
        arguments.IsValid = true;
    }
    else
    {
        arguments.IsValid = false;
    }
}
function CharacterLimit5000(source, arguments)
{
    if (arguments.Value.length  <=5000)
    {
        arguments.IsValid = true;
    }
    else
    {
        arguments.IsValid = false;
    }
}
//-----------------------------------

//ALERT MESSAGE
function FunAlertTrue(msg)
{
    alert(msg);
}
function FunAlertFalse(msg)
{
    alert(msg);
    return false;
}
//-----------------------------------

//DISPLAY NONE
function FunActionClose(cntId)
{
    document.getElementById(cntId).style.display="none";
}


// Removes leading whitespaces
function LTrim( value )
{
	var re = /\s*((\S+\s*)*)/;
	return value.replace(re, "$1");
}
// Removes ending whitespaces
function RTrim( value )
{
	var re = /((\s*\S+)*)\s*/;
	return value.replace(re, "$1");
}
// Removes leading and ending whitespaces
function trim( value )
{
	return LTrim(RTrim(value));
}




















//UTTAM----------------------------------
function FunZoneActionClose()
{
    document.getElementById("ctl00_ContentPlaceHolder1_divAction").style.display="none";
}

function FunResetZone()
{
    document.getElementById("ctl00_ContentPlaceHolder1_txtZoneName").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtDescription").value="";
}

function FunResetBranch()
{
    document.getElementById("ctl00_ContentPlaceHolder1_txtBranchName").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtCode").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtDescription").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txt_branch_address").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txt_phone").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txt_email").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txt_fax").value="";
}

function FunResetDept()
{
    document.getElementById("ctl00_ContentPlaceHolder1_txtDeptName").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtDescription").value="";
}

function FunResetInsp()
{
    document.getElementById("ctl00_ContentPlaceHolder1_txtInspName").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtDescription").value="";
}

function FunResetFees()
{
    document.getElementById("ctl00_ContentPlaceHolder1_txtInspName").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtDescription").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txt_slab_increment").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txt_slab_increment_fee").value="";
}

function FunResetInspbr()
{
    document.getElementById("ctl00_ContentPlaceHolder1_txtInstr").value="";
}

function FunResetInspfee()
{
    document.getElementById("ctl00_ContentPlaceHolder1_txtSlab").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtAmount").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtInstruction").value="";
}

function FunResetFees()
{
    document.getElementById("ctl00_ContentPlaceHolder1_txtSlab").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtAmount").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtDescription").value="";
}

function FunResetReg()
{
    document.getElementById("ctl00_ContentPlaceHolder1_txtReg").value="";
}
function FunResetDayins()
{
    document.getElementById("ctl00_ContentPlaceHolder1_txtIssue").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtStart").value="";
    document.getElementById("ctl00_ContentPlaceHolder1_txtSubmit").value="";
}

//Created bY rAVISH

 function OpenPrintPage()
    {
    
     if($("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="Registration Forms Filled" ||$("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="Companies List" ||$("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="Amendment" || $("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="Renewal")
      {
    
     
   var dateFrom= $("#ctl00_ContentPlaceHolder1_TextBox1").val();
        $("Report_criteria").show();
         var FormType;
         var orderBy;
      var dateto=$("#ctl00_ContentPlaceHolder1_txt_21").val();
       var ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
    var BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
     var Lblreport=$("#ctl00_ContentPlaceHolder1_Report_Type").text();
       var CategoryID= parseInt($("#ctl00_ContentPlaceHolder1_ddl_category_wise_report").val());
        var SubCategoryID=parseInt($("#ctl00_ContentPlaceHolder1_Ddl_category_cast").val());
        var CastID=parseInt($("#ctl00_ContentPlaceHolder1_DdlCategoryUnitSize").val());
          var SatateID=$("#ctl00_ContentPlaceHolder1_ddl_state_wise").val();
          var City_ID=$("#ctl00_ContentPlaceHolder1_Ddl_City_Wise").val();
          var turn_over_one=$("#ctl00_ContentPlaceHolder1_txt_turn_over_wise").val();
          var turn_over_two=$("#ctl00_ContentPlaceHolder1_txt_turnover_wise_to").val();
         var Monetery_limit_one=$("#ctl00_ContentPlaceHolder1_txt_monetary_limit_from").val();
         var Monetary_limit_two=$("#ctl00_ContentPlaceHolder1_txt_monetarylimit_to").val(); 
        var HideStatus=$("#ctl00_ContentPlaceHolder1_ddlstatuswise").val();
          var provisinal=$("#ctl00_ContentPlaceHolder1_DdlProvisinal").val();
              var Statuswise=$("#ctl00_ContentPlaceHolder1_ddlstatuswise").val();
                 if($("#ctl00_ContentPlaceHolder1_ddlstatuswise").val()=="--Select--")
                 {
                   Statuswise="--Selctone--";
                 }
              var orderbyUser=$("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val();
              if($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val()=="")
                {
                  BranchID=0;
                }
                else
                {
                  BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
                }
                if($("#ctl00_ContentPlaceHolder1_ddlZonewise").val()=="")
                   {
                    ZoneID=0;  
                   }
                   
                    else
                    {
                      ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
                    }
                    if($("#ctl00_ContentPlaceHolder1_ddl_state_wise").val()=="0" || $("#ctl00_ContentPlaceHolder1_ddl_state_wise").val()=="undefined" ||  $("#ctl00_ContentPlaceHolder1_ddl_state_wise").val()=="--Select State--" || $("#ctl00_ContentPlaceHolder1_ddl_state_wise").val()=="")
                    {
                         SatateID=0;
                         
                    }
                     else
                     {
                     SatateID=parseInt($("#ctl00_ContentPlaceHolder1_ddl_state_wise").val());
                     }
                     if($("#ctl00_ContentPlaceHolder1_Ddl_City_Wise").val()=="0" || $("#ctl00_ContentPlaceHolder1_Ddl_City_Wise").val()=="undefined" || $("#ctl00_ContentPlaceHolder1_Ddl_City_Wise").val()=="--Select City--" || $("#ctl00_ContentPlaceHolder1_Ddl_City_Wise").val()=="")
                     {
                        City_ID=0;
                     }
                     else
                     {
                        City_ID= parseInt($("#ctl00_ContentPlaceHolder1_Ddl_City_Wise").val());
                     }
                     if($("#ctl00_ContentPlaceHolder1_txt_turn_over_wise").val()=="" || $("#ctl00_ContentPlaceHolder1_txt_turn_over_wise").val()==" ")
                         {
                           turn_over_one=" ";    
                         }
                         else
                         {
                            turn_over_one=$("#ctl00_ContentPlaceHolder1_txt_turn_over_wise").val();
                         }
                         if($("#ctl00_ContentPlaceHolder1_txt_turnover_wise_to").val()=="" || $("#ctl00_ContentPlaceHolder1_txt_turnover_wise_to").val()==" ")
                            {
                                turn_over_two=" ";
                            }
                             else
                            {
                                turn_over_two=$("#ctl00_ContentPlaceHolder1_txt_turnover_wise_to").val();
                            }
                             if($("#ctl00_ContentPlaceHolder1_txt_monetary_limit_from").val()=="" || $("#ctl00_ContentPlaceHolder1_txt_monetary_limit_from").val()== " ")
                             {
                                 Monetery_limit_one=" ";
                             }
                               else
                               {
                                 Monetery_limit_one=$("#ctl00_ContentPlaceHolder1_txt_monetary_limit_from").val();
                               }
                               if($("#ctl00_ContentPlaceHolder1_txt_monetarylimit_to").val()=="" || $("#ctl00_ContentPlaceHolder1_txt_monetarylimit_to").val()==" ")
                                {
                                Monetary_limit_two=" ";
                                }
                                else
                                {
                                 Monetary_limit_two=$("#ctl00_ContentPlaceHolder1_txt_monetarylimit_tos").val();
                                }
                               if($("#ctl00_ContentPlaceHolder1_ddl_category_wise_report").val()=="0")
                                    {
                                    CategoryID=0;
                                        }
                                 else
                                    {
                                 CategoryID= parseInt($("#ctl00_ContentPlaceHolder1_ddl_category_wise_report").val());
                             }
                       if($("#ctl00_ContentPlaceHolder1_Ddl_category_cast").val()=="0")
                        {
                          SubCategoryID=0;
                        }
                         else
                         {
                          SubCategoryID=parseInt($("#ctl00_ContentPlaceHolder1_Ddl_category_cast").val());
                         }
                             if($("#ctl00_ContentPlaceHolder1_DdlCategoryUnitSize").val()=="0")
                                {
                           CastID=0;
                                }
                                else
                                     {
                                 CastID=parseInt($("#ctl00_ContentPlaceHolder1_DdlCategoryUnitSize").val());
                                 }
                          
                        if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="1")
                          {
                              orderBy=1;
                          }  
                          
                        if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="2")
                          {
                              orderBy=2;
                          }  
                            if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="3")
                          {
                              orderBy=3;
                          } 
              
         if(HideStatus=="undefined")
          {
            Statuswise=="--Selctone--";
          }
                else
                {
                
                if($("#ctl00_ContentPlaceHolder1_ddlstatuswise").val()=="--Select--")
                   {
                       Statuswise="--Selctone--"
                   }
                   else
                   {
            Statuswise=$("#ctl00_ContentPlaceHolder1_ddlstatuswise").val();
                         }
                }
            if(provisinal=="undefined")
            {
                 provisinal="--Selctone--";
            }
            else
            {
               provisinal=$("#ctl00_ContentPlaceHolder1_DdlProvisinal").val(); 
            }
            
        if(Statuswise=="--Selctone--" )
          {
             window.open("Print.aspx?dt="+ dateFrom + "&dto=" + dateto + "&brid=" + BranchID + "&zid=" + ZoneID + "&statusid=" +Statuswise + "&CID="+CategoryID + "&SatateID="+SatateID+ "&City_ID="+City_ID+ "&turn_over_one="+turn_over_one+" &turn_over_two= "+turn_over_two+ "&Monetery_limit_one=" +Monetery_limit_one+" &Monetary_limit_two="+Monetary_limit_two+ "&SID="+SubCategoryID+ "&CastID="+CastID+ "&Lblreport=" +Lblreport + "&provisinal="+provisinal + "&orderby="+orderBy, 'Print' , "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1");
 	        }
 	
 	
 	        else
 	        {
 	   window.open("Print.aspx?dt="+dateFrom + "&dto=" +dateto + "&brid=" +BranchID +"&zid=" +ZoneID + "&statusid=" +Statuswise +"&CID=" +CategoryID+"&SatateID="+SatateID+"&City_ID="+City_ID+"&turn_over_one="+turn_over_one+"&turn_over_two="+turn_over_two+"&Monetery_limit_one="+Monetery_limit_one+"&Monetary_limit_two="+Monetary_limit_two+ "&SID="+SubCategoryID+ "&CastID="+CastID+ "&lblreport=" +Lblreport + "&provisinal="+provisinal +  "&orderby="+orderBy, 'Print', "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1"); 
 	   
 	        }
}
 
   else if($("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="Payment list by(Payment Report)")
      {
      
          var dateFrom= $("#ctl00_ContentPlaceHolder1_TextBox1").val();
        $("Report_criteria").show();
      var dateto=$("#ctl00_ContentPlaceHolder1_txt_21").val();
       var ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
         var CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
     var BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
     var Lblreport=$("#ctl00_ContentPlaceHolder1_Report_Type").text();
     var HideStatus=$("#ctl00_ContentPlaceHolder1_ddlstatuswise").val();
     var comanytype=$("#ctl00_ContentPlaceHolder1_ddlType").val();
     var orderBy;
     var orderbyUser=$("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val();
          if($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val()=="")
                {
                  BranchID=0;
                }
                else
                {
                  BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
                }
         if($("#ctl00_ContentPlaceHolder1_ddlZonewise").val()=="")
             {
                ZoneID=0;  
              }
                   
          else
             {
                 ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
             }
           if($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val()=="")
             {
               CompanyID=0;
             }
              else
              {
                 CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
                 
              } 
              
               if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="1")
                          {
                              orderBy=1;
                          }  
                          
                        if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="2")
                          {
                              orderBy=2;
                          }  
                            if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="3")
                          {
                              orderBy=3;
                          }  
              
               window.open("PrintPaymentDetails.aspx?dt="+ dateFrom + "&dto=" + dateto + "&brid=" + BranchID + "&zid=" + ZoneID + "&CompanyID="+CompanyID +"&Ctype="+comanytype + "&Orderby="+orderBy, 'Print' , "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1");      
         
      }
      
      else if($("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="User List")
       {
          var dateFrom= $("#ctl00_ContentPlaceHolder1_TextBox1").val();
        $("Report_criteria").show();
      var dateto=$("#ctl00_ContentPlaceHolder1_txt_21").val();
       var ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
         var CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
         var BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());  
      
           if($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val()=="")
                {
                  BranchID=0;
                }
                else
                {
                  BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
                }
         if($("#ctl00_ContentPlaceHolder1_ddlZonewise").val()=="")
             {
                ZoneID=0;  
              }
                   
          else
             {
                 ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
             }
             window.open("PrintUserListDetails.aspx?dt="+dateFrom+"&dto="+dateto+"&brid="+BranchID+"&zid="+ZoneID, 'Print', "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1");
       }
       
       else if($("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="Inspection Agency List")
       {
          var dateFrom= $("#ctl00_ContentPlaceHolder1_TextBox1").val();
        $("Report_criteria").show();
      var dateto=$("#ctl00_ContentPlaceHolder1_txt_21").val();
       var ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
         var CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
         var BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());  
      
           if($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val()=="")
                {
                  BranchID=0;
                }
                else
                {
                  BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
                }
         if($("#ctl00_ContentPlaceHolder1_ddlZonewise").val()=="")
             {
                ZoneID=0;  
              }
                   
          else
             {
                 ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
             }
             window.open("PrintInspectionAgencyList.aspx?dt="+dateFrom+"&dto="+dateto+"&brid="+BranchID+"&zid="+ZoneID, 'Print', "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1");
       }
       
       else if($("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="Duplicate Certificate Print Request")
       {
         var dateFrom= $("#ctl00_ContentPlaceHolder1_TextBox1").val();
        $("Report_criteria").show();
      var dateto=$("#ctl00_ContentPlaceHolder1_txt_21").val();
       var ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
         var CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
         var BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());  
                 var orderBy;
     var orderbyUser=$("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val();
           if($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val()=="")
                {
                  BranchID=0;
                }
                else
                {
                  BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
                }
              if($("#ctl00_ContentPlaceHolder1_ddlZonewise").val()=="")
             {
                ZoneID=0;  
              }
                   
          else
             {
                 ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
             }
              if($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val()==null)
             {
               CompanyID=0;
             }
              else
              {
                 CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
                 
              } 
              if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="1")
                          {
                              orderBy=1;
                          }  
                          
                        if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="2")
                          {
                              orderBy=2;
                          }  
                            if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="3")
                          {
                              orderBy=3;
                          }  
              
            window.open("PRINTREPORTACESSPRINT.aspx?dt="+dateFrom+"&dto="+dateto+"&brid="+BranchID+"&zid="+ZoneID+"&cid="+CompanyID + "&Orderby="+orderBy, 'Print', "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1");
 
    }
    }


    function FunOpenPopup950x6001(x)
     {
    //window.open(x,'','height=600,width=950,resizable=yes,scrollbars=yes, target=_self');
      location.href =x;

     }
     
     
     // Created By Ravish Kumar Pandey for specific Report Page............
     
     
  function OpenPrintPage1()
    {
    
     if($("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="1" ||$("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="5" ||$("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="2"  || $("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="10")
      {
     
   var dateFrom= $("#ctl00_ContentPlaceHolder1_TextBox1").val();
               var OrderBY;
        $("Report_criteria").show();
      var dateto=$("#ctl00_ContentPlaceHolder1_txt_21").val();
       var ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
    var BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
     var Lblreport=$("#ctl00_ContentPlaceHolder1_Report_Type").text();
        var HideStatus=$("#ctl00_ContentPlaceHolder1_ddlstatuswise").val();
                       
              var Statuswise=$("#ctl00_ContentPlaceHolder1_ddlstatuswise").val();
                if($("#ctl00_ContentPlaceHolder1_ddlstatuswise").val()=="--Select--")
                   {
                     Statuswise="--Selctone--"
                   }
              var  CategoryID=$("#ctl00_ContentPlaceHolder1_ddl_category_wise_report").val();
              
              var   SubCategoryID=$("#ctl00_ContentPlaceHolder1_Ddl_category_cast").val();
              var CastID=$("#ctl00_ContentPlaceHolder1_DdlCategoryUnitSize").val();
               var provisinal=$("#ctl00_ContentPlaceHolder1_DdlProvisinal").val();
               var Orderby_user=$("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val();
               var SatateID=$("#ctl00_ContentPlaceHolder1_ddl_state_wise").val();
               var City_ID=$("#ctl00_ContentPlaceHolder1_Ddl_City_Wise").val();
               var turn_over_one=$("#ctl00_ContentPlaceHolder1_txt_turn_over_wise").val();
               var turn_over_two=$("#ctl00_ContentPlaceHolder1_txt_turnover_wise_to").val();
               var Monetery_limit_one=$("#ctl00_ContentPlaceHolder1_txt_monetary_limit_from").val();
               var Monetary_limit_two=$("#ctl00_ContentPlaceHolder1_txt_monetarylimit_to").val();
              
              if($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val()=="")
                {
                  BranchID=0;
                }
                else
                {
                  BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
                }
                if($("#ctl00_ContentPlaceHolder1_ddlZonewise").val()=="")
                   {
                    ZoneID=0;  
                   }
                   
                    else
                    {
                      ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
                    }
                    
                     if($("#ctl00_ContentPlaceHolder1_ddl_state_wise").val()=="0" || $("#ctl00_ContentPlaceHolder1_ddl_state_wise").val()=="undefined" ||  $("#ctl00_ContentPlaceHolder1_ddl_state_wise").val()=="--Select State--" || $("#ctl00_ContentPlaceHolder1_ddl_state_wise").val()=="")
                    {
                         SatateID=0;
                         
                    }
                     else
                     {
                      SatateID=parseInt($("#ctl00_ContentPlaceHolder1_ddl_state_wise").val());
                     }
                      if($("#ctl00_ContentPlaceHolder1_Ddl_City_Wise").val()=="0" || $("#ctl00_ContentPlaceHolder1_Ddl_City_Wise ").val()=="undefined" ||  $("#ctl00_ContentPlaceHolder1_Ddl_City_Wise").val()=="--Select City--" || $("#ctl00_ContentPlaceHolder1_Ddl_City_Wise").val()=="")
                    {
                         City_ID=0;
                         
                    }
                     else
                     {
                     City_ID=parseInt($("#ctl00_ContentPlaceHolder1_Ddl_City_Wise").val());
                     }
                    if($("#ctl00_ContentPlaceHolder1_txt_turn_over_wise").val()==" " || $("#ctl00_ContentPlaceHolder1_txt_turn_over_wise").val()=="" )
                    {
                         turn_over_one=" ";
                         
                    }
                     else
                     {
                    turn_over_one=$("#ctl00_ContentPlaceHolder1_txt_turn_over_wise").val();
                     }
                    if($("#ctl00_ContentPlaceHolder1_txt_turnover_wise_to").val()==" " || $("#ctl00_ContentPlaceHolder1_txt_turnover_wise_to").val()=="" )
                    {
                        turn_over_two=" ";
                    }
                     else
                     {
                   turn_over_two=$("#ctl00_ContentPlaceHolder1_txt_turnover_wise_to").val();
                     }
                       if($("#ctl00_ContentPlaceHolder1_txt_monetary_limit_from").val()==" " || $("#ctl00_ContentPlaceHolder1_txt_monetary_limit_from").val()=="" )
                    {
                         Monetery_limit_one=" ";
                         
                    }
                     else
                     {
                    Monetery_limit_one=$("#ctl00_ContentPlaceHolder1_txt_monetary_limit_from").val();
                     }
                    if($("#ctl00_ContentPlaceHolder1_txt_monetarylimit_to").val()==" " || $("#ctl00_ContentPlaceHolder1_txt_monetarylimit_to").val()=="" )
                    {
                        Monetary_limit_two=" ";
                    }
                     else
                     {
                   Monetary_limit_two=$("#ctl00_ContentPlaceHolder1_txt_monetarylimit_to").val();
                     }
                     
                     
                     
                    if($("#ctl00_ContentPlaceHolder1_ddl_category_wise_report").val()=="0")
                      {
                       CategoryID=0;
                      }
                       else
                       {
                        CategoryID= parseInt($("#ctl00_ContentPlaceHolder1_ddl_category_wise_report").val());
                       }
                       if($("#ctl00_ContentPlaceHolder1_Ddl_category_cast").val()=="0")
                        {
                          SubCategoryID=0;
                        }
                         else
                         {
                          SubCategoryID=parseInt($("#ctl00_ContentPlaceHolder1_Ddl_category_cast").val());
                         }
                         if($("#ctl00_ContentPlaceHolder1_DdlCategoryUnitSize").val()=="0")
                          {
                           CastID=0;
                          }
                          else
                          {
                          CastID=parseInt($("#ctl00_ContentPlaceHolder1_DdlCategoryUnitSize").val());
                          }
                          
                          if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="1")
                            {
                              OrderBY=1;
                            }
                            if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="2")
                            {
                              OrderBY=2;
                            }
                              if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="3")
                            {
                              OrderBY=3;
                            }
              
         if(HideStatus=="undefined")
          {
            Statuswise=="--Selctone--";
          }
           else
           {
           
               if($("#ctl00_ContentPlaceHolder1_ddlstatuswise").val()=="--Select--")
               {
                 Statuswise="--Selctone--";
               }
               else
               {
       Statuswise=$("#ctl00_ContentPlaceHolder1_ddlstatuswise").val();
                         }
       }
       if(provisinal=="undefined")
       {
          provisinal="--Selctone--";
       }
        else
        {
          provisinal=$("#ctl00_ContentPlaceHolder1_DdlProvisinal").val();
        }
        if(Statuswise=="--Selctone--" )
          {
    
           window.open("Print.aspx?dt="+ dateFrom + "&dto=" + dateto + "&brid=" + BranchID + "&zid=" + ZoneID + "&statusid=" +Statuswise + "&CID=" +CategoryID+ "&SatateID="+SatateID+"&City_ID="+City_ID+"&turn_over_one="+turn_over_one+"&turn_over_two="+turn_over_two+"&Monetery_limit_one="+Monetery_limit_one+"&Monetary_limit_two="+Monetary_limit_two+  "&SID="+SubCategoryID+ "&CastID="+CastID+ "&Lblreport=" +Lblreport + "&provisinal="+provisinal + "&orderby="+OrderBY, 'Print' , "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1");
         	}
 	
 
 	else
 	{
 	   window.open("Print.aspx?dt="+dateFrom + "&dto=" +dateto + "&brid=" +BranchID +"&zid=" +ZoneID + "&statusid=" +Statuswise + "&CID=" +CategoryID+ "&SatateID="+SatateID+"&City_ID="+City_ID+"&turn_over_one="+turn_over_one+"&turn_over_two="+turn_over_two+"&Monetery_limit_one="+Monetery_limit_one+"&Monetary_limit_two="+Monetary_limit_two+  "&SID="+SubCategoryID+ "&CastID="+CastID+ "&lblreport=" +Lblreport+ "&provisinal="+provisinal + "&orderby="+OrderBY, 'Print', "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1"); 
 	   
 	}
}
 
   else if($("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="6")
      {
      var OrderBY;
          var dateFrom= $("#ctl00_ContentPlaceHolder1_TextBox1").val();
        $("Report_criteria").show();
      var dateto=$("#ctl00_ContentPlaceHolder1_txt_21").val();
       var ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
         var CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
    var BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
     var Lblreport=$("#ctl00_ContentPlaceHolder1_Report_Type").text();
     var HideStatus=$("#ctl00_ContentPlaceHolder1_ddlstatuswise").val();
     var comanytype=$("#ctl00_ContentPlaceHolder1_ddlType").val();
     var Orderby_user=$("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val();

          if($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val()=="")
                {
                  BranchID=0;
                }
                else
                {
                  BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
                }
         if($("#ctl00_ContentPlaceHolder1_ddlZonewise").val()=="")
             {
                ZoneID=0;  
              }
                   
          else
             {
                 ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
             }
           if($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val()=="")
             {
               CompanyID=0;
             }
              else
              {
                 CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
                 
              } 
                            if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="1")
                            {
                              OrderBY=1;
                            }
                            if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="2")
                            {
                              OrderBY=2;
                            }
                              if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="3")
                            {
                              OrderBY=3;
                            } 
              
               window.open("PrintPaymentDetails.aspx?dt="+ dateFrom + "&dto=" + dateto + "&brid=" + BranchID + "&zid=" + ZoneID + "&CompanyID="+CompanyID +"&Ctype="+comanytype + "&orderby="+OrderBY, 'Print' , "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1");      
         
      }
      
      else if($("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="7")
       {
          var dateFrom= $("#ctl00_ContentPlaceHolder1_TextBox1").val();
        $("Report_criteria").show();
      var dateto=$("#ctl00_ContentPlaceHolder1_txt_21").val();
       var ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
         var CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
         var BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());  
      
           if($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val()=="")
                {
                  BranchID=0;
                }
                else
                {
                  BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
                }
         if($("#ctl00_ContentPlaceHolder1_ddlZonewise").val()=="")
             {
                ZoneID=0;  
              }
                   
          else
             {
                 ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
             }
             window.open("PrintUserListDetails.aspx?dt="+dateFrom+"&dto="+dateto+"&brid="+BranchID+"&zid="+ZoneID, 'Print', "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1");
       }
       
       else if($("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="8")
       {
          var dateFrom= $("#ctl00_ContentPlaceHolder1_TextBox1").val();
        $("Report_criteria").show();
      var dateto=$("#ctl00_ContentPlaceHolder1_txt_21").val();
       var ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
         var CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
         var BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());  
      
           if($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val()=="")
                {
                  BranchID=0;
                }
                else
                {
                  BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
                }
         if($("#ctl00_ContentPlaceHolder1_ddlZonewise").val()=="")
             {
                ZoneID=0;  
              }
                   
          else
             {
                 ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
             }
             window.open("PrintInspectionAgencyList.aspx?dt="+dateFrom+"&dto="+dateto+"&brid="+BranchID+"&zid="+ZoneID, 'Print', "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1");
       }
       
       else if($("#ctl00_ContentPlaceHolder1_ddlSelectCriteria").val()=="9")
       {
         var dateFrom= $("#ctl00_ContentPlaceHolder1_TextBox1").val();
        $("Report_criteria").show();
        var dateto=$("#ctl00_ContentPlaceHolder1_txt_21").val();
       var ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
         var CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
         var BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());  
      var OrderBY;
       var Orderby_user=$("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val();
           if($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val()=="")
                {
                  BranchID=0;
                }
                else
                {
                  BranchID=parseInt($("#ctl00_ContentPlaceHolder1_ddlBranchWise").val());
                }
              if($("#ctl00_ContentPlaceHolder1_ddlZonewise").val()=="")
             {
                ZoneID=0;  
              }
                   
          else
             {
                 ZoneID=parseInt($("#ctl00_ContentPlaceHolder1_ddlZonewise").val());
             }
              if($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val()==null)
             {
               CompanyID=0;
             }
              else
              {
                 CompanyID=parseInt($("#ctl00_ContentPlaceHolder1_ddlcomapnywise").val());
                 
              } 
              if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="1")
                            {
                              OrderBY=1;
                            }
                            if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="2")
                            {
                              OrderBY=2;
                            }
                              if($("#ctl00_ContentPlaceHolder1_DDl_Order_BY").val()=="3")
                            {
                              OrderBY=3;
                            } 
              
            window.open("PRINTREPORTACESSPRINT.aspx?dt="+dateFrom+"&dto="+dateto+"&brid="+BranchID+"&zid="+ZoneID+"&cid="+CompanyID + "&orderby="+OrderBY , 'Print', "left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1");
                        }
                    }

     
 // Created BY Ravish Kumar Pandey
  function openWin(URL) { 
    aWindow = window.open(URL,"composerwindow","toolbar=no,width=350,height=400,status=no,scrollbars=yes,resize=yes,menubar=yes, target=_blank"); 
}    
     
   /* function clrcook(e)
     {
        var evt = window.event ? event : e;
        if (evt.clientY < 0) 
        {
          //  alert("ravish");

            var thecookie = document.cookie.split(";")
            for (var i = 0; i < thecookie.length; i++) 
            {
                var expires = new Date();
                expires.setUTCFullYear(expires.getUTCFullYear() - 1);
                document.cookie = thecookie[i] + '; expires=' + expires.toUTCString() + '; path=/';

            }
           // alert(thecookie);
        }
    }
$(window).bind('beforeunload', function() 
{

    clrcook(event);
}); */
     
     
     
    