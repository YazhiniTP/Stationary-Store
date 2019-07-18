<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignRoles.aspx.cs" Inherits="SA47_Team12_StationeryStore.AssignRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="well">
        <div class="col-md-6">
    <asp:Label ID="Label1" runat="server" Text="List of users"></asp:Label>
            <br />
         <asp:ListBox ID="UserName" runat="server" Height="150px" Width="300px"></asp:ListBox>   
            </div>
        <div class="col-md-6">
    
            <asp:Label ID="Label2" runat="server" Text="Assign Role"></asp:Label>
            
    <br />
    <br />
    
    <asp:RadioButtonList ID="AssignRolesRadioButtonList" runat="server" Height="20px" Width="150px">
    </asp:RadioButtonList>
    <br />
            </div>
    &nbsp;&nbsp;<asp:Button ID="AssignRolesButton" runat="server" Text="Assign Role" OnClick="AssignRolesButton_Click" class="btn btn-primary"/>
    </div>
    sad
</asp:Content>
