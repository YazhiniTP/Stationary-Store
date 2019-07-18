<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PendingPO.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.PendingPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 aria-dropeffect="none">Pending List of Orders </h2>
           <div>
               <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="Select supplier :"></asp:Label>
               <br />
            <br />
            <asp:RadioButton ID="AllSupplier" runat="server"  Text="All" Checked="true" GroupName="Supplier" OnCheckedChanged="AllSupplier_CheckedChanged" />
               &nbsp;&nbsp;
            <asp:RadioButton ID="Supplier1" runat="server" Text="ALPHA Company Supplies" GroupName="Supplier" OnCheckedChanged="Supplier1_CheckedChanged" />
               &nbsp;&nbsp;
            <asp:RadioButton ID="Supplier2" runat="server" Text="Cheap Stationer" GroupName="Supplier" OnCheckedChanged="Supplier2_CheckedChanged" />
               &nbsp;&nbsp;
            <asp:RadioButton ID="Supplier3" runat="server" Text="BANES Shop" GroupName="Supplier" OnCheckedChanged="Supplier3_CheckedChanged" />
               &nbsp;&nbsp;
            <asp:RadioButton ID="Supplier4" runat="server" Text="OMEGA Stationery Supplier" GroupName="Supplier" OnCheckedChanged="Supplier4_CheckedChanged" />
            <br />
            <br />
            <asp:Button ID="SelectSupButton" runat="server" Text="Select" OnClick="SelectSupButton_Click" CssClass="btn btn-primary" />
            <br />
               <br />
            <br />
            <asp:GridView DataKeyNames="POID" ID="PendingOrderGridView" runat="server" CssClass="table table-striped table-bordered"
                AutoGenerateColumns="False" EmptyDataText="There are no orders" ShowHeaderWhenEmpty="True"
                AllowPaging="True" OnPageIndexChanging="PendingOrderGridView_PageIndexChanging" PageSize="5" 
                GridLines="None" CellPadding="4" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="FirstSupplier" ReadOnly="true" HeaderText="Supplier"/>
                    <asp:BoundField DataField="Description" ReadOnly="true" HeaderText="Description"/>
                    
                    <asp:BoundField DataField="OrderQty" ReadOnly="true" HeaderText="OrderQty"/>
                    <asp:BoundField DataField="SubmissionDate" ReadOnly="true" HeaderText="Submission Date"/>
                    
                    <asp:TemplateField>
                       <ItemTemplate>
                            <asp:Button ID="Confirm" runat="server" Text="Confirm Delivery" OnClick="Confirm_Click" />
                       </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
                 <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#284775" ForeColor="White" />
            <HeaderStyle BackColor="#284775" ForeColor="White" />
            <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Right" />
                        
            </asp:GridView>
            
        </div>

</asp:Content>
