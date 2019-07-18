<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Disbursement.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.Disbursement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 aria-dropeffect="none">Disbursement list</h2>
    <div>
        <asp:GridView DataKeyNames="ItemID" ID="DisbursementGridView" runat="server" CssClass="table table-striped table-bordered"
            AutoGenerateColumns="False" OnRowEditing="DisbursementGridView_RowEditing"
            OnRowCancelingEdit="DisbursementGridView_RowCancelingEdit"
            OnRowUpdating="DisbursementGridView_RowUpdating"
            EmptyDataText="There are no items need to Deliver." ShowHeaderWhenEmpty="True"
            AllowPaging="True" OnPageIndexChanging="DisbursementGridView_PageIndexChanging" PageSize="5"
            GridLines="None" CellPadding="4" ForeColor="#333333" OnRowCommand="DisbursementGridView_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>

                <asp:BoundField DataField="ItemID" ReadOnly="true" HeaderText="ItemID" />
                <asp:BoundField DataField="ItemDes" ReadOnly="true" HeaderText="Description" />
                <asp:TemplateField HeaderText="Disbursed Qty">
                    <EditItemTemplate>
                        <div>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DisbursedQty") %>'></asp:TextBox>
                        </div>
                        <div>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck" Type="Integer"
                                ControlToValidate="TextBox2" ErrorMessage="Input must be a number" Font-Bold="False" ForeColor="Red"></asp:CompareValidator>
                        </div>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                                ErrorMessage="Input cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="RangeValidator" ControlToValidate="TextBox2" ForeColor="Red" Type="Integer"
                             MaximumValue="500" MinimumValue="0">Qty should not be negative</asp:RangeValidator>
                        </div>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("DisbursedQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ButtonType="Button" HeaderText="Update" ShowEditButton="True" />
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
        <br />
    </div>
    <asp:Button ID="ProceedToDeliverButton" runat="server" Text="Proceed to Schedule" OnClick="ProceedToDeliverButton_Click" class="btn btn-primary" />

    <br />
</asp:Content>
