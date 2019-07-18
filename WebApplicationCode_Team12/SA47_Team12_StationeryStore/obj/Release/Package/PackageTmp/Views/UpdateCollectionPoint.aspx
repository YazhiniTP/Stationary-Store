<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateCollectionPoint.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.UpdateCollectionPoint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-horizontal">
            
    <h2 aria-dropeffect="none">Manage Collection Point</h2>
         <div class="well">
             <asp:Label ID="Label2" runat="server" Text="Current Collection Point is :" Font-Size="Large"></asp:Label>
            
            &nbsp;&nbsp;&nbsp;
            
            <asp:Label ID="Label1" runat="server" Font-Size="Medium"></asp:Label>
            <br />
            <br />
             <asp:Label ID="Label3" runat="server" Text="Select Your New Collection Point : " Font-Size="Large"></asp:Label>
            
             <br />
            
             <asp:RadioButtonList ID="CPRadioButtonList" runat="server" 
                DataSourceID="SqlDataSource1" DataTextField="Location" DataValueField="CollectionID">
            </asp:RadioButtonList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
                SelectCommand="SELECT [CollectionID], [Location] FROM [Collection]"></asp:SqlDataSource>
            <br />
            <asp:Button ID="UpdateButton" runat="server" OnClick="UpdateButton_Click"  CssClass="btn btn-primary" Text="Update" />        
        </div>
        </div>

</asp:Content>
