<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewItem.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.AddNewItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-horizontal">
    <h2 aria-dropeffect="none">Add A New Item</h2>
    
        <div class="well">
            &nbsp;ItemID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="ItemIdTextBox" runat="server" Height="20px" Width="243px"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ItemIdTextBox" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <br />
            Category&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <asp:DropDownList ID="CategoryDropDownList" runat="server" 
                DataSourceID="SqlDataSource1" DataTextField="Category_Description" DataValueField="CatID" 
                Height="20px" OnDataBound="CategoryDropDownList_DataBound" Width="254px">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
                SelectCommand="SELECT * FROM [Category]">
            </asp:SqlDataSource>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="CategoryDropDownList" 
                ErrorMessage="Please Select One" InitialValue="--Select--" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <br />
            Description&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox ID="DescTextBox" runat="server" Height="20px" Width="248px"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="DescTextBox" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <br />
            Unit of Measure&nbsp;
            <asp:TextBox ID="UoMTextBox" runat="server" Height="20px" Width="246px"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="UoMTextBox" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <br />
            Unit Price&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="UPTextBox" runat="server" Height="20px" Width="154px"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="UPTextBox" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            &nbsp;<asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="UPTextBox" 
                ErrorMessage="Invalid Input" MaximumValue="10000" MinimumValue="0" Type="Double"></asp:RangeValidator>
            <br />
            <br />
            <br />
            Select supplier for each priority<br />
            <br />
            1st Priority&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="Priority1DropDownList" runat="server" DataSourceID="SqlDataSource2" 
                DataTextField="Name" DataValueField="SupplierID" Width="259px" OnDataBound="Priority1DropDownList_DataBound" Height="20px"> 
            </asp:DropDownList>
            &nbsp;
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [Supplier]"></asp:SqlDataSource>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Priority1DropDownList" 
                ErrorMessage="Please Select One" ForeColor="Red" InitialValue="--Select 1st Priority--"></asp:RequiredFieldValidator>
            <br />
            <br />
            2nd Priority &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="Priority2DropDownList" runat="server" DataSourceID="SqlDataSource2" 
                DataTextField="Name" DataValueField="SupplierID" Width="259px" OnDataBound="Priority2DropDownList_DataBound" Height="20px">
            </asp:DropDownList>
            &nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="Priority2DropDownList" 
                ErrorMessage="Please Select One" ForeColor="Red" InitialValue="--Select 2nd Priority--"></asp:RequiredFieldValidator>
            <br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Priority1DropDownList" ControlToValidate="Priority2DropDownList" 
                ErrorMessage="Cannot be the same as 1st Priority Selection" ForeColor="Red" Operator="NotEqual"></asp:CompareValidator>
            <br />
            <br />
            3rd Priority&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="Priority3DropDownList" runat="server" 
                DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="SupplierID" Width="259px" 
                OnDataBound="Priority3DropDownList_DataBound" Height="20px">
            </asp:DropDownList>
            &nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="Priority3DropDownList" 
                ErrorMessage="Please Select One" ForeColor="Red" InitialValue="--Select 3rd Priority--"></asp:RequiredFieldValidator>
            <br />
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="Priority1DropDownList" ControlToValidate="Priority3DropDownList" 
                ErrorMessage="Cannot be the same as 1st Priority Selection" ForeColor="Red" Operator="NotEqual"></asp:CompareValidator>
            <br />
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="Priority2DropDownList" ControlToValidate="Priority3DropDownList" 
                ErrorMessage="Cannot be the same as 2nd Priority Selection" ForeColor="Red" Operator="NotEqual"></asp:CompareValidator>
            <br />
            <br />
            <asp:Button ID="AddNewButton" runat="server" OnClick="AddNewButton_Click" CssClass="btn btn-primary" Text="Add New " />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
        </div>
        <div>
        </div>
    </div>

</asp:Content>
