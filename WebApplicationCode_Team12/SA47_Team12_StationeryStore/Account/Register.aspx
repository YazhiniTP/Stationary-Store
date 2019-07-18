<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SA47_Team12_StationeryStore.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">

        <h4>Create a new account</h4>
        <hr />
        <div class="well">
            
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">Name</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Name" CssClass="form-control" TextMode="SingleLine" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                        CssClass="text-danger" ErrorMessage="Name field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                        CssClass="text-danger" ErrorMessage="The email field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                        CssClass="text-danger" ErrorMessage="The password field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                    <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Address" CssClass="col-md-2 control-label">Address</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Address" CssClass="form-control" TextMode="MultiLine" Height="75px" Width="650px" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Phone" CssClass="col-md-2 control-label">Phone</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="Phone" CssClass="form-control" TextMode="Phone" />
                            <asp:RegularExpressionValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Phone" 
                                ValidationExpression="\d{8}" ErrorMessage="Please input 8 digit phone number" ForeColor="Red"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Phone"
                        CssClass="text-danger" ErrorMessage="The phone field is required." />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-2 control-label">
                    <asp:Label ID="Label1" runat="server" Text="Select your department" Font-Bold="True" Font-Size="15px"></asp:Label>
                </div>
                <div class="col-sm-7">
                    <asp:RadioButtonList ID="DeptRadioButtonList" runat="server" DataSourceID="SS_Department" DataTextField="Description" DataValueField="DepartmentID" Height="16px" Width="315px">
                        <asp:ListItem>English Dept</asp:ListItem>
                        <asp:ListItem>Computer Science Dept</asp:ListItem>
                        <asp:ListItem>Commerce Dept</asp:ListItem>
                        <asp:ListItem>Registrar Dept</asp:ListItem>
                        <asp:ListItem>Zoology Dept</asp:ListItem>
                        <asp:ListItem>Store</asp:ListItem>
                        <asp:ListItem>Purchasing Dept</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <asp:SqlDataSource ID="SS_Department" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [DepartmentID], [Description] FROM [Department]"></asp:SqlDataSource>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-primary" />
                </div>
            </div>

        </div>
    </div>

</asp:Content>
