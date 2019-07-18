<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaiseRequest.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.RaiseRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2 aria-dropeffect="none">Raise Request</h2>
    <br />
    <div class="row">
        <div class="col-sm-4" >
            <asp:TextBox ID="TextBoxSearch" placeholder=" Type item to search here..." runat="server" Width="500px" Height="30px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="ButtonSearch" runat="server" OnClick="ButtonSearch_Click" CssClass="btn btn-primary" Text="Search" />
        </div>
        <div class="col-sm-5" style="padding-left: 600px; float: left">
            <asp:Button ID="ButtonViewCart" runat="server" OnClick="ButtonViewCart_Click" CssClass="btn btn-warning" Text="View Cart" />
        </div>
    </div>
    <br />
    <br />
    <asp:GridView ID="ItemListGridView" runat="server" CssClass="table table-striped table-bordered" DataKeyNames="ItemID"
        OnSelectedIndexChanged="ItemListGridView_SelectedIndexChanged"
        Visible="False" EmptyDataText="There are no items to display." ShowHeaderWhenEmpty="True"
        AllowPaging="True" OnPageIndexChanging="ItemListGridView_PageIndexChanging" PageSize="15"
        GridLines="None" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="White" />
        <Columns>            
            <asp:BoundField DataField="Item_Description" HeaderText="Item Description" />
            <asp:BoundField DataField="UnitCost" HeaderText="Unit Cost" />
            <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit Of Measure" />
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#284775" ForeColor="White" />
        <HeaderStyle BackColor="#284775" ForeColor="White" />
        <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
    </asp:GridView>
    <br />


    <br />
    <br />

    <asp:GridView ID="CartGridView" runat="server" CssClass="table table-striped table-bordered" DataKeyNames="Id"
        OnRowCancelingEdit="CartGridView_RowCancelingEdit" OnRowDataBound="CartGridView_RowDataBound"
        OnRowDeleting="CartGridView_RowDeleting" OnRowEditing="CartGridView_RowEditing"
        OnRowUpdating="CartGridView_RowUpdating" OnRowCreated="CartGridView_RowCreated"
        Style="text-align: left" AutoGenerateColumns="False"
        EmptyDataText="There are no items in cart." ShowHeaderWhenEmpty="True"
        AllowPaging="False" OnPageIndexChanging="CartGridView_PageIndexChanging" PageSize="5"
        GridLines="None" CellPadding="4" ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Employee ID" />
            <asp:BoundField DataField="Description" HeaderText="Item description" SortExpression="ItemDescription" ReadOnly="true" />
            <asp:BoundField DataField="Qty" HeaderText="Quantity" />
            <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit of Measure" ReadOnly="true" />
            <asp:BoundField DataField="UnitCost" HeaderText="Unit Cost" SortExpression="UnitCost" ReadOnly="true" />
            <asp:BoundField DataField="TotalPrice" HeaderText="Total Amount" ReadOnly="true" />
            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
        </Columns>
        <FooterStyle BackColor="#284775" ForeColor="White" />
        <HeaderStyle BackColor="#284775" ForeColor="White" />
        <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
    </asp:GridView>

    <br />

    <asp:Button ID="ButtonDeleteRequest" runat="server" OnClick="ButtonDeleteRequest_Click" Text="Delete Request" CssClass="btn btn-danger" Visible="False" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="ButtonSubmitRequest" runat="server" OnClick="ButtonSubmitRequest_Click" Text="Submit Request" CssClass="btn btn-success" Visible="False" />

    <div>
        <br />
        <asp:Label ID="LabelDesc" runat="server" Text="Description"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelDescDisplay" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelCat" runat="server" Text="Category"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelCatDisplay" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelUom" runat="server" Text="Unit Of Measurement"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelUoMDisplay" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="LabelQty" runat="server" Text="Qty"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="ButtonDecreaseQty" runat="server" Text="-" OnClick="ButtonDecreaseQty_Click" Visible="False" />&nbsp;&nbsp;
                <asp:TextBox ID="TextBoxQty" runat="server" Width="45px">1</asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="ButtonIncreaseQty" runat="server" Text="+" OnClick="ButtonIncreaseQty_Click" Visible="False" />&nbsp;&nbsp;
                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="RangeValidator" ControlToValidate="TextBoxQty" ForeColor="Red" Type="Integer"
                    MaximumValue="500" MinimumValue="1">Qty should be between 1-500 pcs</asp:RangeValidator>
        <br />
        <br />
        <asp:Button ID="ButtonAddtoCart" runat="server" OnClick="ButtonAddtoCart_Click" CssClass="btn btn-primary" Text="Add to cart" />
        <br />
    </div>

</asp:Content>
