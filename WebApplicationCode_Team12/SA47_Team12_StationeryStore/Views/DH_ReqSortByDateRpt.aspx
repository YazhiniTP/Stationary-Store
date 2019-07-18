<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DH_ReqSortByDateRpt.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.DH_ReqSortByDateRpt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" OnInit="CrystalReportViewer1_Init" ReportSourceID="CrystalReportSource29" />
    <CR:CrystalReportSource ID="CrystalReportSource29" runat="server">
        <Report FileName="~/bin/Reports/DH_ReqItemFull.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>
