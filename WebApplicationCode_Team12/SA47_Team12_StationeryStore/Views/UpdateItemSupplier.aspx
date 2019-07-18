<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateItemSupplier.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.UpdateItemSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-horizontal">
        <h2 aria-dropeffect="none">Update Supplier Priority</h2>

        <div class="well">
            <asp:Label ID="Label7" runat="server" Text="Select Item :" Font-Size="Medium"></asp:Label>
           
            <asp:DropDownList ID="ItemDropDownList" runat="server" DataSourceID="SqlDataSource1" 
                DataTextField="Item_Description" DataValueField="ItemID" Width="211px" OnDataBound="ItemDropDownList_DataBound">
            </asp:DropDownList>
            &nbsp;
            <asp:Button ID="GoButton" runat="server" OnClick="GoButton_Click" CssClass="btn btn-primary" Text="Go" Height="29px" Width="53px" />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
                SelectCommand="SELECT [ItemID], [Item_Description] FROM [CatalogueInventory]">
            </asp:SqlDataSource>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ItemDropDownList" 
                ErrorMessage="Please Select One" ForeColor="Red" InitialValue="--Select--">
            </asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Font-Size="Medium"></asp:Label>
            <br />
            <asp:Label ID="Label4" runat="server" Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="Priority1DropDownList" runat="server" Visible="False" DataSourceID="SqlDataSource2" 
                DataTextField="Name" DataValueField="SupplierID" OnDataBound="Priority1DropDownList_DataBound">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Priority1DropDownList" 
                ErrorMessage="Please Select One" ForeColor="Red" InitialValue="--Select 1st Priority--"></asp:RequiredFieldValidator>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
                SelectCommand="SELECT [SupplierID], [Name] FROM [Supplier]">
            </asp:SqlDataSource>
            
            <br />
            <br />
            
            <asp:Label ID="Label2" runat="server" Font-Size="Medium"></asp:Label>
            <br />
            <asp:Label ID="Label5" runat="server" Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="Priority2DropDownList" runat="server" Visible="False" DataSourceID="SqlDataSource2" 
                DataTextField="Name" DataValueField="SupplierID" OnDataBound="Priority2DropDownList_DataBound">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Priority1DropDownList" 
                ErrorMessage="Please Select One" ForeColor="Red" InitialValue="--Select 2nd Priority--">
            </asp:RequiredFieldValidator>
            <br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Priority1DropDownList" ControlToValidate="Priority2DropDownList" 
                ErrorMessage="Cannot be the same as 1st Priority Selection" ForeColor="Red" Operator="NotEqual"></asp:CompareValidator>
            <br />
            <br />
            
            <asp:Label ID="Label3" runat="server" Font-Size="Medium"></asp:Label>
            <br />
            <asp:Label ID="Label6" runat="server" Font-Size="Medium"></asp:Label>
            <asp:DropDownList ID="Priority3DropDownList" runat="server" Visible="False" DataSourceID="SqlDataSource2" 
                DataTextField="Name" DataValueField="SupplierID" OnDataBound="Priority3DropDownList_DataBound">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Priority3DropDownList" 
                ErrorMessage="Please Select One" ForeColor="Red" InitialValue="--Select 3rd Priority--">
            </asp:RequiredFieldValidator>
            <br />
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="Priority1DropDownList" ControlToValidate="Priority3DropDownList" 
                ErrorMessage="Cannot be the same as 1st Priority Selection" ForeColor="Red" Operator="NotEqual"></asp:CompareValidator>
            <br />
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="Priority2DropDownList" ControlToValidate="Priority3DropDownList" 
                ErrorMessage="Cannot be the same as 2nd Priority Selection" ForeColor="Red" Operator="NotEqual"></asp:CompareValidator>
            <br />
            <br />
            <asp:Button ID="UpdateButton" runat="server" OnClick="UpdateButton_Click" CssClass="btn btn-primary" Text="Update" Visible="False" />
            <br />

        </div>
    </div>


</asp:Content>
