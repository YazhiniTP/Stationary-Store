<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupervisorReports.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.SupervisorReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 aria-dropeffect="none">Generate Reports </h2>

    <div>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Select Report Type:"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="SupReportsDropDownList" runat="server" Width="111px">
            <asp:ListItem>Item Stock</asp:ListItem>
            <asp:ListItem>Item Ordered</asp:ListItem>
            <asp:ListItem>Item Requests (Across Dept)</asp:ListItem>
            <asp:ListItem>Item Requests (By Dept)</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:Button ID="ReportButton" runat="server" OnClick="ReportButton_Click" CssClass="btn btn-primary" Text="Generate Report" />
    </div>

</asp:Content>
