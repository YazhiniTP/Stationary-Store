<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PendingVouchers.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.PendingVouchers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 aria-dropeffect="none">Pending Vouchers</h2>
    <br />
    <asp:Label ID="lblSelection" runat="server" Text="Select to view all or latest:" Font-Size="Large"></asp:Label>
    &nbsp;&nbsp;<br />
    &nbsp;<asp:RadioButton ID="radioButtonAll" runat="server" AutoPostBack="True" Text="Select All" GroupName="radioButtonViews" />
    &nbsp;&nbsp;
    <asp:RadioButton ID="radioButtonLatest" runat="server" Checked="True" Text="Select Latest" GroupName="radioButtonViews" AutoPostBack="True" />
    <br />
    <br />
    <asp:GridView ID="PendingVouchersGridView" DataKeyNames="VoucherId" CssClass="table table-striped table-bordered" runat="server"
        AutoGenerateColumns="False" OnRowDeleting="PendingVouchersGridView_RowDeleting"
        EmptyDataText="There are no pending vouchers" ShowHeaderWhenEmpty="True"
        OnSelectedIndexChanging="PendingVouchersGridView_SelectedIndexChanging" OnSelectedIndexChanged="PendingVouchersGridView_SelectedIndexChanged"
        AllowPaging="True" OnPageIndexChanging="PendingVouchersGridView_PageIndexChanging" PageSize="5"
        GridLines="None" CellPadding="4" ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="SerialNo" HeaderText="Serial No." />
            <asp:BoundField DataField="VoucherId" HeaderText="Voucher Id" />
            <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:CommandField ButtonType="Button" HeaderText="Details" SelectText="Details" ShowSelectButton="True" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" HeaderText="Delete" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#284775" ForeColor="White" />
        <HeaderStyle BackColor="#284775" ForeColor="White" />
        <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
    </asp:GridView>
    <br />
    <br />
    <asp:GridView ID="PendingVoucherItemsGridView" runat="server" CssClass="table table-striped table-bordered"
        AutoGenerateColumns="False" EmptyDataText="There are no items."
        AllowPaging="True" OnPageIndexChanging="PendingVoucherItemsGridView_PageIndexChanging" PageSize="5"
        GridLines="None" CellPadding="4" ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="VoucherDetailId" HeaderText="Voucher Detail Id" Visible="False" />
            <asp:BoundField DataField="SerialNo" HeaderText="Serial No." />
            <asp:BoundField DataField="ItemId" HeaderText="Item Code" />
            <asp:BoundField DataField="AdjustedQty" HeaderText="Adjusted Qty" />
            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#284775" ForeColor="White" />
        <HeaderStyle BackColor="#284775" ForeColor="White" />
        <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
    </asp:GridView>

</asp:Content>
