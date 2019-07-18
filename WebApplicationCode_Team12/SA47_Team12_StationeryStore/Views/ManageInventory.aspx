<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManageInventory.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.ManageInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2 aria-dropeffect="none">Inventory record</h2>
    <div>
        <div>
            <asp:TextBox ID="SearchTextBox" runat="server" placeholder=" Type to search here..." Width="550px" Height="30px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp
            <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" CssClass="btn btn-primary" Width="76px" />
        </div>
    </div>
    <br />

    <div>
        <asp:GridView DataKeyNames="ItemID" CssClass="table table-striped table-bordered" ID="InventoryGridView" runat="server" AutoGenerateColumns="False"
            OnSelectedIndexChanged="InventoryGridView_SelectedIndexChanged" OnRowCommand="InventoryGridView_RowCommand"
            EmptyDataText="There are no items" OnSelectedIndexChanging="InventoryGridView_SelectedIndexChanging" ShowHeaderWhenEmpty="True"
            AllowPaging="True" OnPageIndexChanging="InventoryGridView_PageIndexChanging" PageSize="15" GridLines="None" CellPadding="4" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="SerialNo" HeaderText="Serial No" SortExpression="SerialNo" />
                <asp:TemplateField HeaderText="Item Number" SortExpression="StockCardDescription">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtItemId_TemplateField" runat="server" Text='<%# Bind("ItemID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="labelItemId_TemplateField" runat="server" Text='<%# Bind("ItemID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CategoryDescription" HeaderText="Category Description" />
                <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" SortExpression="TransactionDate" />
                <asp:BoundField DataField="ActualQty" HeaderText="Actual Qty" SortExpression="AdjustedQty" />
                <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit of Measure" />
                <asp:CommandField SelectText="Details" ShowSelectButton="True" HeaderText="Details" ButtonType="button" />
                <asp:TemplateField HeaderText="Adjust Voucher" ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btnAdjustVoucher" runat="server" CausesValidation="False" CommandName="AdjustVoucher" Text="Adjust Voucher" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#284775" ForeColor="White" />
            <HeaderStyle BackColor="#284775" ForeColor="White" />
            <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
        </asp:GridView>
    </div>
    <asp:GridView CssClass="table table-striped table-bordered" ID="StockCardsGridView" runat="server"
        EmptyDataText="There are no transactions for this item" ShowHeaderWhenEmpty="True"
        AllowPaging="True" OnPageIndexChanging="StockCardsGridView_PageIndexChanging" PageSize="5"
        GridLines="None" CellPadding="4" ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#284775" ForeColor="White" />
        <HeaderStyle BackColor="#284775" ForeColor="White" />
        <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
    </asp:GridView>

</asp:Content>
