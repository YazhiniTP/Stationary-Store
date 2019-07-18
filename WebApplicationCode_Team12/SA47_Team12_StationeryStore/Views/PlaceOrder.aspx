<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlaceOrder.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.PlaceOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2 aria-dropeffect="none">Place Orders </h2>
   <div>
            <asp:GridView ID="PlaceOrderGridView" runat="server" DataKeyNames="ItemID" CssClass="table table-striped table-bordered"
                AutoGenerateColumns="False" OnRowEditing="PlaceOrderGridView_RowEditing"
                OnRowCancelingEdit="PlaceOrderGridView_RowCancelingEdit" 
                OnRowUpdating="PlaceOrderGridView_RowUpdating" OnRowDeleting="PlaceOrderGridView_RowDeleting"
                EmptyDataText="There are no items need to order." ShowHeaderWhenEmpty="True" 
                AllowPaging="True" OnPageIndexChanging="PlaceOrderGridView_PageIndexChanging" PageSize="5" 
                GridLines="None" CellPadding="4" ForeColor="#333333" OnRowDataBound="PlaceOrderGridView_RowDataBound" >
            <AlternatingRowStyle BackColor="White" />
                <Columns>
                    
                    <asp:BoundField DataField="ItemID" ReadOnly="true" HeaderText="ItemID"/>
                     
                    <asp:BoundField DataField="Description" ReadOnly="true" HeaderText="Description"/>
                    <asp:BoundField DataField="UnitOfMeasure" ReadOnly="true" HeaderText="Unit of Measure"/>
                    <asp:BoundField DataField="UnitCost" ReadOnly="true" HeaderText="Unit Cost"/>
                    <asp:BoundField DataField="FirstSupplier" ReadOnly="true" HeaderText="Supplier"/>  

                    <asp:TemplateField HeaderText="Alter Supplier">
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Name" DataValueField="SupplierID"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Order Qty">
                        <EditItemTemplate>
                            <div>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("OrderQty") %>'></asp:TextBox>
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
                             MaximumValue="500" MinimumValue="0">"Qty should not be negative"</asp:RangeValidator>
                        </div>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("OrderQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     
                 
                    
                    <asp:CommandField ButtonType="Button" HeaderText="Update" ShowEditButton="True" />
                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" HeaderText="Delete" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#284775" ForeColor="White" />
            <HeaderStyle BackColor="#284775" ForeColor="White" />
            <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
            </asp:GridView>
            <br />
        </div>
        <asp:Button ID="ConfirmButton" runat="server" OnClick="ConfirmButton_Click" CssClass="btn btn-primary" Text="Confirm Order" />

</asp:Content>
