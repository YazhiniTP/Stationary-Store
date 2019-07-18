<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateDelegation.aspx.cs" Inherits="SA47_Team12_StationeryStore.Views.CreateDelegation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h2 aria-dropeffect="none">Delegation of Role</h2>

        <div class="well">
            
            <asp:RadioButtonList ID="DateRadioButtonList" runat="server" >
                <asp:ListItem Selected="True">Select Start Date</asp:ListItem>
                <asp:ListItem>Select End Date</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <asp:Label ID="Label3" runat="server" Font-Size="Medium"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Font-Size="Medium"></asp:Label>
            <br />
            <asp:Label ID="Label4" runat="server" Font-Size="Medium"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Font-Size="Medium"></asp:Label>
            <br />
            <br />
            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" 
                Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" 
                OnSelectionChanged="Calendar1_SelectionChanged" TitleFormat="Month" Width="400px">
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                <DayStyle Width="14%" />
                <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
                <TodayDayStyle BackColor="#CCCC99" />
            </asp:Calendar>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Select Staff" Font-Size="Medium"></asp:Label>
            <br />
            <br />
            <asp:DropDownList ID="SelectStaffDropDownList" runat="server" OnDataBound="SelectStaffDropDownList_DataBound" Height="20px" Width="218px" >
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SelectStaffDropDownList" 
                ErrorMessage="Please Select One" ForeColor="Red" InitialValue="--Select--"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" class="btn btn-primary" Text="Submit" Width="117px" />
            <br />
        </div>
    </div>
</asp:Content>
