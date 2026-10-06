<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Login.master" AutoEventWireup="true" CodeFile="CheckStatus.aspx.cs" Inherits="CheckStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="placeholder" runat="Server">
    <div class="container">
        <div class="row ">
            <div class="col-lg-12 fa-border ">
                <div class="ibox ">
                    <div class="ibox-title">                       
                        <h4 class="font-bold ">Check Status of SPRS Application</h4>
                    </div>
                    <div class="ibox-content">
                        <span>Enter PAN:</span>
                        <asp:textbox id="txtSearchbox" runat="server" class="form-control"></asp:textbox>
                        <br />
                        <asp:button id="btnSearch" runat="server" text="Search" class="btn btn-info" Onclick="btnSearch_Click"  />                       
                    </div>                    
                </div>

            </div>
        </div>
<br/><br/>
        <div class="row">
            <table class="table table-bordered" border="1">
                <tr >
                    <td>Company Name:</td><td><asp:Label runat="server" id="lblCompanyName" text="ABC ENGINEERING" ></asp:Label></td>
                     <td>Associated NSIC Branch:</td><td><asp:Label runat="server" id="lblBranchName" text="ABC ENGINEERING" ></asp:Label></td>
                </tr>
                <tr >
                    <td>Enterprise Category:</td><td><asp:Label runat="server" id="lblECat" text="ABC ENGINEERING" ></asp:Label></td>
                     <td>Social Category:</td><td><asp:Label runat="server" id="lblSCat" text="ABC ENGINEERING" ></asp:Label></td>
                </tr>
                <tr>
                    <td>Application Date</td><td><asp:Label runat="server" id="lblAppDate" text="ABC ENGINEERING" ></asp:Label></td>
                    <td>Inspection Allotment Date:</td><td><asp:Label runat="server" id="lblIADate" text="ABC ENGINEERING" ></asp:Label></td>
                </tr>
                <tr>
                    <td>Inspection Agency Involved:</td><td><asp:Label runat="server" id="lblIAName" text="ABC ENGINEERING" ></asp:Label></td>
                    <td>Inspection Passed & Finance Setting Monetary Limit:</td><td><asp:Label runat="server" id="lblIPML" text="ABC ENGINEERING" ></asp:Label></td>
                </tr>
                <tr >
                    <td colspan="2">Registration Completed on</td><td>Hello</td>

                </tr>
            </table>
        </div>
    </div>
</asp:Content>

