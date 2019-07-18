<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CancelDelegation.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.CancelDelegation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 aria-dropeffect="none">Cancel Delegation</h2>
    <p aria-dropeffect="none">Overall Delegation History</p>

    <asp:GridView ID="DelegationHistoryGridView" runat="server" CssClass="table table-striped table-bordered" 
        AutoGenerateColumns="False" DataKeyNames="DelegationID"
        EmptyDataText="There is no job delegated." ShowHeaderWhenEmpty="True"
         AllowPaging="True" OnPageIndexChanging="DelegationHistoryGridView_PageIndexChanging" PageSize="5" 
        GridLines="None" CellPadding="4" ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
            <asp:BoundField DataField="Name" HeaderText="Employee Name" SortExpression="Name" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#284775" ForeColor="White" />
            <HeaderStyle BackColor="#284775" ForeColor="White" />
            <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
    </asp:GridView>
    <br />
    Ongoing/Upcoming Delegation Schedule<br />
    <br />
    <asp:GridView ID="DelegationScheduleGridView" runat="server" CssClass="table table-striped table-bordered" 
        AutoGenerateColumns="False" DataKeyNames="DelegationID"
        OnRowDeleting="DelegationScheduleGridView_RowDeleting" OnRowDataBound="DelegationScheduleGridView_RowDataBound"
        EmptyDataText="There is no job delegated." ShowHeaderWhenEmpty="True"
         AllowPaging="True" OnPageIndexChanging="DelegationScheduleGridView_PageIndexChanging" PageSize="5" 
        GridLines="None" CellPadding="4" ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="StartDate" SortExpression="StartDate">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StartDate") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EndDate" SortExpression="EndDate">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EndDate") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Employee Name" SortExpression="Name" />
            <asp:CommandField  ShowDeleteButton="True" DeleteText="Cancel Delegation Now" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#284775" ForeColor="White" />
            <HeaderStyle BackColor="#284775" ForeColor="White" />
            <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
    </asp:GridView>


</asp:Content>
