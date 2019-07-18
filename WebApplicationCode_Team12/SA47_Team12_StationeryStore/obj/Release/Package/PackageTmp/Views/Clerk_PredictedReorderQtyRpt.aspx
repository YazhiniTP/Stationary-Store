<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clerk_PredictedReorderQtyRpt.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.Clerk_PredictedReorderQtyRpt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource24" />
    <CR:CrystalReportSource ID="CrystalReportSource24" runat="server">
        <Report FileName="~/bin/Reports/Clerk_PredictedReorderQty.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>
