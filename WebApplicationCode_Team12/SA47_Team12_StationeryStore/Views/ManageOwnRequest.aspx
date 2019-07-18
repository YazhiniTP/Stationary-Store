<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageOwnRequest.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.ManageOwnRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2 aria-dropeffect="none">Manage own request</h2>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Select status :" Font-Size="Large"></asp:Label>
        <br />
        <asp:RadioButtonList ID="StatusRadioButtonList" runat="server" >
        </asp:RadioButtonList>
        <br />
        <asp:Button ID="SelectButton" runat="server" OnClick="SelectButton_Click" CssClass="btn btn-primary" Text="Select" />
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
            SelectCommand="SELECT * FROM [RequestDetail]"></asp:SqlDataSource>
        <br />
        <br />
        <asp:GridView ID="PendingRequestGridView" runat="server" CssClass="table table-striped table-bordered"
            OnRowDataBound="RequestGridView_RowDataBound" AutoGenerateColumns="False" 
            OnSelectedIndexChanged="PendingRequestGridView_SelectedIndexChanged" OnRowDeleting="PendingRequestGridView_RowDeleting"
            EmptyDataText="There are no request to display." ShowHeaderWhenEmpty="True" DataKeyNames="RequestId"
            AllowPaging="True" OnPageIndexChanging="PendingRequestGridView_PageIndexChanging" PageSize="5"
            GridLines="None" CellPadding="4" ForeColor="#333333" 
            >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="RequestID" HeaderText="Request ID" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:C}"/>
                <asp:BoundField DataField="SubmittedTime" HeaderText="Submitted Date"/>
                <asp:CommandField ButtonType="Button" SelectText="View Details" ShowSelectButton="True" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#284775" ForeColor="White" />
            <HeaderStyle BackColor="#284775" ForeColor="White" />
            <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
        </asp:GridView>
        <br />
        <br />

         <asp:GridView ID="RequestGridView" runat="server" CssClass="table table-striped table-bordered"
            OnRowDataBound="RequestGridView_RowDataBound" EmptyDataText="There are no request to display." ShowHeaderWhenEmpty="True" DataKeyNames="RequestId"
            AllowPaging="True" OnPageIndexChanging="RequestGridView_PageIndexChanging" PageSize="5"
            GridLines="None" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" OnSelectedIndexChanged="RequestGridView_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="RequestID" HeaderText="Request ID" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:C}"/>
                <asp:BoundField DataField="SubmittedTime" HeaderText="Submitted Date"/>
                <asp:BoundField DataField="Status" HeaderText="Status"/>
                <asp:BoundField DataField="Remarks" HeaderText="Remarks"/>
                <asp:CommandField ButtonType="Button" SelectText="View Details" ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#284775" ForeColor="White" />
            <HeaderStyle BackColor="#284775" ForeColor="White" />
            <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
        </asp:GridView>
        <br />
        <br />
        <asp:GridView ID="RequestDetailsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered"
                EmptyDataText="There are no request to display."  ShowHeaderWhenEmpty="True"  DataKeyNames="Id"
                AllowPaging="True" OnPageIndexChanging="RequestDetailsGridView_PageIndexChanging" PageSize="5" 
                GridLines="None" CellPadding="4" ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Request ID" />
                    <asp:BoundField DataField="Description" HeaderText="Item" />
                    <asp:BoundField DataField="Qty" HeaderText="Qty" />
                    <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit of Measure" />
                    <asp:BoundField DataField="UnitCost" HeaderText="Unit Cost" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="TotalPrice" HeaderText="Total Amt" DataFormatString="{0:C}" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#284775" ForeColor="White" />
                <HeaderStyle BackColor="#284775" ForeColor="White" />
                <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
            </asp:GridView>
        <br />
        <asp:Button ID="DeleteButton" runat="server" OnClick="ButtonDeleteRequest_Click" CssClass="btn btn-danger" Text="Delete Request" Visible="False" />
        <br />
    </div>
</asp:Content>
