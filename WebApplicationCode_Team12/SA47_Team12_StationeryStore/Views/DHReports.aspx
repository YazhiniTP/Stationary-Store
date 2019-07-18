<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DHReports.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.DHReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 aria-dropeffect="none">Generate Reports </h2>
    <div>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Select Report Type:"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="DHReportsDropDownList" runat="server">
            <asp:ListItem>Requests Sort by Date</asp:ListItem>
            <asp:ListItem>Requests Sort by Frequency</asp:ListItem>
            <asp:ListItem>Requests Sort by Total Amt</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:Button ID="ReportButton" runat="server" OnClick="ReportButton_Click" CssClass="btn btn-primary" Text="Generate Report" />
    </div>

</asp:Content>
