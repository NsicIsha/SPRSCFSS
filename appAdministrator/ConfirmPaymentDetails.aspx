<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="ConfirmPaymentDetails.aspx.cs" Inherits="appAdministrator_ConfirmPaymentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" >
 <tr>
            <td align="left" valign="top" style="padding-left:10px; padding-right:10px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowStep">
                            Payment Details
                        </td>
                    </tr>
                   
                   <%-- <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Registration Fees Slab:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblUnitSlabName" runat="server" ></asp:Label>
                        </td>
                    </tr>
                     
                    <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Inspection Fees Slab:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblAgencySlabName" runat="server" ></asp:Label>
                        </td>
                    </tr>--%>
                  
                     <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Registration Fees:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblUnitAmnt" runat="server" ></asp:Label>
                        </td>
                    </tr>
                     <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Inspection Fees:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblAgencyAmnt" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Sub Total:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblSubTot" runat="server" ></asp:Label>
                        </td>
                    </tr><tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Service Tax:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblServiceTax" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    
                     <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Total amount paid:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblTotAmnt" runat="server" ></asp:Label>
                        </td>
                    </tr>
                   <%-- <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>--%>
                    
                    <tr align="left" valign="top">
                        <td  align="left" valign="top" class="leftHeading" style="width:200px;">
                            Mode of Payment
                        </td>
                         <td align="left" valign="top">
                           <asp:Label ID="lblmode" runat="server" ></asp:Label>
                        </td>
                    </tr>
                     <tr align="left" valign="top" id="trcash" runat="server" >
                        <td  align="left" valign="top" class="leftHeading" style="width:200px;">
                            Cash Receipt No
                        </td>
                         <td align="left" valign="top">
                           <asp:TextBox ID="txtCashRecpt" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                     <tr align="left" valign="top" id="trcheck" runat="server" >
                        <td  align="left" valign="top" class="leftHeading" style="width:200px;">
                            Cheque/DD No
                        </td>
                         <td align="left" valign="top">
                           <%--<asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>--%>
                           <asp:Label ID="lblddno" runat="server" ></asp:Label>
                        </td>
                    </tr>
                     <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>
                     <tr align="left" valign="top">
                        <td  align="left" valign="top" class="leftHeading" style="width:200px;">
                           
                        </td>
                         <td align="left" valign="top">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                 onclick="btnSubmit_Click" />
                        </td>
                    </tr>
                        
                    
                </table>
             </td> 
         </tr> 
 
 </table>
         
</asp:Content>

