<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RMA_Questionnaire.aspx.cs" Inherits="RMA_Questionnaire" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NSIC:: POINT OF INTEREST</title>


    <link href="http://www.nsic.co.in/css/bootstrap.min.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/font-awesome/css/font-awesome.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/slick/slick.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/slick/slick-theme.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/dataTables/datatables.min.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/animate.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/style.css?ver=2018" rel="stylesheet" />




</head>
<body>
    <!DOCTYPE HTML>
    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <title>Form Style 10</title>
        <link href='http://fonts.googleapis.com/css?family=Bitter' rel='stylesheet' type='text/css'>
        <style type="text/css">
            body {
                background: #AEE4FF;
            }

            .form-style-10 {
                max-width: 1100px;
                padding: 30px;
                margin: 40px auto;
                background: #FFF;
                border-radius: 10px;
                -webkit-border-radius: 10px;
                -moz-border-radius: 10px;
                box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
                -moz-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
                -webkit-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
            }

                .form-style-10 .inner-wrap {
                    padding: 30px;
                    background: #F8F8F8;
                    border-radius: 6px;
                    margin-bottom: 15px;
                }

                .form-style-10 h1 {
                    background: #2A88AD;
                    padding: 20px 30px 15px 30px;
                    margin: -30px -30px 30px -30px;
                    border-radius: 10px 10px 0 0;
                    -webkit-border-radius: 10px 10px 0 0;
                    -moz-border-radius: 10px 10px 0 0;
                    color: #fff;
                    text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.12);
                    font: normal 30px 'Bitter', serif;
                    -moz-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    -webkit-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    border: 1px solid #257C9E;
                }

                    .form-style-10 h1 > span {
                        display: block;
                        margin-top: 2px;
                        font: 13px Arial, Helvetica, sans-serif;
                    }

                .form-style-10 label {
                    display: block;
                    font: 13px Arial, Helvetica, sans-serif;
                    color: #888;
                    margin-bottom: 15px;
                }

                .form-style-10 input[type="text"],
                .form-style-10 input[type="date"],
                .form-style-10 input[type="datetime"],
                .form-style-10 input[type="email"],
                .form-style-10 input[type="number"],
                .form-style-10 input[type="search"],
                .form-style-10 input[type="time"],
                .form-style-10 input[type="url"],
                .form-style-10 input[type="password"],
                .form-style-10 textarea,
                .form-style-10 select {
                    display: block;
                    box-sizing: border-box;
                    -webkit-box-sizing: border-box;
                    -moz-box-sizing: border-box;
                    width: 90%;
                    padding: 8px;
                    border-radius: 6px;
                    -webkit-border-radius: 6px;
                    -moz-border-radius: 6px;
                    border: 2px solid #fff;
                    box-shadow: inset 0px 1px 1px rgba(0, 0, 0, 0.33);
                    -moz-box-shadow: inset 0px 1px 1px rgba(0, 0, 0, 0.33);
                    -webkit-box-shadow: inset 0px 1px 1px rgba(0, 0, 0, 0.33);
                    height: 50%;
                }

                .form-style-10 .section {
                    font: normal 20px 'Bitter', serif;
                    color: #2A88AD;
                    margin-bottom: 5px;
                }

                    .form-style-10 .section span {
                        background: #2A88AD;
                        padding: 5px 10px 5px 10px;
                        position: absolute;
                        border-radius: 50%;
                        -webkit-border-radius: 50%;
                        -moz-border-radius: 50%;
                        border: 4px solid #fff;
                        font-size: 14px;
                        margin-left: -45px;
                        color: #fff;
                        margin-top: -3px;
                    }

                .form-style-10 input[type="button"],
                .form-style-10 input[type="submit"] {
                    background: #2A88AD;
                    padding: 8px 20px 8px 20px;
                    border-radius: 5px;
                    -webkit-border-radius: 5px;
                    -moz-border-radius: 5px;
                    color: #fff;
                    text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.12);
                    font: normal 30px 'Bitter', serif;
                    -moz-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    -webkit-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    border: 1px solid #257C9E;
                    font-size: 15px;
                }

                    .form-style-10 input[type="button"]:hover,
                    .form-style-10 input[type="submit"]:hover {
                        background: #2A6881;
                        -moz-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.28);
                        -webkit-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.28);
                        box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.28);
                    }

                .form-style-10 .privacy-policy {
                    float: right;
                    width: 250px;
                    font: 12px Arial, Helvetica, sans-serif;
                    color: #4D4D4D;
                    margin-top: 10px;
                    text-align: right;
                }

            .two-col {
                overflow: hidden; /* Makes this div contain its floats */
            }

                .two-col .col1,
                .two-col .col2 {
                    width: 50%;
                }

                .two-col .col1 {
                    float: left;
                }

                .two-col .col2 {
                    float: right;
                }

            /*.two-col label {
                    display: block;
                }*/
        </style>

    </head>

    <body>

        <div class="form-style-10">






            <div style="width: 98%; padding-top: 5px; padding-bottom: 5px;" class="container top-logo-panel">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%; text-align: left">
                            <img src="images/newNS.png" height="100" /></td>
                        <td style="width: 60%; text-align: center">
                            <%-- <div class="col-sm-8 col-xs-12">
                        <div class="text-success">--%>
                            <strong style="color: #0098DA">National Small Industries Corporation</strong>

                            <h4 style="color: #0098DA">(A Government of India Enterprise)</h4>
                            <%-- </div>
                        <div>--%>
                            <%--<b>Registration under Single Point Registration Scheme</b>--%>
                            <br />
                            <br />
                            <br />
                            <b>RAW MATERIAL BANK</b>
                            <%--</div>
                    </div>--%>

                        </td>
                        <td style="width: 20%; text-align: left">
                            <img src="images/msme.png" height="100" width="150" /></td>
                    </tr>
                </table>
               
            </div>







            <form runat="server">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
                <asp:UpdatePanel ID="updSubmt" runat="server">
                    <ContentTemplate>
                         
                        


                        
                         



                        <div class="section"><span>1</span>QUESTIONNAIRE DETAILS  </div>
                        <div class="inner-wrap">
                          <%--  <div class="two-col">
                                <div class="col1">

                                    <label for="txtUAM">
                                        <span style="color: black; font-weight: bold">UAM:</span> <span style="color: orangered">*</span><asp:TextBox ID="txtUAM" runat="server" MaxLength="12" ValidationGroup="validate" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtUAM" ErrorMessage="Required" runat="server" ValidationGroup="Reg" />
                                    </label>



                                </div>
                                <div class="col2">
                                    <label for="txtAadhaar">
                                        <span style="color: black; font-weight: bold">Aadhaar Number: </span><span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtAadhaarNumber" runat="server" MaxLength="12" ValidationGroup="validate" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtAadhaarNumber" ErrorMessage="Required" runat="server" ValidationGroup="Reg" />
                                    </label>
                                </div>
                            </div>--%>

                            <%-- <div class="text-center">
                                            <asp:Button ID="btnf" runat="server" Text="Fetch"   ValidationGroup="validate" /> if you do not have UAM  <a target="_blank" href="http://www.nsicspronline.com" tabindex="-1">Click Here</a>
  

                                            <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="updSubmt">
                                                <ProgressTemplate>
                                                    <img alt="progress" src="Images/Admin/loading.gif" />
                                                    Processing...            
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>--%>
                            <div class="two-col">
                                <div class="col1">
                                </div>
                                <div class="col2">
                                    <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>

                        

                        
                        <div class="inner-wrap">
                            <label>
                                <span style="color: black; font-weight: bold">DETAILS OF RAW MATERIALS REQUIRED</span>
                            </label>
                          
                              <div class="two-col">
                                <div class="col1">

                                    <label for="txtUAM">
                                        <span style="color: black; font-weight: bold">UAM:</span> <span style="color: orangered">*</span><asp:TextBox ID="txtUAM" runat="server" MaxLength="12" ValidationGroup="validate" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtUAM" ErrorMessage="Required" runat="server" ValidationGroup="Reg" />
                                    </label>



                                </div>
                                <div class="col2">
                                    <label for="txtAadhaar">
                                        <span style="color: black; font-weight: bold">Aadhaar Number: </span><span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtAadhaarNumber" runat="server" MaxLength="12" ValidationGroup="validate" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtAadhaarNumber" ErrorMessage="Required" runat="server" ValidationGroup="Reg" />
                                    </label>
                                </div>
                            </div>


                            <div class="two-col">
                                <div class="two-col">
                                    <label for="ddlNsicState">
                                        <asp:GridView ID="grdRMB_Details" ShowFooter="true" Width="100%" AutoGenerateColumns="false" runat="server" EmptyDataText="No Records Available">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:TemplateField ItemStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Center"> 
                                                     <HeaderTemplate>
                                                         <table ><tr><td style="color:black; padding:10px;width:250px" >Name of the Raw Material required
                                                             <br /><span style="font-size:10px; color:maroon">(Enter raw material detailed specification eg. MS Angle ISA 40*40*3 SAIL Expanded Polystyrene 422 LG Chem)</span>
                                                                 </td></tr></table>
                                                     </HeaderTemplate> 
                                                    <ItemTemplate>  
                                                        <asp:TextBox ID="txtNameofRawMaterial" TextMode="MultiLine" runat="server"></asp:TextBox>  
                                                        <asp:RequiredFieldValidator ControlToValidate="txtNameofRawMaterial" ErrorMessage="Required" runat="server" ValidationGroup="Reg" />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>  


                                                 <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"> 
                                                     <HeaderTemplate>
                                                         <table><tr><td style="color:black;padding:10px;width:150px">Price of Material(PMT)
                                                             <br /><span style="font-size:10px; color:maroon">(Please indicate appropriate unit, in place of PMT, whereever required)</span>
                                                                 </td></tr></table>
                                                     </HeaderTemplate> 
                                                    <ItemTemplate>  
                                                        <asp:TextBox ID="txtMaterialPrice" runat="server"></asp:TextBox>  
                                                        <asp:RequiredFieldValidator ControlToValidate="txtMaterialPrice" ErrorMessage="Required" runat="server" ValidationGroup="Reg" />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>  

                                                 <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"> 
                                                     <HeaderTemplate>
                                                         <table><tr><td style="color:black;padding:10px;width:150px">Quantity(in MT.) required per month
                                                             <br /><span style="font-size:10px; color:maroon">(Please indicate appropriate unit, in place of PMT, whereever required)</span>
                                                                 </td></tr></table>
                                                     </HeaderTemplate> 
                                                    <ItemTemplate>  
                                                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>  
                                                        <asp:RequiredFieldValidator ControlToValidate="txtQuantity" ErrorMessage="Required" runat="server" ValidationGroup="Reg" />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>  

                                                <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"> 
                                                     <HeaderTemplate>
                                                         <table><tr><td style="color:black">Name of Suppiler/Producer(s)
                                                             <br /><span style="font-size:10px; color:maroon">(eg. SAIL, NALCO etc.)</span>
                                                                 </td></tr></table>
                                                     </HeaderTemplate> 
                                                    <ItemTemplate>  
                                                        <asp:TextBox ID="txtSupName" runat="server"></asp:TextBox>  
                                                        <asp:RequiredFieldValidator ControlToValidate="txtSupName" ErrorMessage="Required" runat="server" ValidationGroup="Reg" />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>  

                                                <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"> 
                                                     <HeaderTemplate>
                                                         <table><tr><td style="color:black">Alternate Suppilers
                                                             <br /><span style="font-size:10px; color:maroon">(if any)</span>
                                                                 </td></tr></table>
                                                     </HeaderTemplate> 
                                                    <ItemTemplate>  
                                                        <asp:TextBox ID="txtAltSupName" runat="server"></asp:TextBox>  
                                                        <asp:RequiredFieldValidator ControlToValidate="txtAltSupName" ErrorMessage="Required" runat="server" ValidationGroup="Reg" />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>  

                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center"> 
                                                     <HeaderTemplate>
                                                         <table><tr><td style="color:black">Mode of Sourcing
                                                             <br /><span style="font-size:10px; color:maroon">(Please select)</span>
                                                                 </td></tr></table>
                                                     </HeaderTemplate> 
                                                    <ItemTemplate>  
                                                        <asp:DropDownList ID="ddlMode" ForeColor="Black" runat="server" Width="180px" >
                                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Direct from Producer"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Through NSIC"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Through Market Dealer"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Any Other"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ControlToValidate="ddlMode" InitialValue="0" ErrorMessage="Required" runat="server" ValidationGroup="Reg" />
                                                    </ItemTemplate>  

                                                    <FooterStyle HorizontalAlign="Right" />  
                <FooterTemplate>  
                    <asp:Button ID="ButtonAdd" runat="server" Text="Add New" OnClick="ButtonAdd_Click" ValidationGroup="Reg" /> </FooterTemplate>

                                                </asp:TemplateField>  

                                            </Columns>
                                        </asp:GridView>
                                    </label>
                                </div>
                                 
                            </div>

               

                            </div>


                         <div class="inner-wrap">
                            <label>
                                <span style="color: black; font-weight: bold">SUB ASSEMBLY/COMPONENTS AS RAW MATERIAL</span>
                            </label>

                              <div class="two-col">
                                <div class="two-col">
                                    <label for="ddlNsicState">
                                        <asp:GridView ID="grdRMB_SubRaw_Details" ShowFooter="true" Width="100%" AutoGenerateColumns="false" runat="server" EmptyDataText="No Records Available">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-ForeColor="Black">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:TemplateField ItemStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Center"> 
                                                     <HeaderTemplate>
                                                         <table ><tr><td style="color:black; padding:10px;width:250px" >Name of component(s)
                                                             <%--<br /><span style="font-size:10px; color:maroon">(Enter raw material detailed specification eg. MS Angle ISA 40*40*3 SAIL Expanded Polystyrene 422 LG Chem)</span>--%>
                                                                 </td></tr></table>
                                                     </HeaderTemplate> 
                                                    <ItemTemplate>  
                                                        <asp:TextBox ID="txtNameofComponents" TextMode="MultiLine" runat="server"></asp:TextBox>  
                                                        <asp:RequiredFieldValidator ControlToValidate="txtNameofComponents" ErrorMessage="Required" runat="server" ValidationGroup="SubRaw" />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>  


                                                 <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"> 
                                                     <HeaderTemplate>
                                                         <table><tr><td style="color:black;padding:10px;width:150px">Specification
                                                             <br /><span style="font-size:10px; color:maroon">(Size, Code)</span>
                                                                 </td></tr></table>
                                                     </HeaderTemplate> 
                                                    <ItemTemplate>  
                                                        <asp:TextBox ID="txtSpecification" runat="server"></asp:TextBox>  
                                                        <asp:RequiredFieldValidator ControlToValidate="txtSpecification" ErrorMessage="Required" runat="server" ValidationGroup="SubRaw" />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>  

                                                 <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"> 
                                                     <HeaderTemplate>
                                                         <table><tr><td style="color:black;padding:10px;width:150px">Quantity Procured per Months
                                                            <%-- <br /><span style="font-size:10px; color:maroon">(Please indicate appropriate unit, in place of PMT, whereever required)</span>--%>
                                                                 </td></tr></table>
                                                     </HeaderTemplate> 
                                                    <ItemTemplate>  
                                                        <asp:TextBox ID="txtQuantityPPerMonth" runat="server"></asp:TextBox>  
                                                        <asp:RequiredFieldValidator ControlToValidate="txtQuantityPPerMonth" ErrorMessage="Required" runat="server" ValidationGroup="SubRaw" />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>  

                                                <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"> 
                                                     <HeaderTemplate>
                                                         <table><tr><td style="color:black">Name of Suppiler/Address
                                                            <%-- <br /><span style="font-size:10px; color:maroon">(eg. SAIL, NALCO etc.)</span>--%>
                                                                 </td></tr></table>
                                                     </HeaderTemplate> 
                                                    <ItemTemplate>  
                                                        <asp:TextBox ID="txtSupNameAddress" TextMode="MultiLine" runat="server"></asp:TextBox>  
                                                        <asp:RequiredFieldValidator ControlToValidate="txtSupNameAddress" ErrorMessage="Required" runat="server" ValidationGroup="SubRaw" />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>  

                                                <asp:TemplateField ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"> 
                                                     <HeaderTemplate>
                                                         <table><tr><td style="color:black">Alternate Suppilers/Source
                                                             <br /><span style="font-size:10px; color:maroon">(if any)</span>
                                                                 </td></tr></table>
                                                     </HeaderTemplate> 
                                                    <ItemTemplate>  
                                                        <asp:TextBox ID="txtAltSupSource" runat="server"></asp:TextBox>  
                                                        <asp:RequiredFieldValidator ControlToValidate="txtAltSupSource" ErrorMessage="Required" runat="server" ValidationGroup="SubRaw" />
                                                    </ItemTemplate>  

                                                     <FooterStyle HorizontalAlign="Right" />  
                <FooterTemplate>  
                    <asp:Button ID="ButtonAdd" runat="server" Text="Add New" OnClick="ButtonAddSubRaw_Click" ValidationGroup="SubRaw" /> </FooterTemplate>


                                                </asp:TemplateField>  

                                                 

                                            </Columns>
                                        </asp:GridView>
                                    </label>
                                </div>
                                 
                            </div>
                        </div>
                            <div class="button-section">
                                  <asp:Button ID="btnEnroll" runat="server" Text="Submit" CausesValidation="true" ValidationGroup="Enroll" OnClick="BtnSave_Click" />
                            </div>
                            <br />
                            <br />
                            
 
                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>
        </div>

    </body>


    </html>

</body>
</html>
