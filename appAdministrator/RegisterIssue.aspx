<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminNew.master" AutoEventWireup="true" CodeFile="RegisterIssue.aspx.cs" Inherits="appAdministrator_RegisterIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">

    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>SINGLE POINT REGISTRATION - SPRS</title>

    <link href="../CSS/bootstrap.min.css" rel="stylesheet">
    <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet">

    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-default" style="margin-top: 5px; margin-left: 5px; margin-right: 5px">
        <div class="panel-heading">
            <h4>Log a Ticket</h4>
        </div>
        <div class="panel-body">
            <div class="col-lg-12">
                <div class="col-lg-6">
                    <span>Ticket Title <span style="color: Red; ">*</span></span>
                    <asp:TextBox ID="txtBugTitle" runat="server" CssClass="form-control" placeholder="--Ticket Title max(500 character)--" />
                    <asp:RequiredFieldValidator ID="rqd1" runat="server" ControlToValidate="txtBugTitle" CssClass="alert-danger" ErrorMessage="Please enter bug title" Display="Dynamic" />
                </div>
                <div class="col-lg-6">
                    <span>Project Name <span style="color: Red; font-size: small;">*</span></span>
                    <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control">
                        <asp:ListItem Text="SPRS" Value="SPRS" Selected="True" />
                    </asp:DropDownList>
                </div>
               
               <div class="col-lg-6">
                    <span>Priority<span style="color: Red; font-size: small;">*</span></span>
                    <asp:DropDownList ID="ddlBugPriority" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--Select--" Value="NULL" />
                        <asp:ListItem Text="Urgent" Value="Urgent" />
                        <asp:ListItem Text="High" Value="High" />
                        <asp:ListItem Text="Normal" Value="Normal" />
                        <asp:ListItem Text="Low" Value="Low" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="NULL" ControlToValidate="ddlBugPriority" CssClass="alert-danger" ErrorMessage="Please enter bug priority" Display="Dynamic" />
                </div>
                 <div class="col-lg-6">
                    <span>Ticket Reporter:<span style="color: Red; font-size: small;">*</span></span>
                    <asp:TextBox ID="txtBugReporter" runat="server" CssClass="form-control" ReadOnly="true" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBugReporter" CssClass="alert-danger" ErrorMessage="Please enter bug reporter" Display="Dynamic" />
                </div>
                <div class="col-lg-6">
                    <span>EmailID:<span style="color: Red; font-size: small;">*</span></span>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="true" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmail" CssClass="alert-danger" ErrorMessage="Please enter bug reporter" Display="Dynamic" />
                </div>
                  <div class="col-lg-6">
                    <span>Assigned To<span style="color: Red; font-size: small;">*</span></span>
                    <asp:DropDownList ID="ddlAssignedTo" runat="server" CssClass="form-control" Enabled="false">
                        <asp:ListItem Text="--Select--" Value="NULL" />
                        <asp:ListItem Text="IT Team" Value="IT" Selected="True" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="NULL" ControlToValidate="ddlAssignedTo" CssClass="alert-danger" ErrorMessage="Please select assignee for bug" Display="Dynamic" />
                </div>
               
                <div class="col-lg-6">
                    <span>PAN<span style="color: Red; font-size: small;">*</span></span>
                    <asp:TextBox ID="txtBugPan" runat="server" CssClass="form-control" placeHolder="--PAN of the Unit--" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBugPan" CssClass="alert-danger" ErrorMessage="Please enter PAN of unit" Display="Dynamic" />
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBugPan" Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$" ></asp:RegularExpressionValidator>
                </div>
                <div class="col-lg-6">
                    <span>Ticket Description<span style="color: Red; font-size: small;">*</span></span>
                    <asp:TextBox ID="txtSTR" runat="server" CssClass="form-control" TextMode="MultiLine" PlaceHolder="--Description of the Ticket--" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSTR" CssClass="alert-danger" ErrorMessage="Please enter bug description" Display="Dynamic" />
                </div>
                <div class="col-lg-6">
                    <span>Special Notes</span>
                    <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" PlaceHolder="--Any Special Comment--" />
                </div>
                <div class="col-lg-6">
                    <span>Attachment:</span>
                    <asp:FileUpload ID="fileuploadAnnexureD" runat="server" CssClass="form-control" Style="min-width: 100%" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="NULL" ControlToValidate="fileuploadAnnexureD" CssClass="alert-danger" ErrorMessage="Please attach required document" Display="Dynamic" />
                </div>

            </div>
            <div class="col-lg-12"></div>
            <div class="text-center" id="divSubmitbutton" runat="server">
                <asp:Label ID="lblSubmitMsg" runat="server" CssClass="text-danger" />
                <asp:Button ID="btnSubmit" Text="Log the Ticket" runat="server" CssClass="btn  btn-warning" OnClick="btnSubmit_Click" CausesValidation="true" />
            </div>
        </div>
    </div>

</asp:Content>

