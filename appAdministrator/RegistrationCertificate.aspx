<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrationCertificate.aspx.cs" Inherits="appAdministrator_RegistrationCertificate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
<%--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />--%>

    <title>Registration Certificate</title>
 <%--<link type="text/css" rel="Stylesheet" href="../CSS/StyleAdmin1.css" />--%>
  
    <style  type="text/css">
           body {
	margin-left: 10px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	/*background-image: url(../Images/NSIC_Logo1.gif);*/
	background-color:#F8F8F8;
	background-repeat:repeat;
	background-position:0px 0px;
	/*text-align:center;*/
    }
    body,th,p,div,span
        {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 14px;
	
    }
  
         @page:first
             {
     	        margin-top:1.8cm;
             	
             }
         @page:left 
         {
         	margin-top:3.5cm;
             
           /* page-break-before:inherit;*/
        
        }
         @page:right {
            margin-top: 6.5cm;
           page-break-before:inherit;
          }
        
     /*@media print {*/
                  /*  #divtest */
                  /*  { */
                    /*	margin:0 0.5cm 0cm 0.5cm;*/
                    /*	page-break-before:inherit;*/
                    	
                  /*  }  */
             /*   }*/ 
                
           
           .grdturnmrgtop   
           {
           	margin-top:5px;
           }
           .styleIE
       {
       	 width:100%; 
       	 height:190px;
       	  margin-top:55px;/*'changes 5 px'*/
       }
       
        .stylechrome
       {
       	 width:100%; 
       	 height:200px;
       	  margin-top:85px;
       	   /*'changes 85 px'*/ /*'changes 75 px'*/
       	
       }

         .lblRpt
           {
           	 white-space:normal;
           	  word-spacing :20px;
          }
           .tblrowcolor
             {
              background-color:#0055FF;
              
             }
              .lblclass
      {
      	 font-size:14px;
      	  font-family:Arial;
      	  font-weight:bold;
      }
        .lblclassrpt
          {
          	 font-size:14px;
      	  font-family:Arial;
      	  font-weight:bold;
      	  vertical-align:middle;
          }
          
           .RptSNO
             {
            font-size:4px;
      	  font-family:Arial;
      	  font-weight:bold;
      	  vertical-align:middle; 
      	   width:1px; 
             }
             
            .partnername
              {
              	  font-family: Arial,Helvetica,sans-serif;
                       font-size: 11px;
              }
            
                
            .tHead
            {    
                display : table-header-group;    
                border:solid 1px #000000;
                 
            }
            
            .Logo 
            {
             font-family:Verdana;
             font-size:16px;
             font-weight:bold;
             color:#000000;
            }
            
          
        
           .label_Address
            {
            	font-family:Arial, Sans-Serif;
            	font-size:18px;
            	font-weight:bold; 
            }
            .leftHeading_printcertificate
            {
	font-family:Arial, Helvetica, sans-serif;
	font-size: 10px;
	height:15px;
	padding-left:5px;
	padding-right:5px;
	font-weight:bold;
	  text-align:right;
	   padding-left:550px;
        }
          
         .leftHeading_printcertificate_first
            {
	font-family:Arial, Helvetica, sans-serif;
	font-size: 12px;
	height:15px;      
	padding-left:5px;
	padding-right:5px;
	font-weight:bold;
	
        }
        .btnCls_CERTIFICATE
{
	font-family: Arial, Helvetica, sans-serif;
	font-size: 10px;
	background-color:#22518F;
	color:#FFFFFF;
	font-weight:bold;
	width:90px;
	display:none;
}

  .leftHeading_certificate_store
{
	font-family:Arial;
	font-size: 12px;
	height:10px;
	padding-left:5px;
	padding-right:5px;
	font-weight:bold;
}
.dynRowHeadercertificate_grid
{
    
   border:solid 1px #011e0f;
   
}
    .RowClass
  {
 vertical-align:top;


 
   
  }
    .styleclass_ie
       {
       	  width:100%;
       	   height:190px; 
       	   margin-top:30px;
       	   
       }
       
        .styleclass_chrome
          {
          	width:100%;
       	   height:190px; 
       	   margin-top:45px;
       	 
          	
          }
            .tdheightclassIE
          {
          	 height:80px;
          	 margin-top:30px;  /* 5px  28px height:40*/
          }
        .tdHeightclassChrome
             {
                margin-top:10px;	
             	 height:20px;
             }
             
             .tdHeightclassChrome
             {
             	
             	 height:85px;
             	
             }
             
              .td_heightcompresrowie
                {
                	 height:70px;  /*85px*/
                }
                  .td_heightcompresrowchrome
                {
                	 height:90px;  /*100px;*/
                	
                }
          /*  .add_space
              {
              	 margin-top:200px;
              }*/
             .div {
                    page-break-before : always;
                        }
                        
                            .classtrspace
                          {
                          	 display:none;
                            }
                             .classtrspacedisplay
                            {
                            	 display:none;
                             }
                          
                            .classtable_storechrome
                            {
                            	    margin-top:3px;  
                            	
                            	 padding-left:15px;
                            	     /* change width:100%; position:absolute;  

                        padding-left:15px; add new*/
                            	
                            }
                             .classtable_storeIE
                            {
                            	margin-top:3px;  
                            
                            	 padding-right:10px;
                            	 padding-left:2px;
                            }
                            .serial_no_paddingIE
                              {
                              	  padding-right:30px;
                              }
                              
                              .serial_no_paddingChrome
                                {
                                	  padding-right:2px;
                                	 
                                }
                                .Serial_No_StoreIE
                                 {
                                 	 padding-right:25px;
                                 }
                                 .Serial_No_StoreChrome
                                 {
                                 	 padding-right:1px;
                                 	
                                 	 
                                 }
                                 .tbl_width_storeIE
                                  {
                                  	width:100%;
                                  }
                                  .tbl_width_storeChrome
                                  {
                                  	 width:100%;     /**/
                                  
                                  }
 .leftHeading_certificate1
{
	font-family:Arial, Helvetica, sans-serif;
	font-size: 12px;
	height:20px;
	padding-left:3.5px;
	padding-right:5px;
	font-weight:bold;
}

     .leftHeading_certificate1_ref
{
	font-family:Arial, Helvetica, sans-serif;
	font-size: 12px;
	height:20px;
	padding-left:2px;
	
	font-weight:bold;
}
    
      .gapStyleChrome
         {
         	  padding-left: 15px;
         }      
         .gapStyleIE
            {
            	 padding-left:5px;
            }
            .tblbranchtbl
              {
              	 padding-left:15px;
              }
    .leftHeading1
        {
	font-family:Arial, Helvetica, sans-serif;
	font-size: 12px;
	height:25px;
	padding-left:5px;
	padding-right:5px;
	font-weight:bold;
}
    .leftHeading2
       {
       	 font-family:Arial, Helvetica, sans-serif;
	font-size: 14px;
	height:25px;
	padding-left:10px;
	padding-right:5px;
	font-weight:bold;
       }
       .classA
        {
            color: #999;
            padding: 10px;
            width: 250px;
            height: 150px;
            background-color: #fafafa;
            font-size: 12px;
            font-family: verdana;
            border: solid 1px #666;
        }
        </style>
         

    <script src="../JScript/jquery-1.7.1.js" type="text/javascript"></script>
        <script type="text/javascript" language="javascript">

            function CallCondition() {
                alert("Print is not allowed");
                // return false; //word-wrap: break-word;
                window.close();
            }


            $(document).ready(function() {




                /* ===== Firefox ===== */

                // Select Firefox
                if (jQuery.browser.mozilla) {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');


                }

                // Select Firefox 1.5.x to 2.x
                if (jQuery.browser.mozilla && jQuery.browser.version.substr(0, 3) == '1.8') {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select Firefox under 3.x
                if (jQuery.browser.mozilla && jQuery.browser.version < '1.9') {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');

                }

                // Select Firefox 3.0.x and above
                if (jQuery.browser.mozilla && jQuery.browser.version.substr(0, 3) == '1.9') {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select just Firefox 2.0.x
                if (jQuery.browser.mozilla && jQuery.browser.version == '1.8.1') {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select just Firefox 3.0.x
                if (jQuery.browser.mozilla && jQuery.browser.version == '1.9') {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select just Firefox 3.5.x
                if (jQuery.browser.mozilla && jQuery.browser.version == '1.9.1') {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select just Firefox 3.6.x
                if (jQuery.browser.mozilla && jQuery.browser.version == '1.9.2') {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select just Firefox 7.x
                if (jQuery.browser.mozilla && jQuery.browser.version.substr(0, 1) == '7') {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select just Firefox 14.x
                if (jQuery.browser.mozilla && jQuery.browser.version.substr(0, 2) == '14') {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }


                /* ===== Internet Explorer ===== */

                // Select Internet Explorer
                if (jQuery.browser.msie) {
                    // do sth.
                    $('#HederText').addClass('styleIE');
                    $('#td_height').addClass('tdheightclassIE');

                    $('#td_space_tr').addClass('td_heightcompresrowie');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeIE');
                    $('#td_branch').addClass('gapStyleIE');

                    $('#td_annexureclose').addClass('gapStyleIE');
                    $('#td_store_gap').addClass('gapStyleIE');
                    $('#tbl_branch_tbl').addClass('tblbranchtbl');
                }

                // Select Internet Explorer above 6
                if (jQuery.browser.msie && jQuery.browser.version > 6) {
                    // do sth.
                    $('#HederText').addClass('styleIE');
                    $('#td_height').addClass('tdheightclassIE');
                    $('#td_space_tr').addClass('td_heightcompresrowie');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeIE');
                    $('#td_branch').addClass('gapStyleIE');

                    $('#td_annexureclose').addClass('gapStyleIE');
                    $('#td_store_gap').addClass('gapStyleIE');
                    $('#tbl_branch_tbl').addClass('tblbranchtbl');
                }

                // Select Internet Explorer 7 and below
                if (jQuery.browser.msie && jQuery.browser.version <= 7) {
                    $('#HederText').addClass('styleIE');
                    $('#td_height').addClass('tdheightclassIE');
                    $('#td_space_tr').addClass('td_heightcompresrowie');
                    $('#td_storedetails').addClass('classtable_storeIE');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeIE');
                    $('#td_branch').addClass('gapStyleIE');

                    $('#td_annexureclose').addClass('gapStyleIE');
                    $('#td_store_gap').addClass('gapStyleIE');
                    $('#tbl_branch_tbl').addClass('tblbranchtbl');
                    // do sth.
                }

                // Select just Internet Explorer 6
                if (jQuery.browser.msie && jQuery.browser.version == '6.0') {
                    // do sth.
                    $('#HederText').addClass('styleIE');
                    $('#td_height').addClass('tdheightclassIE');
                    $('#td_space_tr').addClass('td_heightcompresrowie');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeIE');
                    $('#td_branch').addClass('gapStyleIE');

                    $('#td_annexureclose').addClass('gapStyleIE');
                    $('#td_store_gap').addClass('gapStyleIE');
                    $('#tbl_branch_tbl').addClass('tblbranchtbl');
                }

                // Select just Internet Explorer 7
                if (jQuery.browser.msie && jQuery.browser.version == '7.0') {
                    // do sth.
                    $('#HederText').addClass('styleIE');
                    $('#td_height').addClass('tdheightclassIE');
                    $('#td_space_tr').addClass('td_heightcompresrowie');
                    $('#td_storedetails').addClass('classtable_storeIE');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeIE');
                    $('#td_branch').addClass('gapStyleIE');

                    $('#td_annexureclose').addClass('gapStyleIE');
                    $('#td_store_gap').addClass('gapStyleIE');
                    $('#tbl_branch_tbl').addClass('tblbranchtbl');
                }

                // Select just Internet Explorer 8
                if (jQuery.browser.msie && jQuery.browser.version == '8.0') {
                    // do sth.
                    $('#HederText').addClass('styleIE');
                    $('#td_height').addClass('tdheightclassIE');
                    $('#td_space_tr').addClass('td_heightcompresrowie');
                    $('#td_storedetails').addClass('classtable_storeIE');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeIE');
                    $('#td_branch').addClass('gapStyleIE');

                    $('#td_annexureclose').addClass('gapStyleIE');
                    $('#td_store_gap').addClass('gapStyleIE');
                    $('#tbl_branch_tbl').addClass('tblbranchtbl');
                }

                // Select just Internet Explorer 9
                if (jQuery.browser.msie && jQuery.browser.version == '9.0') {
                    // do sth.
                    $('#HederText').addClass('styleIE');
                    $('#td_height').addClass('tdheightclassIE');
                    $('#td_space_tr').addClass('td_heightcompresrowie');
                    $('#serial_no_padding').addClass('serial_no_paddingIE');
                    $('#Serial_No_Store').addClass('Serial_No_StoreIE');
                    $('#tbl_width_store').addClass('tbl_width_storeIE');
                    $('#td_branch').addClass('gapStyleIE');

                    $('#td_annexureclose').addClass('gapStyleIE');
                    $('#td_store_gap').addClass('gapStyleIE');
                    $('#tbl_branch_tbl').addClass('tblbranchtbl');
                }


                /* ===== Safari / Chrome ===== */

                // Select Safari / Chrome
                if (jQuery.browser.safari) {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#td_storedetails').addClass('classtable_storechrome');
                    $('#serial_no_padding').addClass('serial_no_paddingChrome');
                    $('#Serial_No_Store').addClass('Serial_No_StoreChrome');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select Safari 3
                if (jQuery.browser.safari && (navigator.appVersion.indexOf('3.') != -1)) {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#td_storedetails').addClass('classtable_storechrome');
                    $('#serial_no_padding').addClass('serial_no_paddingChrome');
                    $('#Serial_No_Store').addClass('Serial_No_StoreChrome');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select Safari 4
                if (jQuery.browser.safari && (navigator.appVersion.indexOf('4.') != -1)) {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#td_storedetails').addClass('classtable_storechrome');
                    $('#serial_no_padding').addClass('serial_no_paddingChrome');
                    $('#Serial_No_Store').addClass('Serial_No_StoreChrome');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select Chrome 1
                if (jQuery.browser.safari && (navigator.appVersion.indexOf('1.') != -1)) {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#td_storedetails').addClass('classtable_storechrome');
                    $('#serial_no_padding').addClass('serial_no_paddingChrome');
                    $('#Serial_No_Store').addClass('Serial_No_StoreChrome');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select Chrome 3
                if (jQuery.browser.safari && (navigator.appVersion.indexOf('3.') != -1)) {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#td_storedetails').addClass('classtable_storechrome');
                    $('#serial_no_padding').addClass('serial_no_paddingChrome');
                    $('#Serial_No_Store').addClass('Serial_No_StoreChrome');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select Chrome 13
                if (jQuery.browser.safari && (navigator.appVersion.indexOf('13.') != -1)) {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#td_storedetails').addClass('classtable_storechrome');
                    $('#serial_no_padding').addClass('serial_no_paddingChrome');
                    $('#Serial_No_Store').addClass('Serial_No_StoreChrome');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }

                // Select Chrome 16
                if (jQuery.browser.safari && (navigator.appVersion.indexOf('16.') != -1)) {
                    // do sth.
                    $('#HederText').addClass('stylechrome');
                    $('#td_height').addClass('tdHeightclassChrome');
                    $('#td_space_tr').addClass('td_heightcompresrowchrome');
                    $('#td_storedetails').addClass('classtable_storechrome');
                    $('#serial_no_padding').addClass('serial_no_paddingChrome');
                    $('#Serial_No_Store').addClass('Serial_No_StoreChrome');
                    $('#tbl_width_store').addClass('tbl_width_storeChrome');
                    $('#td_branch').addClass('gapStyleChrome');

                    $('#td_annexureclose').addClass('gapStyleChrome');
                    $('#td_store_gap').addClass('gapStyleChrome');
                }




            });
     

         
     
 function CallPrint(strid)
   {
    document.getElementById('MainDiv').style.display="block";
document.getElementById('PrintDiv_Button').style.display="none";
      
  var prtContent = document.getElementById(strid);
   var WinPrint = window.open('','','left=0,top=0,width=700px,height=800px,toolbar=0,scrollbars=0,status=0, directories=no,menubar=no, copyhistory=no ,location=no');
  //document.getElementById('div_print_button').style.display ="none" ;

                                                                                 
  WinPrint.document.write(prtContent.innerHTML);
  WinPrint.print();
   WinPrint.document.close();
  // WinPrint.focus();
  
   
   document.getElementById('MainDiv').style.display="none";
  document.getElementById('PrintDiv_Button').style.display="none";
 // document.getElementById('div_print_button').style.display="none";

}
function windowclose() {
    window.close();
}
   
   function showHideDiv()
     {
     document.getElementById('MainDiv').style.display="none";
     document.getElementById('Print_Stores_Details').style.display = "block";
     document.getElementById('tbl_store_Tbl').style.display = "block";
     document.getElementById('tbl_print_deatils').style.display = "block";
     document.getElementById('tablshow').style.display = "block";
       document.getElementById('div_print_button').style.display = "block";
        
       document.getElementById("<%=btnPrnt.ClientID%>").style.display = "block";
       }
     
  



                function AddTHEAD(tableName) {

                    var table = document.getElementById(tableName);

                    if (table != null) {

                        var head = document.createElement("THEAD");

                        head.style.display = "table-header-group";

                        head.appendChild(table.rows[0]);

                        table.insertBefore(head, table.childNodes[0]);

                    }
                }

                $(document).ready(function() {
                    if ($("#tbl_rept tr").length == 2) {
                        $("#Rpt_Address_ctl01_Label2").hide();
                        //  $("#Rpt_Address_ctl01_lblAddress").text().substr(1);
                        var str = $("#Rpt_Address_ctl01_lblAddress").text();
                        var newstr = str.substr(1);
                        $("#Rpt_Address_ctl01_lblAddress").text(newstr);


                    }
                });

                function mypopup() {
                    var id = document.getElementById("<%=hdn_company_id.ClientID%>").value;

                    mywindow = window.open("CompanyCertificatePrintPreview.aspx?id="+id, "mywindow", "location=1,status=1,scrollbars=1,width=800,height=800");
                    mywindow.moveTo(10,10);
                }


                function printDiv(divID) {

                  
                  // var  = document.getElementById("Show_Report_Div");
                    document.getElementById("MainDiv").style.display = "block";
                    document.getElementById("PrintDiv_Button").style.display = "none";
                    // document.getElementById('div_print_button').style.display = "none";
                    document.getElementById("<%=btnPrnt.ClientID%>").style.display = "none";
                    //Get the HTML of div
                    var divElements = document.getElementById(divID).innerHTML;
                    //Get the HTML of whole page
                    var oldPage = document.body.innerHTML;
                     
                    //Reset the page's HTML with div's HTML only
                    document.body.innerHTML = "<html><title></title><body>" + divElements + "</body>";
                    document.body.innerHTML = divElements;
                    alert(divElements);
                    //Print Page
                    window.print();

                    //Restore orignal HTML
                  document.body.innerHTML = oldPage;
                    divElements = document.body;
                        window.close();
                     window.focus();
                         //disable postback on print button
//               document.getElementById('<%= btnPrnt.ClientID %>').disabled = true;
                            
                                     
                   return true;

               }


               function print_showreportdiv(divID) {
                   document.getElementById('MainDiv').style.display = "block";
                   document.getElementById('PrintDiv_Button').style.display = "none";
                   // document.getElementById('div_print_button').style.display = "none";
                   document.getElementById("<%=btnPrnt.ClientID%>").style.display = "none";
                  // var printFriendly = document.getElementById("Show_Report_Div");
                   var divElements = document.getElementById(divID).innerHTML;
                 //  alert(divElements);
                   //Get the HTML of whole page
                   // var oldPage = document.body.innerHTML;
                   document.body.innerHTML = "<html><title></title><body>" + divElements + "</body>";
                   var printWin = window.open("about:blank", "Voucher", "menubar=no;status=no;toolbar=no;");
                   // printWin.document.write("<html><head><title></title></head><body><h1></h1>" + divElements + "</body></html>");
                   printWin.document.write(document.body.innerHTML);
                   printWin.document.close();
                   printWin.window.print();
                   printWin.close();
               }

               function CallDiv() {
                   var divTag = document.createElement("div");
                   divTag.id = "divDyn";
                   divTag.className = "classA";
                   divTag.innerHTML = "Welcome Mr. Ravish Kumar Pandey ";
                   var tableBody = document.getElementById("tbl_store_Tbl");
                   document.body.appendChild(divTag);
                   tableBody.appendChild(divTag);
                  
               }

               function print1_text() {
                   document.getElementById("MainDiv").style.display = "block";
                   document.getElementById("PrintDiv_Button").style.display = "none";
                   document.getElementById("<%=btnPrnt.ClientID%>").style.display = "none";
                   document.getElementById("div_print_button").style.display = "none";

                  
                     
                      
                   


                   if ($.browser.msie) {
                   
                   
                      
                   }
                   else {
                    
                       $('#tbl_store_Tbl').css("overflow", "hidden");
                   }
               
                
              
               
                  window.print();
                   
                }
               
              
        </script>
    
</head>
<body>
    <form id="form1" runat="server" style="width:790px;">  <%--width:790px;--%>
   
   <%--  <div id="Show_Report_Div"   style="width:755px; height:100%; "  > --%> <%--align="center"--%>
      
              
<%--   margin-left:10px;  height:1224px;--%>
    
    
    
<div id="MainDiv"
style="width:100%; margin-left:10px; margin-right: 0px;">
<div id="PrintDiv">

<div id="HederText" >   <%--style="width:100%; height:190px; margin-top:25px;"--%>
<table cellpadding="2" cellspacing="2" border="0" width="100%" >
<tr valign="bottom"><td align="right" colspan="4" valign="bottom"  id="serial_no_padding" > <b> S.No.</b> 
<asp:Label ID="lblserialNo" runat="server" Text=""></asp:Label></td>
 
</tr>
<%--<tr valign="top"><td colspan="4" style="height:2.5cm;"></td></tr>--%>
 <tr >
   <td align="right" valign="top" style="padding-left:90px;" > <b>Branch Office : </b></td>
    <td align="left" colspan="3" valign="top">
      <table cellpadding="0" cellspacing="0" border="0">
        <tr>
          <td colspan="2" valign="top"  style="width:420px;"><b><asp:Label ID="lbl_branch_address" runat="server" Text="" CssClass="leftHeading_certificate1"></asp:Label></b></td>
        </tr>
        <tr>
         <td align="left"  class="leftHeading_certificate1"><b>Ph : <asp:Label ID="lbl_branch_phone" runat="server" Text="" CssClass="leftHeading_certificate1"></asp:Label></b></td>
          <td align="left"  class="leftHeading_certificate1"><b>Fax : <asp:Label ID="lbl_branch_fax" runat="server" Text="" CssClass="leftHeading_certificate1"></asp:Label></b></td>
        </tr>
         <tr>
           <td align="left"   class="leftHeading_certificate1"><b>Email : <asp:Label ID="lbl_branch_email" runat="server" Text="" CssClass="leftHeading_certificate1"></asp:Label></b> </td>
           <td align="left"   class="leftHeading_certificate1"><b>Website : <asp:Label ID="Label1" runat="server" Text="www.nsic.co.in" ></asp:Label></b> </td>
         </tr>
        
      </table>
    
    </td>
 </tr>




</table>

</div>


<div id="Referances" style="width:100%;clear: both; ">
<table cellpadding ="0" cellspacing="0" border="0" style="margin-top:5px;">
<tr>

<td colspan="4" align="center" > <%--class="style1"--%>
<asp:Label ID="lblvalidToFrom" runat="server" Text="" ></asp:Label>
<%--<br />--%>  <%--changes--%>
</td> <%--CssClass="label_Address" --%>
</tr>


<tr style="margin-top:10px ;">
<td class="leftHeading_certificate1" align="left" colspan="2" style="white-space:nowrap;" ><b>Ref.No</b>&nbsp;
<asp:Label ID="lblrefnumber" runat="server" Text="" ></asp:Label><%-- CssClass="label_Address"--%>
</td>

<td class="leftHeading_certificate1" align="right" style="padding-right:20px;">
<asp:Label ID="lblDate" Text="Date:" runat="server"> </asp:Label><asp:Label ID="lbldateform" runat="server" Text="" ></asp:Label> </td> <%--CssClass="label_Address"--%>
<%--<td class="leftHeading" align="left">
<p class="leftHeading">&nbsp;</p>


</td>--%>
</tr>
<%--<tr><td colspan="4" style="height:4px;"></td></tr>--%>
<tr >
<td colspan="2" align="left" valign="top">
<table cellpadding="0" cellspacing="0" border="0" style="width:200px;">
<tr>
<td align="left" valign="top" ><b><asp:Label ID="lblNameCom" runat="server" Text='' ></asp:Label></b></td><%--CssClass="label_Address"--%>

</tr>
<tr>
<td valign="top" ><asp:Label ID="LblCompanyAdress" runat="server" Text='' ></asp:Label></td> <%--CssClass="label_Address"--%>
</tr>
  <tr>
    <td align="left" valign="top"><asp:Label ID="lbl_city_name" runat="server" Text=""></asp:Label></td>
  </tr>
<tr>
<td valign="top" style="white-space:nowrap;"> <asp:Label ID="lblStateName" runat="server" Text='' ></asp:Label> <%--CssClass="label_Address"--%>

<asp:Label ID="lblcompanypin" runat="server" Text=''></asp:Label> </td>
</tr>

</table>


</td>
<td colspan="2" align="left" valign="top" style="height:190px;">
<table cellpadding="0" cellspacing="0" border="0" style=" height:190px;">  <%--style="height:10px;--%>
<%--<tr><td class="leftHeading1" align="left" valign="top" ><b>Factory Address:</b></td></tr>--%>
<tr  valign="top" style=" border-spacing:0; padding:0;" >
<td style="vertical-align:top; padding-left:10px;" valign="top">
    <b>Factory Address:</b><br />
<asp:Repeater ID="Rpt_Address" runat="server" >
          
<HeaderTemplate>

</HeaderTemplate>
<ItemTemplate>
<table cellpadding="0" cellspacing="0" border="0" style=" width:550px;  vertical-align:top;" id="tbl_rept">
<tr valign="top" style="border-spacing:0; padding:0">
<td align="left"  colspan="2" class="leftHeading_certificate1_ref" > 
<asp:Label ID="Label2" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
<asp:Label ID="lblAddress" runat="server" Text='<%#Eval("AddressName")%>'></asp:Label></td>

</tr>
<tr valign="top" style=" border-spacing:0; padding:0;">
  <td  valign="top" align="left"  colspan="2"><asp:Label ID="lbl_citynameUnit" runat="server" Text='<%#Eval("CityName")%>'></asp:Label>
<asp:Label ID="lblState" runat="server" Text='<%#Eval("fGPtbl_MS_Name") %>'></asp:Label> 
<asp:Label ID="lbl_pin" runat="server" Text='<%#Eval("fGPtbl_CUnit_1B_Factory_Zip")%>'></asp:Label>
</td>
<%--<td align="left"><asp:Label ID="lbl_pin" runat="server" Text='<%#Eval("fGPtbl_CUnit_1B_Factory_Zip")%>'></asp:Label>
</td>--%>

</tr>


</table>

</ItemTemplate>
<SeparatorTemplate>

<table cellpadding="0" cellspacing="0" border="0">

<tr>
<td colspan="3" ></td>

</tr>

</table>

</SeparatorTemplate>



</asp:Repeater>

</td>

</tr>
</table>
<%-- <asp:Label ID="lblFactoryAddress" runat="server" Text='' Visible="false"></asp:Label>--%>

</td>



</tr>
<%--<tr><td colspan="4" style="height:10px;"></td></tr>--%>
<tr style="margin-top:10px;">
<td colspan="2" align="left" ><b>Name of the <asp:Label ID="lbl_name_of_DIRECTOR" runat="server" Text=""></asp:Label></b>
<%--<br />--%>
<asp:HiddenField ID="hdnlablName" runat="server" />
<br />

<asp:Label ID="lblpartnaerName" runat="server" Text='' CssClass="partnername"></asp:Label>
</td>
<td colspan="2" class="leftHeading1" align="left"><p> Certificate of Registration under Single

Point Registration Scheme. Units <br />registered

under this scheme are considered to
be at par with those <br />

registered directly with DGS & D.</p> </td> </tr>
<%--<tr> <td colspan="4" style="height:10px"></td></tr>--%>
<tr style="margin-top:10px;">
<td align="left" colspan="4" ><b>Constitution of the firm-</b>
<asp:Label ID="lblconstitutionCode" runat="server" Text="" CssClass="partnername"></asp:Label></td>

</tr>
<tr> <td colspan="4" style="height:5px"></td></tr>
<tr style="margin-top:5px;">
<td colspan="4" style="white-space:nowrap;" align="center"><b>GOVERNMENT



&nbsp;PURCHASE REGISTRATION NO:</b>&nbsp;<b><asp:Label ID="lblRegisNo" runat="server" Text=""></asp:Label></b>

</td>


</tr>
<tr style="margin-top:8px;">
<td colspan="4" style="white-space:nowrap;" align="center" id="old_registration_no" runat="server"><b>(OLD REGISTRATION NO:</b>&nbsp;<b><asp:Label ID="lbl_old_registration_no" runat="server" Text=""></asp:Label>
    )</b></td>


</tr>
<tr> <td colspan="4" style="height:3px;"></td></tr>
<tr style="margin-top:10px;">

<td colspan="4" style="white-space:nowrap;" align="center">
<b>Monetary Limit:</b>
<img src="../Images/RupeeLogoSmall.jpg" alt="RS." /><asp:Label ID="lblMonLimit" runat="server" Text=""></asp:Label>

<img src="../Images/RupeeLogoSmall.jpg" alt="RS." /><asp:Label ID="lblTextRupee" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr> <td colspan="4" style="height:5px"></td></tr>
<%--<tr style="margin-top:10px;">
<td colspan="4">&nbsp;&nbsp;&nbsp;</td>
</tr>--%>

<tr style="margin-top:10px;">
<td colspan="4" align="center" style="height:200px;" >
<asp:Label ID="lbltrunover" Text="TURNOVER (Rupees in Lakhs)" CssClass="leftHeading1" runat="server"></asp:Label>
  <br />


 <asp:GridView ID="grdTurnOver" AutoGenerateColumns="true" runat="server" 
         CellPadding="7" CellSpacing="1" BorderColor="Black" BorderStyle="Solid" CssClass="grdturnmrgtop"
        onrowdatabound="grd_TurnOver_RowDataBound" 
        onrowcreated="grd_Turnover_RowCreated" Height="150px">
                                <%--<HeaderStyle CssClass="GrdviewHeader_certificate" Height="50" />
                                <RowStyle CssClass="GrdviewRowOdd_certificate"  />
                                <AlternatingRowStyle CssClass="GrdviewRowEven_certificate"  />--%>
                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                <Columns>
                                   
                                    
                                </Columns>
                             </asp:GridView>


</td>

</tr>
<%--<tr> <td colspan="4" style="height:10px"></td></tr>--%>

<tr style=" border-spacing:0; padding:0;">
<td colspan="4" align="left">
<p>
Your name has been registered as a SSI Unit eligible for participation in the<b> Central Government Store Purchase <br />Programme</b> as per the 
 <b>Single Point Registration Scheme</b> for the following Items/Stores.

</p>

</td>
</tr>
<%--<tr> <td colspan="4" style="height:2px;"></td></tr>--%>
<tr style=" border-spacing:0; padding:0;">
<td colspan="4" align="left"  style="height:0px; border-spacing:0; padding:0;">
<asp:GridView ID="grdstores" runat="server" AutoGenerateColumns="false"

Width="790"  Height="20" RowStyle-Height="0" 
BorderWidth="1" CellPadding="0" CellSpacing="0"
>
<%--<RowStyle CssClass="dynRowEven" />
<AlternatingRowStyle CssClass="dynRowOdd" />
<HeaderStyle CssClass="dynRowHeader" />--%>



<Columns>


<asp:BoundField DataField="StoresName" HeaderText="Name of the stores" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />
<asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"/>
<asp:BoundField DataField="QualitativeCapacity" HeaderText="Qualitative Capcity" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"/>
<asp:BoundField DataField="QuantitativeCapcity" HeaderText="Quantitative Capacity P.M" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"/>



</Columns>

</asp:GridView>

</td>


</tr>
<tr style=" border-spacing:0; padding:0;"> <%-- style="margin-top:2px;"--%>
<td colspan="4" align="left">
<table cellpadding="0" cellspacing="0" border="1" style="height:100%; width:750px;" align="left">
<tr>
<td class="leftHeading1" align="center">Name of the stores </td>
<td class="leftHeading1" align="center">Specification</td>
<td class="leftHeading1" align="center">Qualitative Capacity</td>
<td class="leftHeading1" align="center">Quantitative Capacity <asp:Label ID="lbl_Quantitive_capacity" runat="server" Text=""></asp:Label></td>

</tr>
</table>

</td>


</tr>
<%--<tr> <td colspan="4" style="height:5px"></td></tr>--%>
<tr>
<td colspan="4" align="center" class="leftHeading_printcertificate_first">"As per List Attached"</td>

</tr>
<tr>
<td colspan="4" align="center" style=" border-spacing:0; padding:0;" class="leftHeading_printcertificate_first">
<asp:Label ID="lblGridCount" runat="server" Text=''></asp:Label>

</td>


</tr>
<%--<tr><td colspan="4"  valign="top"></td></tr>--%> <%--style="height:1px;"--%>
<tr valign="top" style=" border-spacing:0; padding:0;" >

<td colspan="3" align="right"  style="padding-right:25px;  margin-top:2.5cm;"> <br /><br /><br /><br /> <br /><br /><br />Authorised Signatory</td>   <%--class="leftHeading_printcertificate_first"--%>
<td>&nbsp;</td>

</tr>



</table>


</div>

</div>
<div id="PrintDiv_Button" align="center" >
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
    &nbsp;</td>
<td align="center" >
<%-- <asp:Button ID="btn_next" runat="server" Text="Next" onclick="btn_next_Click" />--%>
<input type="button" value="Next Page" onclick="Javascript:showHideDiv();" />
 <%--<asp:Button ID="btn_print_preview" runat="server" Text="Print Preview"   
        OnClientClick="javascript:mypopup();" Visible="False" />--%>
        
     <%-- <input type="button" value="Print Preview"  onclick="javascript:mypopup();" />  --%>
 
  
</td>
</tr>

</table>

</div>



</div>



<div  style="width:100%; margin-top:1px;  display:block;page-break-before:always;   clear:both;"> <%--height:10px;--%>


</div>



<%--<div id="Print_Stores_Details"  style="width:720px; margin-left:4px; margin-top:0px; display:block;" >--%> <%--// width:1000px;--%>


<%--<div id="Main_Heads" style="width:720px; font-size:15px; font-weight:bold; margin-left:20px; margin-top:1px;" align="left">--%>   <%--margin-left:64px;--%>
<%--   -------------------------Old Table Structure------------------------------------%>
 
  
  <%--   ------------------------- End   Old Table Structure------------------------------------%>

<%--<table cellpadding="0" cellspacing="0" border="0" id="Print_Stores_Details"    style="display:block; ">   <%--page-break-before:always; clear:both; --%>
    
<%--    ----------------serial number store and branch office table ----------------------------%>
     <%-- <div id="divtest">--%>
      
      <table cellpadding="0" cellspacing="0" border="0"    id="Print_Stores_Details" style="display:none; "  width="750px">
            <tr>
    <%--style="padding-left:620px;" --%>  <%--style="padding-left:470px;"--%>   <%-- 

style="padding-left:490px;"--%>
 <td >&nbsp;</td>   <%--id="td_space_tr"--%>
   </tr>
  <tr>
<td align="right" valign= "top" id="Serial_No_Store"  style="width:750px;  "  ><b>
   S.No.</b> <asp:Label ID="lblserial_Store" runat="server" Text=""></asp:Label></td>   <%--<br /><br /> --%>
  </tr>
   <tr>
     <td  style="height:25px; width:750px;">&nbsp;</td>  <%--style="height:25px;"--%>
   </tr>  
          
     </table>
    
   <%-- <tr>
      <td align="left"   id="td_annexureclose"  >--%>
      
  
 <%-- <p align="left">   --%>
<table cellpadding="0" cellspacing="0" border="0"  width="780px" id="tablshow"   style="display:none;"  > <%--" align="left" "--%>
  <tr>
  <td width="15px"></td>
      <td   colspan="2">
      
      
      
      
      
           <table cellpadding="0" cellspacing="0" border="0"  width="765px">  <%--id="tbl_branch_tbl"--%>
         <%--<tr><td colspan="2">&nbsp;</td></tr>--%>
         
 <tr><td colspan="3"  id="td_height">&nbsp;</td></tr>

           <tr >
     <td align="left" valign="top" colspan="3"  id="td_branch" > <%-- id="td_branch"--%>
           BRANCH OFFICE   <asp:Label ID="lbl_Branch_Name" runat="server" Text=""></asp:Label> 
     </td>

  </tr>
          <tr >
          <td colspan="3" valign="top" align="left" ><b> <asp:Label ID="lbl_branchaddress" runat="server" 

Text="" CssClass="leftHeading_certificate1_ref"></asp:Label></b></td>
        </tr>
        
        
        <tr >
         <td align="left"   style="width:275px;" class="leftHeading_certificate1_ref" ><b>Ph : <asp:Label 

ID="lbl_phoneAddress" runat="server" Text="" 

CssClass="leftHeading_certificate1_ref"></asp:Label></b></td>
          <td align="left"   style="width:350px;" class="leftHeading_certificate1_ref"><b>Fax : <asp:Label 

ID="lbl_FaxAddress" runat="server" Text="" 

CssClass="leftHeading_certificate1_ref"></asp:Label></b></td>
    <td style="width:130px;">&nbsp;</td>
        </tr>
        
        
        
         <tr >
           <td align="left"    style="width:275px;" class="leftHeading_certificate1_ref" ><b>Email : <asp:Label 

ID="lbl_EmailAddress" runat="server" Text="" 

CssClass="leftHeading_certificate1_ref"></asp:Label></b> </td>
           <td align="left"   style="width:350px;"  class="leftHeading_certificate1_ref" ><b>Website : <asp:Label 

ID="lbl_website" runat="server" Text="www.nsic.co.in" ></asp:Label></b> </td>
<td style="width:130px;">&nbsp;</td>
         </tr>
         </table>
         
         
         
         
         
         
         
      </td>
    
  
  </tr>


<tr> <%-- margin-left:4px;--%>

 <td style="width:15px;" ></td>
<td align="left" > ANNEXURE TO GOVERNMENT PURCHASES ENLISTMENT CERTIFICATE </td>


</tr>
<tr>
 <td style="width:15px;"></td>
<td align="left"  > NO.&nbsp; <asp:Label ID="lblregistration_number" runat="server" Text= '' 

CssClass="lblclass"></asp:Label>&nbsp; D.T.&nbsp;<asp:Label ID="lbldatestore" runat="server" 

Text='' ></asp:Label> </td> <%--CssClass="Lblclass"--%>
</tr>
<tr><td colspan="2" style="height:1px;"></td></tr>
<tr>
 <td style="width:15px;"></td>
<td align="left" > ISSUED TO&nbsp; <asp:Label ID="lblCompanyNameStore" runat="server" Text='' 

CssClass="lblclass"></asp:Label> <asp:Label ID="lblstanameStore" runat="server" Text='' 

></asp:Label></td> <%--CssClass="Lblclass"--%>
</tr>
<%--<tr><td style="height:4px;"></td></tr>--%>

</table>
<%--</p>--%>
      
     <%-- </td>
    
    </tr>
  </table>--%> 
      <%--  ---------------- End  serial number store and branch office table --------------------------  --%>
      
      
      <%--    ----------------  start branch strore Details Table ----------------------------%>
   <%--   <p align="left">  --%> 
        <table cellpadding="0" cellspacing="0" border="0"     id="tbl_store_Tbl"  style="display:none; " > <%-- align="left" --%>
             
              
             <tr>
    <td style="width:15px;">&nbsp;</td>
      <td     >  <%--id="td_store_gap"--%>
    <%--   ///this side table// tha  but --%>
      
   <table cellpadding="0" cellspacing="0" border="0" style="  font-family:Arial; font-weight:bold; ">  <%--  table-layout:fixed; margin-top:2px; --%>


  <tr  >
        <td    id="td_placeholder" runat="server"  >   <%-- id="td_storedetails"--%>
         <%--style="margin-top:3px;  width:100%;"--%>
<%--<div id="td_placeholder"  align="left" >
     <asp:placeholder id="PlaceHolder1" runat="server"></asp:placeholder> 
         
   </div>  --%>
   
    
   
    </td>
 
  </tr>
        
        </table>
      
      
      </td>
      </tr>
      </table>
     <%--</p>--%>
      
       <%--    ---------------- end branch strore Details Table ----------------------------%>
       
       
         <%--start table   for authorized signatory and print  details--%>
         
         <%-- <p align="left"> --%>
         <table cellpadding="0" cellspacing="0" border="0"   width="750px" id="tbl_print_deatils"  style="display:none;">
           <tr><td  height="50px">&nbsp;</td></tr>
    <tr>
      <%--<td align="left"  style="width:425px;">&nbsp;</td>--%>
        <td align="right" width="750px" style="  padding-right:5px;" class="Serial_No_StoreChrome"><br /><br /> <br /><br /><br />Authorised Signatory</td>
    </tr>
<tr>
  
  
  
    <td align="center" width="750px">
      <div  id="div_print_button" style="display:none;vertical-align:top;" > 
   <%-- <input type="button" value="print"  

OnClientClick="javascript:print_showreportdiv()('Show_Report_Div');" 

onclick="javascript:CallPrint('Show_Report_Div');" />--%>
  
   <asp:Button runat="server" ID="btnPrnt"   Text="Print"  OnClick="btnPrnt_Click"   

OnClientClick="javascript:print1_text();"  
              CssClass="btnCls_CERTIFICATE"  />   <%--CssClass="btnCls_CERTIFICATE" --%>
   <asp:Label ID="lbl_serail_add_no" runat="server" Text="" Visible="false"></asp:Label>
   <asp:HiddenField ID="hdn_company_id" runat="server" />
      <%--<input type="button" value="print" onclick="PrintDiv();" />--%>
 </div>
    
    </td>
    
  </tr>
  </table>

    <%--     </p>--%>
       
         <%--End  table   for authorized signatory and print  details--%>
      
  <%--    </div>--%>







    
    </form>
</body>
</html>
