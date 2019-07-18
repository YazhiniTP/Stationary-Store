<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClerkReports.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.ClerkReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 aria-dropeffect="none">Generate Reports </h2>
    <div>
        <p>
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Select Report Type:"></asp:Label>
            &nbsp;&nbsp;
        <asp:DropDownList ID="ClerkReportsDropDownList" runat="server">
            <asp:ListItem>Requests Sort By Date</asp:ListItem> 
            <asp:ListItem>Item Stock</asp:ListItem>
            <asp:ListItem>Item Ordered</asp:ListItem>
            <asp:ListItem>Reorder Stock Forecast</asp:ListItem>
      
        </asp:DropDownList>
            &nbsp;&nbsp;
        <asp:Button ID="ReportButton" runat="server" OnClick="ReportButton_Click" CssClass="btn btn-primary" Text="Generate Report" />
        </p>
    </div>

</asp:Content>
