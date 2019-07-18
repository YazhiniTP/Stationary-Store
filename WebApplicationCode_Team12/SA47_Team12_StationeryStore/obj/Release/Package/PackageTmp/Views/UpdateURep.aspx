<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateURep.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.UpdateURep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-horizontal">
        <h2 aria-dropeffect="none">Assign User Representative</h2>
        <div class="well">
            <asp:Label ID="Label1" runat="server" Text="Current User Representative :" Font-Size="Medium"></asp:Label>
            &nbsp;<asp:Label ID="Label2" runat="server" Font-Size="Medium"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text=" Select Your New User Representative :" Font-Size="Medium"></asp:Label>
            <br />
            <asp:DropDownList ID="URDropDownList" runat="server" Height="22px" Width="233px">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="UpdateButton" runat="server" OnClick="UpdateButton_Click" CssClass="btn btn-primary" Text="Update" />
        </div>
    </div>
</asp:Content>
