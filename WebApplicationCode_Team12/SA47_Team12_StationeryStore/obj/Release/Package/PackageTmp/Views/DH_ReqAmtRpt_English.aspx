<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DH_ReqAmtRpt_English.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.DH_ReqAmtRpt_English" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource19" />
    <CR:CrystalReportSource ID="CrystalReportSource19" runat="server">
        <Report FileName="~/bin/Reports/DH_ReqByDeptSortByTotalAmt_English.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>
