<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DH_ReqFreqRpt_Commerce.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.DH_ReqFreqRpt_Commerce" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource11" />
    <CR:CrystalReportSource ID="CrystalReportSource11" runat="server">
        <Report FileName="~/bin/Reports/DH_ReqByDeptSortByFreq_Commerce.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>
