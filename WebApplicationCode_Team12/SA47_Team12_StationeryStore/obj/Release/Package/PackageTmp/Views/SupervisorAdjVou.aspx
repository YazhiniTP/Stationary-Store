<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupervisorAdjVou.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.SupervisorAdjVou" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h2 aria-dropeffect="none">Manage Adjustment Voucher</h2>

        <div class="well">

            <asp:Label ID="Label2" runat="server" Text="Select status:" Font-Size="Large"></asp:Label>
            <br />
            <asp:RadioButtonList ID="StatusRadioButtonList" runat="server">
                <asp:ListItem>Pending Supervisor Approval</asp:ListItem>
                <asp:ListItem>Approved</asp:ListItem>
                <asp:ListItem>Rejected</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <asp:Button ID="ViewButton" runat="server" Height="26px" OnClick="ViewButton_Click" CssClass="btn btn-primary" Text="View List" />
            <br />
            <br />
            <br />
            <asp:GridView ID="AdjVouGridView" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" 
                OnSelectedIndexChanged="AdjVouGridView_SelectedIndexChanged" DataKeyNames="VoucherID"
                EmptyDataText="There are no request to approve."  ShowHeaderWhenEmpty="True" 
                AllowPaging="True" OnPageIndexChanging="AdjVouGridView_PageIndexChanging" PageSize="5" 
                GridLines="None" CellPadding="4" ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="VoucherID" HeaderText="VoucherID" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                    <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:CommandField ButtonType="Button" HeaderText="Details" ShowSelectButton="True" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#284775" ForeColor="White" />
                <HeaderStyle BackColor="#284775" ForeColor="White" />
                <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
                <RowStyle BackColor="White" />
            </asp:GridView>
            <br />

            <asp:GridView ID="AdjVouDetailsGridView" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" 
                OnRowDataBound="AdjVouDetailsGridView_RowDataBound" DataKeyNames="ItemID,ActualQty,AdjQty,VoucherID,ApprovalDate,AdjAmt" 
                Height="191px" Style="margin-bottom: 13px" EmptyDataText="There are no items."  ShowHeaderWhenEmpty="True" 
                AllowPaging="True" OnPageIndexChanging="AdjVouDetailsGridView_PageIndexChanging" PageSize="5"
                GridLines="None" CellPadding="4" ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="VoucherID" HeaderText="Voucher ID" />
                    <asp:BoundField DataField="ItemName" HeaderText="Item Description" />
                    <asp:BoundField DataField="Reasons" HeaderText="Reasons for Adjustment" />
                    <asp:BoundField DataField="AdjQty" HeaderText="Adjusted Qty" />
                    <asp:BoundField DataField="AdjAmt" HeaderText="Adjusted Amt" />
                    <asp:BoundField DataField="Remarks" HeaderText="Supervisor/Manager Remarks" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#284775" ForeColor="White" />
                <HeaderStyle BackColor="#284775" ForeColor="White" />
                <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
                <RowStyle BackColor="White" />
            </asp:GridView>
            &nbsp;
            <br />
            <asp:Label ID="Label3" runat="server" Text="Additional comments for approval/rejection (optional) :" Visible="False" Font-Size="Large"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="RemarkTextBox" runat="server" Height="211px" TextMode="MultiLine" Visible="False" Width="607px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:Button ID="RejectButton" runat="server" OnClick="RejectButton_Click" CssClass="btn btn-danger" Text="Reject Voucher" Visible="False" />            
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
            <asp:Button ID="ApproveButton" runat="server" OnClick="ApproveButton_Click" CssClass="btn btn-success" Text="Approve Voucher" Visible="False"  />
            &nbsp;&nbsp;&nbsp;<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<br />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="ManagerAppButton" runat="server" OnClick="ManagerAppButton_Click" CssClass="btn btn-primary" Text="Submit to Manager for Approval" Visible="False" Width="293px" />
        </div>
    </div>

</asp:Content>
