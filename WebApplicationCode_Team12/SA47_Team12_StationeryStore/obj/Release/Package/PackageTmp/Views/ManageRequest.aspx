<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageRequest.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.ManageRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div>
        <h2 aria-dropeffect="none">Manage Request</h2>
              <asp:Label ID="Label2" runat="server" Text="Select status:" Font-Size="Large"></asp:Label>
&nbsp;
            &nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:RadioButtonList ID="StatusRadioButtonList" runat="server">
            </asp:RadioButtonList>
            <br />
            <asp:Button ID="ViewButton" runat="server" Height="26px" OnClick="ViewButton_Click" CssClass="btn btn-primary" Text=" View List" />
            <br />
            <br />
            <br />
            
            <asp:GridView ID="RequestGridView" runat="server" AutoGenerateColumns="False"  CssClass="table table-striped table-bordered"
                OnSelectedIndexChanged="RequestGridView_SelectedIndexChanged" DataKeyNames="RequestID" 
                EmptyDataText="There are no request to display."  ShowHeaderWhenEmpty="True" 
                AllowPaging="True" OnPageIndexChanging="RequestGridView_PageIndexChanging" PageSize="5" 
                GridLines="None" CellPadding="4" ForeColor="#333333" >
                <AlternatingRowStyle BackColor="White" />

                <Columns>
                    <asp:BoundField DataField="RequestId" HeaderText="Request ID" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="SubmittedTime" HeaderText="Date Submitted" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="ApprovedTime" HeaderText="Date Approved" />
                    <asp:CommandField ButtonType="Button" HeaderText="Details" ShowSelectButton="True" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#284775" ForeColor="White" />
                <HeaderStyle BackColor="#284775" ForeColor="White" />
                <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
            </asp:GridView>
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
            <asp:Label ID="Label3" runat="server" Text="Additional comments for approval/rejection (optional) :" Visible="False" Font-Size="Medium"></asp:Label>
            &nbsp;<br />
            <br />
            <asp:TextBox ID="RemarksTextBox" runat="server" Height="118px" TextMode="MultiLine" Visible="False" Width="368px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="RejectButton" runat="server" OnClick="RejectButton_Click" Text="Reject Request" CssClass="btn btn-danger" Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="ApproveButton" runat="server" OnClick="ApproveButton_Click" Text="Approve Request" CssClass="btn btn-success" Visible="False" />
            <br />
            <br />
            <br />
            


        </div>



</asp:Content>
