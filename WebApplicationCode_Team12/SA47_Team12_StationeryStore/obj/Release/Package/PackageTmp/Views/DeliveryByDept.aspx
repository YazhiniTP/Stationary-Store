<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeliveryByDept.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.DeliveryByDept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 aria-dropeffect="none">Delivery Details by department</h2>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Select status:" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:RadioButtonList ID="StatusRadioButtonList" runat="server">
            <asp:ListItem>Outstanding Delivery</asp:ListItem>
            <asp:ListItem>Today's Delivery</asp:ListItem>
            <asp:ListItem>Upcoming Delivery</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Button ID="ViewButton" runat="server" Height="26px" OnClick="ViewButton_Click" CssClass="btn btn-primary" Text=" View List" />
        <br />
    </div>

    <div>
        <br />
        <br />
        <div class="row">
            <div class="col-sm-4">
                <asp:Label ID="LabelDep2001" runat="server" Text="Department :" Font-Size="Large"></asp:Label>
                <asp:Label ID="Dep2001" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelCP2001" runat="server" Text="Collection Point :" Font-Size="Large"></asp:Label>
                <asp:Label ID="CP2001" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelUR2001" runat="server" Text="User Representative :" Font-Size="Large"></asp:Label>
                <asp:Label ID="UR2001" runat="server" Font-Size="Large"></asp:Label>
            </div>
        </div>
        <br />
        <asp:GridView DataKeyNames="DisbursementID" ID="DeliveryDepTable2001" runat="server" CssClass="table table-striped table-bordered"
            Visible="false" AutoGenerateColumns="False" OnRowEditing="DeliveryDepTable2001_RowEditing"
            OnRowCancelingEdit="DeliveryDepTable2001_RowCancelingEdit"
            OnRowUpdating="DeliveryDepTable2001_RowUpdating"
            EmptyDataText="There are no items need to Deliver." ShowHeaderWhenEmpty="True"
            AllowPaging="True" OnPageIndexChanging="DeliveryDepTable2001_PageIndexChanging" PageSize="5"
            GridLines="None" CellPadding="4" ForeColor="#333333" OnRowCommand="DeliveryDepTable2001_RowCommand">
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ForeColor="Red"
                                ErrorMessage="Input cannot be empty"></asp:RequiredFieldValidator>
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
        <asp:Button ID="Confirm2001" runat="server" Text="Confirm" OnClick="Confirm2001_Click" CssClass="btn btn-primary" Visible="false" />

    </div>
    <br />
    <br />
    <div>
        <br />
        <br />
        <div class="row">
            <div class="col-sm-4">
                <asp:Label ID="LabelDep2002" runat="server" Text="Department :" Font-Size="Large"></asp:Label>
                <asp:Label ID="Dep2002" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelCP2002" runat="server" Text="Collection Point :" Font-Size="Large"></asp:Label>
                <asp:Label ID="CP2002" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelUR2002" runat="server" Text="User Representative :" Font-Size="Large"></asp:Label>
                <asp:Label ID="UR2002" runat="server" Font-Size="Large"></asp:Label>
            </div>
        </div>
        <br />
        <asp:GridView DataKeyNames="DisbursementID" ID="DeliveryDepTable2002" runat="server" CssClass="table table-striped table-bordered"
            Visible="false" AutoGenerateColumns="False" OnRowEditing="DeliveryDepTable2002_RowEditing"
            OnRowCancelingEdit="DeliveryDepTable2002_RowCancelingEdit"
            OnRowUpdating="DeliveryDepTable2002_RowUpdating"
            EmptyDataText="There are no items need to Deliver." ShowHeaderWhenEmpty="True"
            AllowPaging="True" OnPageIndexChanging="DeliveryDepTable2002_PageIndexChanging" PageSize="5"
            GridLines="None" CellPadding="4" ForeColor="#333333" OnRowCommand="DeliveryDepTable2002_RowCommand">
            <AlternatingRowStyle BackColor="White" />

            <Columns>
                <asp:BoundField DataField="ItemID" ReadOnly="true" HeaderText="Department" />
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ForeColor="Red"
                                ErrorMessage="Input cannot be empty"></asp:RequiredFieldValidator>
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
        <asp:Button ID="Confirm2002" runat="server" Text="Confirm" OnClick="Confirm2002_Click" CssClass="btn btn-primary" Visible="false" />
    </div>
    <br />
    <br />
    <div>
        <br />
        <br />
        <div class="row">
            <div class="col-sm-4">
                <asp:Label ID="LabelDep2003" runat="server" Text="Department :" Font-Size="Large"></asp:Label>
                <asp:Label ID="Dep2003" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelCP2003" runat="server" Text="Collection Point :" Font-Size="Large"></asp:Label>
                <asp:Label ID="CP2003" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelUR2003" runat="server" Text="User Representative :" Font-Size="Large"></asp:Label>
                <asp:Label ID="UR2003" runat="server" Font-Size="Large"></asp:Label>
            </div>
        </div>
        <br />
        <asp:GridView DataKeyNames="DisbursementID" ID="DeliveryDepTable2003" runat="server" CssClass="table table-striped table-bordered"
            Visible="false" AutoGenerateColumns="False" OnRowEditing="DeliveryDepTable2003_RowEditing"
            OnRowCancelingEdit="DeliveryDepTable2003_RowCancelingEdit"
            OnRowUpdating="DeliveryDepTable2003_RowUpdating"
            EmptyDataText="There are no items need to Deliver." ShowHeaderWhenEmpty="True"
            AllowPaging="True" OnPageIndexChanging="DeliveryDepTable2003_PageIndexChanging" PageSize="5"
            GridLines="None" CellPadding="4" ForeColor="#333333" OnRowCommand="DeliveryDepTable2003_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ItemID" ReadOnly="true" HeaderText="Department" />
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ForeColor="Red"
                                ErrorMessage="Input cannot be empty"></asp:RequiredFieldValidator>
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
        <asp:Button ID="Confirm2003" runat="server" Text="Confirm" OnClick="Confirm2003_Click" CssClass="btn btn-primary" Visible="false" />
    </div>
    <br />
    <br />

    <div>
        <br />
        <br />
        <div class="row">
            <div class="col-sm-4">
                <asp:Label ID="LabelDep2004" runat="server" Text="Department :" Font-Size="Large"></asp:Label>
                <asp:Label ID="Dep2004" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelCP2004" runat="server" Text="Collection Point :" Font-Size="Large"></asp:Label>
                <asp:Label ID="CP2004" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelUR2004" runat="server" Text="User Representative :" Font-Size="Large"></asp:Label>
                <asp:Label ID="UR2004" runat="server" Font-Size="Large"></asp:Label>
            </div>
        </div>
        <br />
        <asp:GridView DataKeyNames="DisbursementID" ID="DeliveryDepTable2004" runat="server" CssClass="table table-striped table-bordered"
            Visible="false" AutoGenerateColumns="False" OnRowEditing="DeliveryDepTable2004_RowEditing"
            OnRowCancelingEdit="DeliveryDepTable2004_RowCancelingEdit" OnRowUpdating="DeliveryDepTable2004_RowUpdating"
            EmptyDataText="There are no items need to Deliver." ShowHeaderWhenEmpty="True"
            AllowPaging="True" OnPageIndexChanging="DeliveryDepTable2004_PageIndexChanging" PageSize="5"
            GridLines="None" CellPadding="4" ForeColor="#333333" OnRowCommand="DeliveryDepTable2004_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ItemID" ReadOnly="true" HeaderText="Department" />
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ForeColor="Red"
                                ErrorMessage="Input cannot be empty"></asp:RequiredFieldValidator>
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
        <asp:Button ID="Confirm2004" runat="server" Text="Confirm" OnClick="Confirm2004_Click" CssClass="btn btn-primary" Visible="false" />
    </div>
    <br />
    <br />
    <div>
        <br />
        <br />
        <div class="row">
            <div class="col-sm-4">
                <asp:Label ID="LabelDep2005" runat="server" Text="Department :" Font-Size="Large"></asp:Label>
                <asp:Label ID="Dep2005" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelCP2005" runat="server" Text="Collection Point :" Font-Size="Large"></asp:Label>
                <asp:Label ID="CP2005" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelUR2005" runat="server" Text="User Representative :" Font-Size="Large"></asp:Label>
                <asp:Label ID="UR2005" runat="server" Font-Size="Large"></asp:Label>
            </div>
        </div>
        <br />
        <asp:GridView DataKeyNames="DisbursementID" ID="DeliveryDepTable2005" runat="server" CssClass="table table-striped table-bordered"
            Visible="false" AutoGenerateColumns="False" OnRowEditing="DeliveryDepTable2005_RowEditing"
            OnRowCancelingEdit="DeliveryDepTable2005_RowCancelingEdit" OnRowUpdating="DeliveryDepTable2005_RowUpdating"
            EmptyDataText="There are no items need to Deliver." ShowHeaderWhenEmpty="True"
            AllowPaging="True" OnPageIndexChanging="DeliveryDepTable2005_PageIndexChanging" PageSize="5"
            GridLines="None" CellPadding="4" ForeColor="#333333" OnRowCommand="DeliveryDepTable2005_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ItemID" ReadOnly="true" HeaderText="Department" />
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ForeColor="Red"
                                ErrorMessage="Input cannot be empty"></asp:RequiredFieldValidator>
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
        <asp:Button ID="Confirm2005" runat="server" Text="Confirm" OnClick="Confirm2005_Click" CssClass="btn btn-primary" Visible="false" />
    </div>
    <br />
    <br />
    <div>
        <br />
        <br />
        <div class="row">
            <div class="col-sm-4">
                <asp:Label ID="LabelDep2006" runat="server" Text="Department :" Font-Size="Large"></asp:Label>
                <asp:Label ID="Dep2006" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelCP2006" runat="server" Text="Collection Point :" Font-Size="Large"></asp:Label>
                <asp:Label ID="CP2006" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelUR2006" runat="server" Text="User Representative :" Font-Size="Large"></asp:Label>
                <asp:Label ID="UR2006" runat="server" Font-Size="Large"></asp:Label>
            </div>
        </div>
        <br />
        <asp:GridView DataKeyNames="DisbursementID" ID="DeliveryDepTable2006" runat="server" CssClass="table table-striped table-bordered"
            Visible="false" AutoGenerateColumns="False" OnRowEditing="DeliveryDepTable2006_RowEditing"
            OnRowCancelingEdit="DeliveryDepTable2006_RowCancelingEdit" OnRowUpdating="DeliveryDepTable2006_RowUpdating"
            EmptyDataText="There are no items need to Deliver." ShowHeaderWhenEmpty="True"
            AllowPaging="True" OnPageIndexChanging="DeliveryDepTable2006_PageIndexChanging" PageSize="5"
            GridLines="None" CellPadding="4" ForeColor="#333333" OnRowCommand="DeliveryDepTable2006_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ItemID" ReadOnly="true" HeaderText="Department" />
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ForeColor="Red"
                                ErrorMessage="Input cannot be empty"></asp:RequiredFieldValidator>
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
        <asp:Button ID="Confirm2006" runat="server" Text="Confirm" OnClick="Confirm2006_Click" CssClass="btn btn-primary" Visible="false" />
    </div>
    <br />
    <br />
    <div>
        <br />
        <br />
        <div class="row">
            <div class="col-sm-4">
                <asp:Label ID="LabelDep2007" runat="server" Text="Department :" Font-Size="Large"></asp:Label>
                <asp:Label ID="Dep2007" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelCP2007" runat="server" Text="Collection Point :" Font-Size="Large"></asp:Label>
                <asp:Label ID="CP2007" runat="server" Font-Size="Large"></asp:Label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="LabelUR2007" runat="server" Text="User Representative :" Font-Size="Large"></asp:Label>
                <asp:Label ID="UR2007" runat="server" Font-Size="Large"></asp:Label>
            </div>
        </div>
        <br />
        <asp:GridView DataKeyNames="DisbursementID" ID="DeliveryDepTable2007" runat="server" CssClass="table table-striped table-bordered"
            Visible="false" AutoGenerateColumns="False" OnRowEditing="DeliveryDepTable2007_RowEditing"
            OnRowCancelingEdit="DeliveryDepTable2007_RowCancelingEdit" OnRowUpdating="DeliveryDepTable2007_RowUpdating"
            EmptyDataText="There are no items need to Deliver." ShowHeaderWhenEmpty="True"
            AllowPaging="True" OnPageIndexChanging="DeliveryDepTable2007_PageIndexChanging" PageSize="5"
            GridLines="None" CellPadding="4" ForeColor="#333333" OnRowCommand="DeliveryDepTable2007_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ItemID" ReadOnly="true" HeaderText="Department" />
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ForeColor="Red"
                                ErrorMessage="Input cannot be empty"></asp:RequiredFieldValidator>
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
        <asp:Button ID="Confirm2007" runat="server" Text="Confirm" OnClick="Confirm2007_Click" CssClass="btn btn-primary" Visible="false" />

    </div>

</asp:Content>
