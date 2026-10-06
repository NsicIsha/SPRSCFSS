<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <p class="MsoNormal" style="mso-margin-top-alt:auto;mso-margin-bottom-alt:auto;
line-height:normal;background:white">
        <span style="font-size: 10.5pt; font-family: &quot;Segoe UI&quot;, sans-serif;">
            Dear
            Sir,<o:p></o:p>
        </span>
    </p><p class="MsoNormal" style="mso-margin-top-alt:auto;mso-margin-bottom-alt:auto;
line-height:normal;background:white">
        <span style="font-size: 10.5pt; font-family: &quot;Segoe UI&quot;, sans-serif;">
            Greetings
            from NSIC SPRS Team.<o:p></o:p>
        </span>
    </p><p class="MsoNormal" style="mso-margin-top-alt:auto;mso-margin-bottom-alt:auto;
line-height:normal;background:white">
        <span style="font-size: 10.5pt; font-family: &quot;Segoe UI&quot;, sans-serif;">
            We
            acknowledge with thanks that NSIC&nbsp;
        </span>$$AutorizedName$$&nbsp;<span style="font-family: &quot;Segoe UI&quot;, sans-serif; font-size: 10.5pt;">
            has accepted your application and uploads for registration with NSIC under
            Single Point Registration Scheme (SPRS). You are requested to follow below link
            and complete selection of Inspection Agency and submit fees.
        </span>

    </p>
    
    <p class="MsoNormal"><a target="_blank" href="http://www.nsicspronline.com/appAdministrator/NewUnitRegistrationStep6Online.aspx"> http://www.nsicspronline.com/appAdministrator/NewUnitRegistrationStep6Online.aspx </a><o:p></o:p></p>
    
    <p> Bank Details are given below for making payment&nbsp;</p>
  <%--  <p class="MsoNormal" style="mso-margin-top-alt:auto;mso-margin-bottom-alt:auto;
line-height:normal;background:white"><span style="font-family: &quot;Segoe UI&quot;, sans-serif; font-size: 10.5pt;"><br /></span></p>
    --%>
    
    
    <table align="center" border="1" cellpadding="5" cellspacing="5" width="80%" style="padding: 2px; border-width: 2px;"><tbody>
        <tr><td colspan="4" style="font-size:20px" align="center"> Branch Contact Person Details</td></tr>
        <tr><td>&nbsp; Name of Contact Person</td><td>Address</td><td colspan="2">Mobile Number</td></tr>
        <tr><td>$$AutorizedName$$</td><td>$$Address$$</td><td colspan="2">$$MobileNumber$$</td></tr>


        <tr><td colspan="4" style="font-size:20px" align="center">Bank Details</td></tr>

         <tr><td>&nbsp; Bank Name</td><td>Branch</td><td colspan="2">Bank Address</td></tr>
        <tr><td>$$BankName$$</td><td>$$Branch$$</td><td colspan="2">$$BranchAddress$$</td></tr>

        <tr><td colspan="4" style="font-size:20px" align="center">Account Details</td></tr>
         <tr><td>&nbsp; Account Type</td><td>IFSC CODE</td><td>Account Number</td><td>MICR</td></tr>
        <tr><td>$$AccountType$$</td><td>$$IFSCCODE$$</td><td>$$AccountNumber$$</td><td>$$MICR$$</td></tr>


         <tr><td colspan="4" style="font-size:20px; color:red" align="center">Please verify above details with branch before making payment</td></tr>

        <%-- <tr><td colspan="4" style="font-size:20px;" align="center">http://www.nsicspronline.com/appAdministrator/NewUnitRegistrationStep6Online.aspx</td></tr>--%>

                                                                            </tbody></table><p class="MsoNormal" style="mso-margin-top-alt:auto;mso-margin-bottom-alt:auto;
line-height:normal;background:white">

    </p><p class="MsoNormal" align="center" style="mso-margin-top-alt:auto;mso-margin-bottom-alt:
auto;text-align:center;line-height:normal;background:white"><span style="font-size: 10.5pt; font-family: &quot;Segoe UI&quot;, sans-serif;">OR<o:p></o:p></span></p><p class="MsoNormal" style="mso-margin-top-alt:auto;mso-margin-bottom-alt:auto;
line-height:normal;background:white">

    </p><p class="MsoNormal" style="mso-margin-top-alt:auto;mso-margin-bottom-alt:auto;
line-height:normal;background:white">
        <span style="font-size: 10.5pt; font-family: &quot;Segoe UI&quot;, sans-serif;">
            The
            selected branch can also help you in Inspection agency selection and resolving
            any fees related query. Please feel free to contact on below details for
            queries.<o:p></o:p>
        </span>
    </p><p class="MsoNormal"><b>&nbsp;</b></p><p></p>

    <table style="padding:2px; border-width:2px;" border="1" width="80%" align="center">
        <tbody>


            <tr><td>&nbsp; Name</td><td>Mobile</td><td>Email-Id</td><td>Address</td></tr>

            <tr><td>$$AutorizedName$$</td><td>$$Mobile$$</td><td>$$Email$$</td><td>$$Address$$</td></tr>
        </tbody>
    </table>





    <p class="MsoNormal"><span style="font-size: 10.5pt; line-height: 107%; font-family: &quot;Segoe UI&quot;, sans-serif;">Thanking you &amp; assuring you our best service always</span></p><p class="MsoNormal" style="margin-bottom:0cm;margin-bottom:.0001pt">
        <span lang="EN-US">
            <b>&nbsp;</b><br />
            <b>
                SPRS Team <br />
                National Small Industries Corporation Ltd.<br />
                Head Office
            </b>

        </span>
    </p>
    </form>
</body>
</html>
