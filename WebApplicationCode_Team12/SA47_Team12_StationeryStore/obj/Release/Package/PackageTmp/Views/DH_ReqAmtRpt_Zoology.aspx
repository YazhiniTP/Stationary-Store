<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DH_ReqAmtRpt_Zoology.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.DH_ReqAmtRpt_Zoology" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource23" />
    <CR:CrystalReportSource ID="CrystalReportSource23" runat="server">
        <Report FileName="~/bin/Reports/DH_ReqByDeptSortByTotalAmt_Zoology.rpt">
        </Report>
    </CR:CrystalReportSource>

</asp:Content>
