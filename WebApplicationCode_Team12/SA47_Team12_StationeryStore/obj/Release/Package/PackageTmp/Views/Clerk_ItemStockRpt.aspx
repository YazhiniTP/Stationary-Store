<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clerk_ItemStockRpt.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.Clerk_ItemStockRpt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <p>
        <br />
    </p>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource4" />
    <p>
        <CR:CrystalReportSource ID="CrystalReportSource4" runat="server">
            <Report FileName="~/bin/Reports/Clerk_ItemStockRpt.rpt">
            </Report>
        </CR:CrystalReportSource>
    </p>
    <p>
    </p>
    <p>
    </p>

</asp:Content>
